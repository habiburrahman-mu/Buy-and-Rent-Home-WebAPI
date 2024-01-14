using AutoMapper;
using BuyAndRentHomeWebAPI.Dtos;
using BuyAndRentHomeWebAPI.Errors;
using BuyAndRentHomeWebAPI.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using BuyAndRentHomeWebAPI.Services.Interfaces;

namespace BuyAndRentHomeWebAPI.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public AccountController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        //api/account/login
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDto loginRequest)
        {
            ApiError apiError = new ApiError();
            if (loginRequest.UserName == null || loginRequest.Password == null)
            {
                apiError.ErrorCode = BadRequest().StatusCode;
                apiError.ErrorMessage = "Provide mandatory informations";
                apiError.ErrorDetails = "This error appears when Username or Password is missing in request.";
                return BadRequest(apiError);
            }

            var user = await _userService.Authenticate(loginRequest);
            if (user == null)
            {
                apiError.ErrorCode = Unauthorized().StatusCode;
                apiError.ErrorMessage = "Invalid User ID or Password";
                apiError.ErrorDetails = "This error appears when provided user id or password doesnot exist.";
                return Unauthorized(apiError);
            }

            var loginResponse = _userService.CreateLoginCredintials(user);
            return Ok(loginResponse);
        }

        // api/account/register
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto register)
        {
            ApiError apiError = new ApiError();

            if (register.UserName.IsEmpty() || register.Email.IsEmpty() || register.Password.IsEmpty())
            {
                apiError.ErrorCode = BadRequest().StatusCode;
                apiError.ErrorMessage = "Provide mandatory informations";
                apiError.ErrorDetails = "This error appears when necessary information is missing in request.";
                return BadRequest(apiError);
            }

            if (await _userService.UserAlreadyExists(register.UserName))
            {
                apiError.ErrorCode = BadRequest().StatusCode;
                apiError.ErrorMessage = "User already exists, please try something else";
                apiError.ErrorDetails = "This error appears when username already exist in record.";
                return BadRequest(apiError);
            }

            var isRegistered = await _userService.Register(register);
            if (isRegistered)
            {
                return StatusCode(201);
            }
            else
            {
                return StatusCode(500);
            }
        }
    }
}

using AutoMapper;
using BuyandRentHomeWebAPI.Dtos;
using BuyandRentHomeWebAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BuyandRentHomeWebAPI.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AccountController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        //api/account/login
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDto loginRequest)
        {
            var user = await _unitOfWork.UserRepository.Authenticate(loginRequest.Username, loginRequest.Password);
            if(user == null)
            {
                return Unauthorized();
            }
            var loginResponse = new LoginResponseDto();
            loginResponse.Username = user.Username;
            loginResponse.Token = "Token to be generated";
            return Ok(loginResponse);
        }
    }
}

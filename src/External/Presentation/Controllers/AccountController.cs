using Presentation.Errors;
using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Common.Extensions;
using Domain.Entities;
using Presentation.Services;

namespace Presentation.Controllers;

public class AccountController : BaseController
{
    private readonly IUserService _userService;
    private readonly ITokenService tokenService;

    public AccountController(IUserService userService, ITokenService tokenService)
    {
        _userService = userService;
        this.tokenService = tokenService;
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

        var user = await _userService.GetUserDetail(loginRequest);
        if (user == null)
        {
            apiError.ErrorCode = Unauthorized().StatusCode;
            apiError.ErrorMessage = "Invalid User ID or Password";
            apiError.ErrorDetails = "This error appears when provided user id or password does not exist.";
            return Unauthorized(apiError);
        }

        var loginResponse = CreateLoginCredintials(user);
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

        if (await _userService.IsUserAlreadyExists(register.UserName))
        {
            apiError.ErrorCode = BadRequest().StatusCode;
            apiError.ErrorMessage = "User name already exists, please try something else";
            apiError.ErrorDetails = "This error appears when user name already exist in record.";
            return BadRequest(apiError);
        }

        var isRegistered = await _userService.Register(register);
        return Ok(isRegistered);
        //if (isRegistered)
        //{
        //    return StatusCode(201);
        //}
        //else
        //{
        //    return StatusCode(500);
        //}
    }

    private LoginResponseDto CreateLoginCredintials(User user)
    {
        var loginResponse = new LoginResponseDto();
        loginResponse.Name = user.Name;
        loginResponse.UserName = user.Username;
        loginResponse.Token = tokenService.CreateJWT(user);
        return loginResponse;
    }
}

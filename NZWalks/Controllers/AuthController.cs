using NZWalks.Models.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZWalks.Repositories;

namespace NZWalks.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly ITokenReposity _tokenRepository;

    public AuthController(UserManager<IdentityUser> userManager, ITokenReposity tokenRepository)  // UserManager class help us to create new user
    {
        _userManager = userManager;
        _tokenRepository = tokenRepository;
    }

    [HttpPost]
    [Route("Register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
    {
        var identityUser = new IdentityUser
        {
            UserName = registerRequestDto.Username,
            Email = registerRequestDto.Username
        };
        var identityResult = await _userManager.CreateAsync(identityUser, registerRequestDto.Password);
        
        if(identityResult.Succeeded)
        {
            // Add UserRoles to this User
            if(registerRequestDto.Roles != null)
            {
                identityResult = await _userManager.AddToRolesAsync(identityUser, registerRequestDto.Roles);

                if (identityResult.Succeeded)
                {
                    return Ok("User was register! Please login.");
                }
            }  
        }
        return BadRequest("Something went wront");
    }

    [HttpPost]
    [Route("Login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
    {

        var user = await _userManager.FindByEmailAsync(loginRequestDto.Username);
        if(user != null)
        {
            var checkPasswordResponse = await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);
            if (checkPasswordResponse)
            {
                //Get Roles for this user
                var roles = await _userManager.GetRolesAsync(user);
                if(roles !=  null)
                {
                    //Create token
                    var jwtToken = _tokenRepository.CreateJWTtoken(user, roles.ToList());
                    var response = new LoginResponseDTOs
                    {
                        JwtToken = jwtToken
                    };
                    return Ok(response);
                }
                

                return Ok();
            }
        }

        return BadRequest("Username or Password is incorrect");
    }
}

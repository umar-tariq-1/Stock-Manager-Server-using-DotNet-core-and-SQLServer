using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using server.Dtos.Account;
using server.Interfaces;
using server.Models;

namespace server.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController(UserManager<AppUser> userManager, ITokenService tokenService, SignInManager<AppUser> signInManager) : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager = userManager;
        private readonly ITokenService _tokenService = tokenService;
        private readonly SignInManager<AppUser> _signInManager = signInManager;

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var user = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == loginDto.UserName.ToLower());
                if (user == null)
                {
                    return Unauthorized(new { Message = "Invalid UserName or Password" });
                }

                var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
                if (!result.Succeeded)
                {
                    return Unauthorized(new { Message = "Invalid UserName or Password!" });
                }

                return Ok(new NewUserDto
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    Token = _tokenService.createToken(user)
                });
            }
            catch (Exception e)
            {
                return StatusCode(500, new { Message = "An error occurred while logging in", Error = e });
            }
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var appUser = new AppUser
                {
                    UserName = registerDto.UserName,
                    Email = registerDto.Email,
                };

                var createdUser = await _userManager.CreateAsync(appUser, registerDto.Password);

                if (createdUser.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(appUser, "User");
                    if (!roleResult.Succeeded)
                    {
                        return StatusCode(500, roleResult.Errors);
                    }

                    return Ok(new
                    {
                        Message = "User registered successfully",
                        UserName = appUser.UserName,
                        Email = appUser.Email,
                        Token = _tokenService.createToken(appUser)
                    });
                }

                return StatusCode(500, createdUser.Errors);

            }
            catch (Exception e)
            {
                return StatusCode(500, new { Message = "An error occurred while registering the user", Error = e });
            }
        }
    }
}
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PortHub.Api.Users.Data;
using PortHub.Api.Users.Data.Models;
using PortHub.Api.Users.Data.Models.ViewModel;
using PortHub.Api.Users.Data.Services;

namespace PortHub.Api.Users.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private IUserServices _userServices;


        public AuthenticationController(IUserServices service)
        {
            _userServices = service;

        }

        [HttpPost("register-user")]
        public async Task<IActionResult> Register([FromBody] RegisterVM registerVM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Please provide all the required Field");
            }

            return await _userServices.CreateUserAsync(registerVM);
        }

        [HttpPost("login-user")]

        public async Task<IActionResult> Login([FromBody] LoginVM loginVM) 
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Please provide all the required Field");
            }
            var result = await _userServices.LoginUserAsync(loginVM);
            if (result == null) { 
            return Unauthorized();
            }
            return Ok(result);


        }

       
    }
}

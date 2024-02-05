using Microsoft.AspNetCore.Mvc;
using PortHub.Api.Users.Data.Models.ViewModel;
using PortHub.Api.Users.Interface;

namespace PortHub.Api.Users.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserServices _userServices;
   

        public UserController(IUserServices service)
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

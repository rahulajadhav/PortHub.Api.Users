using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PortHub.Api.Users.Data;
using PortHub.Api.Users.Data.Models;
using PortHub.Api.Users.Data.Models.ViewModel;
using PortHub.Api.Users.Data.Models.viewModels;
using PortHub.Api.Users.Interface;

namespace PortHub.Api.Users.Services
{
    public class UserServices : IUserServices
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly appDbContext _appDbContext;
        private readonly RoleManager<IdentityRole> _roleManager;
        private IHttpClientFactory _httpClientFactory;


        public UserServices(UserManager<ApplicationUser> userManager,
                appDbContext appDbContext,
                RoleManager<IdentityRole> roleManager,
                IHttpClientFactory httpClientFactory
                )
        {
            _userManager = userManager;
            _appDbContext = appDbContext;
            _roleManager = roleManager;
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IActionResult> CreateUserAsync(RegisterVM newUsers)
        {

            var userExist = await _userManager.FindByEmailAsync(newUsers.Email);
            if (userExist != null)
            {
                return new BadRequestObjectResult("User Already exist");
            }

            ApplicationUser newUser = new ApplicationUser()
            {
                FirstName = newUsers.FirstName,
                LastName = newUsers.LastName,
                Email = newUsers.Email,
                UserName = newUsers.Username,
                SecurityStamp = Guid.NewGuid().ToString(),
            };
            var result = await _userManager.CreateAsync(newUser, newUsers.Password);
            if (result.Succeeded)
            {
                return new OkObjectResult("User registered");
            }
            return new BadRequestObjectResult(result.Errors);
        }

        public Task DeleteUserAsync(string emailId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ApplicationUser>> GetAllUserAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ApplicationUser> GetUserByEmailIdAsync(string emailId)
        {
            throw new NotImplementedException();
        }

        public async Task<IActionResult> LoginUserAsync(LoginVM newLogin)
        {
            var userExists = await _userManager.FindByNameAsync(newLogin.username);
            string relativeUri = $"/api/Token/refresh-login-user/";
            if (userExists != null && await _userManager.CheckPasswordAsync(userExists, newLogin.password))
            {

                var jsonContent = new StringContent(JsonSerializer.Serialize(userExists), Encoding.UTF8, "application/json");
                var client = _httpClientFactory.CreateClient("TokenGenetator");
                var response = await client.PostAsync(relativeUri, jsonContent);
                if (response.IsSuccessStatusCode)
                {
                    var rescontent = await response.Content.ReadAsByteArrayAsync();
                    var option = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                    var result = JsonSerializer.Deserialize<AuthVM>(rescontent, option);
                    return new OkObjectResult(result);

                }
                else
                {
                    return null;
                }

            }
            else
            {
                return null;
            }
        }



        public Task<ApplicationUser> UpdateUserAsync(ApplicationUser newUser)
        {
            throw new NotImplementedException();
        }

      

    }
}

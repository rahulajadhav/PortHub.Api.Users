using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PortHub.Api.Users.Data.Models;
using PortHub.Api.Users.Data.Models.ViewModel;

namespace PortHub.Api.Users.Data.Services
{
    public class UserServices : IUserServices
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly appDbContext _appDbContext;
        private readonly RoleManager<IdentityRole> _roleManager;
        
        

        public UserServices(UserManager<ApplicationUser> userManager,
                appDbContext appDbContext,
                RoleManager<IdentityRole> roleManager
                )
            {
                _userManager = userManager;
                _appDbContext = appDbContext;
                _roleManager = roleManager;  
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
            if (userExists != null && await _userManager.CheckPasswordAsync(userExists, newLogin.password)) 
            {
              return new OkObjectResult(userExists);

            }
            else { 
            return null;
            }
        }

        

        public Task<ApplicationUser> UpdateUserAsync(ApplicationUser newUser)
        {
            throw new NotImplementedException();
        }

        public async Task<IActionResult> RefreshTokenVM()
        {
            // var result = VerifyAndGenerateTokenAsync(VerifyToken);
            // return new OkObjectResult(result);
            throw new NotImplementedException();
        }

        /*private async Task<AuthVM> VerifyAndGenerateTokenAsync(RefreshTokenVM verifyToken)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var storedToken = await _appDbContext.RefreshTokens.FirstOrDefaultAsync(x => x.Token == verifyToken.RefreshToken);
            var dbUser = await _userManager.FindByIdAsync(storedToken.UserId);
            try
            {
                var tokenCheckResult = jwtTokenHandler.ValidateToken(verifyToken.Token, _tokenValidationParameters, out var validatedToken);
                return await GenerateJWTTokenAsync(dbUser, storedToken);

            }
            catch(SecurityTokenExpiredException) {
                if (storedToken.DateExpire >= DateTime.UtcNow)
                {
                   return await GenerateJWTTokenAsync(dbUser, storedToken);
                }
                else {
                    return await GenerateJWTTokenAsync(dbUser, null); ;
                }
            }
        }*/
    }
}

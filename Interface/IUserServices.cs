using Microsoft.AspNetCore.Mvc;
using PortHub.Api.Users.Data.Models;
using PortHub.Api.Users.Data.Models.ViewModel;

namespace PortHub.Api.Users.Interface
{
    public interface IUserServices
    {
        
        public Task<ApplicationUser> GetUserByEmailIdAsync(string emailId);
        public Task<IEnumerable<ApplicationUser>> GetAllUserAsync();
        public Task<IActionResult> CreateUserAsync(RegisterVM newUsers);
        public Task DeleteUserAsync(string emailId);

        public Task<ApplicationUser> UpdateUserAsync(ApplicationUser newUser);
        public Task<IActionResult> LoginUserAsync(LoginVM newLogin);
    }
}

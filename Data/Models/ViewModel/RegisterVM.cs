using System.ComponentModel.DataAnnotations;

namespace PortHub.Api.Users.Data.Models.ViewModel
{
    public class RegisterVM
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required] 
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}

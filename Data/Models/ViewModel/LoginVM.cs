using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace PortHub.Api.Users.Data.Models.ViewModel
{
    public class LoginVM
    {
        [Required]
        public string username { get; set; }
        [Required]
        public  string  password { get; set; }
    }
}

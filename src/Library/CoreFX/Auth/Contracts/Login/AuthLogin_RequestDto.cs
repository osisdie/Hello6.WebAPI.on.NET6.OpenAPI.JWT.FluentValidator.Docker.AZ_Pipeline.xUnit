using System.ComponentModel.DataAnnotations;

namespace CoreFX.Auth.Contracts.Login
{
    public class AuthLogin_RequestDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}

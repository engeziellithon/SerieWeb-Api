using System.ComponentModel.DataAnnotations;

namespace Udemy.Api.Dtos
{
    public class UserForRegisterDto
    {
        [Required]
        [StringLength(30, MinimumLength = 2 , ErrorMessage = "The username need be between 2 and 30 characters")]
        public string Username { get; set; }
        
        [Required]
        [StringLength(8, MinimumLength = 4, ErrorMessage = "The password need be between 4 and 8 characters")]
        public string Password { get; set; }   
    }
}
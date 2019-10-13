using System.ComponentModel.DataAnnotations;

namespace DatingApp1.API.Dtos
{
    public class UserForLoginDtos
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [StringLength(8, MinimumLength=4,ErrorMessage="Password length should be between 4 to 8 charecters")]
        public string Password{ get; set; }
    }
}
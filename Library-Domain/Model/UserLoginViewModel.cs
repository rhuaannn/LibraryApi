namespace Library_Domain.Model
{
    using System.ComponentModel.DataAnnotations;

        public class UserLoginViewModel
    {
                [Required]
        public string Email { get; set; } = string.Empty;

                [Required]
        public string Password { get; set; } = string.Empty;
    }
}

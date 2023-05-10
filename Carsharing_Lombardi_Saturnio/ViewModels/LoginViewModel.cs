using System.ComponentModel.DataAnnotations;

namespace Carsharing_Lombardi_Saturnio.ViewModels
{
    public class LoginViewModel
    {
        private string username;
        private string password;

        [Required(ErrorMessage = "The username is required!"), StringLength(20, MinimumLength = 4, ErrorMessage = "The username must be between 4 and 20 characters.")]
        public string Username { get => username; set => username = value; }
        [Required(ErrorMessage = "The password is required!")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*).{6,30}$", ErrorMessage = "The password must be between 6 and 30 characters, have one capital letter and one number.")]
        public string Password { get => password; set => password = value; }
    }
}

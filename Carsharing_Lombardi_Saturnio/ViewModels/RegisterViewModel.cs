using System.ComponentModel.DataAnnotations;

namespace Carsharing_Lombardi_Saturnio.ViewModels
{
    public class RegisterViewModel
    {
        private string username;
        private string password;
        private string confirmpassword;
        private string first_name;
        private string last_name;
        private int phone_number;

        [Display(Name = "Username")]
        [Required(ErrorMessage = "The username is required!"), StringLength(20, MinimumLength = 4, ErrorMessage = "The username must be between 4 and 20 characters.")]
        public string Username { get => username; set => username = value; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "The password is required!")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*).{6,30}$", ErrorMessage = "The password must be between 6 and 30 characters, have one capital letter and one number.")]
        public string Password { get => password; set => password = value; }

        [Display(Name = "Confirm Password")]
        [Required(ErrorMessage = "You need to confirm the password!")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*).{6,30}$", ErrorMessage = "The password must be between 6 and 30 characters, have one capital letter and one number.")]
        [Compare("Password",ErrorMessage = "The confirm password doesn't match the password!")]
        public string ConfirmPassword { get => confirmpassword; set => confirmpassword = value; }


        [Display(Name = "Firstname")]
        [Required(ErrorMessage = "The firstname is required!"), StringLength(20, MinimumLength = 2, ErrorMessage = "The firstname must be between 2 and 50 characters")]
        public string First_Name { get => first_name; set => first_name = value; }

        [Display(Name = "Lastname")]
        [Required(ErrorMessage = "The lastname is required!"), StringLength(20, MinimumLength = 2, ErrorMessage = "The lastname must be between 2 and 50 characters")]
        public string Last_Name { get => last_name; set => last_name = value; }

        [Display(Name = "Phone Number")]
        [Required(ErrorMessage =" The phone number is required!")]
        //
        //[RegularExpression(@"^\\+?[1-9][0-9]{7,14}$", ErrorMessage = "The phone number is incorrect.")]
        public int Phone_number { get => phone_number; set => phone_number = value; }
    }
}

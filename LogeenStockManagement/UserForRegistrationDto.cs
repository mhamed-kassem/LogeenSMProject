using System.ComponentModel.DataAnnotations;

namespace LogeenStockManagement
{
    public class UserForRegistrationDto
    {
        [Required(ErrorMessage = "Employee ID is required.")]
        public int EmployeeId { get; set; }


        [Required(ErrorMessage = "Email is required.")]
        public string Email { get; set; } //username


        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        public string Role { get; set; }

        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}

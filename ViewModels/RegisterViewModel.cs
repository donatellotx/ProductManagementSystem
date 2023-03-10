using System.ComponentModel.DataAnnotations;

namespace ProductManagementSystem.ViewModels
{
    public class RegisterViewModel : LoginViewModel
    {
        [Required(ErrorMessage = "Please enter first name")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Please enter last name")]
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public int PhoneNumber { get; set; }
    }
}

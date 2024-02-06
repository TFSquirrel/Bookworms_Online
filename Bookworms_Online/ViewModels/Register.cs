using System.ComponentModel.DataAnnotations;

namespace Bookworms_Online.ViewModels
{
    public class Register
    {
        [Required(ErrorMessage = "First Name is required")]
        [StringLength(50, ErrorMessage = "First Name cannot exceed 50 characters")]
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Invalid Name Format")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        [StringLength(50, ErrorMessage = "Last Name cannot exceed 50 characters")]
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Invalid Name Format")]

        public string LastName { get; set; }

        [Required(ErrorMessage = "Shipping address is required")]
        [StringLength(100, ErrorMessage = "Shipping address cannot exceed 100 characters")]

        public string Shipping { get; set; }

        [Required(ErrorMessage = "Billing address is required")]
        [StringLength(100, ErrorMessage = "Billing address cannot exceed 100 characters")]
        public string Billing { get; set; }

        [Required(ErrorMessage = "Mobile Number is required")]
        [RegularExpression(@"^[0-9+]{8,11}$", ErrorMessage = "Invalid mobile number format.")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Invalid Mobile Number")]
        public string MobileNo { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Credit Info is required")]
        [RegularExpression(@"^\d{10,19}$", ErrorMessage = "Invalid Credit Card Format")]
        public string CreditCard { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [StringLength(50, MinimumLength = 12, ErrorMessage = "Password must be at least 12 characters long")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is required")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Password and confirmation password do not match")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "File is required")]
        [DataType(DataType.Upload)]
        public IFormFile File { get; set; }

    }
}

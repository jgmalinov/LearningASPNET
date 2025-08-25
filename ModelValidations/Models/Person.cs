using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ModelValidations.CustomValidationAttributes;

namespace ModelValidations.Models
{
    public class Person
    {
        [DisplayName("Person's First Name")]
        [Required(ErrorMessage = "REQUIRED")]
        [StringLength(30, ErrorMessage = "{0} can't be over {1} characters or less than {2}", MinimumLength = 5)]
        public string? Name { get; set; }

        [Required]
        [MinimumYearOfBirth(2005, ErrorMessage = "Custom Error Message - YOB must be earlier than {0}")]
        public DateTime DateOfBirth { get; set; }

        [EmailAddress(ErrorMessage = "{0} must be a valid email address format")]
        public string? Email { get; set; }

        [RegularExpression("^[A-Za-z0-9]$", ErrorMessage = "Address cannot contain non-alphanumerical characters")]
        public string? Address { get; set; }

        [Phone(ErrorMessage = "{0} is not a valid phone number")]
        public string? PhoneNumber { get; set; }
        
        [MinLength(10)]
        [Required(ErrorMessage = "{0} cannot be an empty value")]
        public string? Password { get; set; }

        [DisplayName("Password confirm")]
        [Compare("Password", ErrorMessage = "{0} and {1} do not match")]
        public string? ConfirmPassword { get; set; }
        
        [DisplayName("Net Worth (In Thousands)")]
        [Range(1, 1000, ErrorMessage = "{0} cannot be less than {1} thousand or above {2} thousand USD")]
        public double? NetWorthInThousands { get; set; }

        public DateTime StartDate { get; set; }

        [DateRange("StartDate", ErrorMessage = "CUSTOM - {1} cannot be later than {0}")]
        public DateTime EndDate { get; set; }

        public override string ToString()
        {
            return $"Name: {Name}\nEmail: {Email}\nAddress: {Address}\nPassword: {Password}\n" +
                $"Confirm Password: {ConfirmPassword}\nNet Worth: {NetWorthInThousands}k\n";
        }
    }
}

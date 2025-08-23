using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ModelValidations.Models
{
    public class Person
    {
        [DisplayName("Person's First Name")]
        [Required(ErrorMessage = "REQUIRED")]
        [StringLength(30, ErrorMessage = "{0} can't be over {1} characters or less than {2}", MinimumLength = 5)]
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        [MinLength(10)]
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
        [DisplayName("Net Worth (In Thousands)")]
        [Range(1, 1000, ErrorMessage = "{0} cannot be less than {1} thousand or above {2} thousand USD")]
        public double? NetWorthInThousands { get; set; }

        public override string ToString()
        {
            return $"Name: {Name}\nEmail: {Email}\nAddress: {Address}\nPassword: {Password}\n" +
                $"Confirm Password: {ConfirmPassword}\nNet Worth: {NetWorthInThousands}k\n";
        }
    }
}

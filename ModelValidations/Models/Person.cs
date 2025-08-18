using System.ComponentModel.DataAnnotations;

namespace ModelValidations.Models
{
    public class Person
    {
        [Required]
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
        public double? NetWorthInThousands { get; set; }

        public override string ToString()
        {
            return $"Name: {Name}\nEmail: {Email}\nAddress: {Address}\nPassword: {Password}\n" +
                $"Confirm Password: {ConfirmPassword}\nNet Worth: {NetWorthInThousands}k\n";
        }
    }
}

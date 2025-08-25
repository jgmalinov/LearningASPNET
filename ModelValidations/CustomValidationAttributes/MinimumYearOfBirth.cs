using System.ComponentModel.DataAnnotations;

namespace ModelValidations.CustomValidationAttributes
{
    public class MinimumYearOfBirth: ValidationAttribute
    {
        public MinimumYearOfBirth() { }
        public MinimumYearOfBirth(int minimumYear)
        {
            this.MinimumYear = minimumYear;
        }
        public int MinimumYear { get; set; } = 2000;
        public string DefaultErrorMessage { get; set; } = "Default Error Message - Min Year is {0}";
        protected override ValidationResult? IsValid(object? value, ValidationContext context)
        {
            if (value == null)
            {
                return null;
            }
            DateTime date = (DateTime)value;
            
            if (date.Year >= MinimumYear)
            {
                return new ValidationResult(String.Format(ErrorMessage ?? DefaultErrorMessage, MinimumYear));
            }
            else
            {
                var result = ValidationResult.Success;
                return result;
            }
        }
    }
}

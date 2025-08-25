using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace ModelValidations.CustomValidationAttributes
{
    public class DateRange: ValidationAttribute
    {
        public string DefaultErrorMessage { get; set; } = "{1} cannot be later than {0}";
        public string StartDatePropertyName { get; set; } = "StartDate";
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateRange(string startDatePropertyName)
        {
            StartDatePropertyName = startDatePropertyName;
        }
        public DateRange() { }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return null;
            }
            EndDate = (DateTime)value;

            PropertyInfo? StartDatePropertyInfo = validationContext.ObjectType.GetProperty(StartDatePropertyName);
            StartDate = Convert.ToDateTime(StartDatePropertyInfo?.GetValue(validationContext.ObjectInstance));
            if (StartDate == null)
            {
                return null;
            }

            if (StartDate > EndDate)
            {
                return new ValidationResult(String.Format(ErrorMessage ?? DefaultErrorMessage, validationContext.MemberName, StartDatePropertyName), new List<string> { validationContext.MemberName, StartDatePropertyName});
            } else
            {
                return ValidationResult.Success;
            }
        }
    }
}

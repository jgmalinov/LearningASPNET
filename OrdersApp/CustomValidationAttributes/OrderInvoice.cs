using OrdersApp.Models;
using System.ComponentModel.DataAnnotations;

namespace OrdersApp.CustomValidationAttributes
{
    public class OrderInvoice: ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return null;
            }
            Order order = (Order)validationContext.ObjectInstance;
            double invoice = 0;

            foreach (Product product in order.Products)
            {
                invoice += product.Price * product.Quantity;
            }

            if (invoice != order.InvoicePrice)
            {
                return new ValidationResult($"Invoice Price {order.InvoicePrice} does not match the calculated invoice {invoice}", new List<string> { validationContext.MemberName });
            }
            else
            {
                return ValidationResult.Success;
            }
        }
    }
}

using System.ComponentModel.DataAnnotations;
using OrdersApp.CustomValidationAttributes;

namespace OrdersApp.Models
{
    public class Order
    {
        public int? OrderNo { get; set; }
        
        [Required]
        public DateTime OrderDate { get; set; }
        
        [Required]
        [OrderInvoice]
        public double InvoicePrice { get; set; }
        
        [Required]
        [MinLength(1)]
        public List<Product> Products { get; set; }

        public void GenerateOrderNo()
        {
            Random random = new Random();
            OrderNo = random.Next(1, 99999);
        }
    }
}

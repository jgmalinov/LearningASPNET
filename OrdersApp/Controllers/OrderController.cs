using Microsoft.AspNetCore.Mvc;
using OrdersApp.Models;

namespace OrdersApp.Controllers
{
    public class OrderController : Controller
    {
        [Route("Order")]
        public IActionResult Index([Bind(nameof(Order.Products), nameof(Order.InvoicePrice), 
            nameof(Order.OrderDate))] Order order)
        {
            if (ModelState.IsValid)
            {
                order.GenerateOrderNo();
                return Json(order.OrderNo);
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                //return BadRequest(errors);
                return BadRequest(ModelState);
            }
        }
    }
}

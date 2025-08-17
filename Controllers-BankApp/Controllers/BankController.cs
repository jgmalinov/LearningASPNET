using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Controllers_BankApp.Controllers
{
    public class BankController : Controller
    {
        public object Account { get; set; } = new
        {
            accountNumber = 1001,
            accountHolderName = "Georgi Milanov",
            currentBalance = 5000
        };
        
        [Route("Bank")]
        public IActionResult Index()
        {
            return Content("Welcome to The Best Bank!", "text/plain");
        }

        [Route("Bank/Account-Details")]
        public IActionResult AccountDetails()
        {
            //return Content(Account, "application/json");
            //return new ObjectResult(Account);
            return Json(Account);
        }

        [Route("Bank/Account-Statement")]
        public IActionResult AccountStatement()
        {
            return File("/dummy_bank_statement.pdf", "application/pdf");
        }

        [Route("Bank/get-current-balance/{accountNumber:int?}")]
        public IActionResult GetBalance()
        {
            object? accountNumberObj = Request.RouteValues["accountNumber"];
            if (accountNumberObj is null)
            {
                return NotFound("Account Number should be supplied");
            }
            int accountNumber = Convert.ToInt32(accountNumberObj);

            if (accountNumber != 1001)
            {
                return BadRequest("Account Number should be 1001");
            }
            return Content("5000");
        }
    }
}

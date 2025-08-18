using Microsoft.AspNetCore.Mvc;
using ModelValidations.Models;

namespace ModelValidations
{
    [Route("")]
    public class HomeController : Controller
    {
        public IActionResult Index(Person person)
        {
            return Json(person);
        }
    }
}

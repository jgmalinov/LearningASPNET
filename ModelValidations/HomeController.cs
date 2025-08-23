using Microsoft.AspNetCore.Mvc;
using ModelValidations.Models;

namespace ModelValidations
{
    public class HomeController : Controller
    {
        [Route("")]
        public IActionResult Index(Person person)
        {
            if (ModelState.IsValid == false)
            {
                string errors = String.Join("\n", ModelState.Values
                    .SelectMany(value => value.Errors)
                    .Select(error => error.ErrorMessage)
                    .ToList());
                
                return BadRequest(errors);
            }
            return Json(person);
        }
    }
}

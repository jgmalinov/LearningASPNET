using Microsoft.AspNetCore.Mvc;
using RazorViews.Models;

namespace RazorViews.Controllers
{
    public class HomeController : Controller
    {
        [Route("Home")]
        [Route("")]
        public IActionResult Index()
        {
            return View();
            // return View("AlternativeViewName");
            // return new ViewResult { ViewName = "AlternativeViewName" };
        }

        [Route("Proper")]
        public IActionResult Proper()
        {
            List<Person> people = new List<Person>()
            {
                new Person() { Id = 1, Name = "Alice", Gender=Gender.Female, DateOfBirth = Convert.ToDateTime("2000-12-12")},
                new Person() { Id = 2, Name = "Bob", Gender=Gender.Male, DateOfBirth = Convert.ToDateTime("1956-12-24")},
                new Person() { Id = 3, Name = "Charlie", Gender=Gender.Male }
            };
            ViewData["Title"] = "Proper View";
            ViewData["People"] = people;

            return View();
        }
    }
}

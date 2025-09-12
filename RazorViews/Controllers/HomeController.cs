using Microsoft.AspNetCore.Mvc;
using RazorViews.Models;

namespace RazorViews.Controllers
{
    public class HomeController : Controller
    {
        List<Person> People { get; set; } = new List<Person>()
        {
                new Person() { Id = 1, Name = "Alice", Gender=Gender.Female, DateOfBirth = Convert.ToDateTime("2000-12-12")},
                new Person() { Id = 2, Name = "Bob", Gender=Gender.Male, DateOfBirth = Convert.ToDateTime("1956-12-24")},
                new Person() { Id = 3, Name = "Charlie", Gender=Gender.Male }
        };

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

            ViewData["Title"] = "Proper View";
            //ViewBag.People = people;
            ViewData["People"] = People;


            return View();
        }

        [Route("StronglyTyped")]
        public IActionResult StronglyTyped()
        {
            return View(People);
        }

        [Route("Person/{name:alpha}")]
        public IActionResult Person(string name)
        {
            var person = People.FirstOrDefault(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            if (person == null)
            {
                return NotFound();
            }
            return View(person);
        }
    }
}

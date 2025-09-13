using Microsoft.AspNetCore.Mvc;
using RazorViews.Models;

namespace RazorViews.Controllers
{
    public class HomeController : Controller
    {
        List<Person> People { get; set; } = new List<Person>()
        {
                new Person() { Id = 1, Name = "Jimi Hendrix", Gender=Gender.Female, DateOfBirth = Convert.ToDateTime("2000-12-12")},
                new Person() { Id = 2, Name = "Dave Grohl", Gender=Gender.Male, DateOfBirth = Convert.ToDateTime("1956-12-24")},
                new Person() { Id = 3, Name = "Charlie", Gender=Gender.Male }
        };

        List<Instrument> Instruments { get; set; } = new List<Instrument>()
        {
                new Instrument() { Id = 1, PersonId=1, Name = "Carol/Linda", Type = InstrumentType.String, Brand="Fender Stratocaster", Price=2000000},
                new Instrument() { Id = 2, PersonId=2, Name = "Drums", Type = InstrumentType.Percussion, Brand="Yamaha", Price=1500000},
                new Instrument() { Id = 3, PersonId=3, Name = "Flute", Type = InstrumentType.Wind, Brand="Pearl", Price=500000}
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

        [Route("Person/{name}/Instrument")]
        public IActionResult Instrument(string name)
        {
            var sanitizedName = name.Replace("-", " ").Replace("_", " ");
            var person = People.FirstOrDefault(p => p.Name.Equals(sanitizedName, StringComparison.OrdinalIgnoreCase));
            var instrument = Instruments.FirstOrDefault(i => i.PersonId == person.Id);
            if (person == null || instrument == null)
            {
                return NotFound();
            }
            var viewModel = new InstrumentPersonViewModel { Instrument = instrument, Person = person };
            return View(viewModel);
        }

    }
}

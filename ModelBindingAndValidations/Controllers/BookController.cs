using Microsoft.AspNetCore.Mvc;
using ModelBinding.Models;

namespace ModelBinding.Controllers
{
    public class BookController : Controller
    {
        Dictionary<int, string> books = new Dictionary<int, string>()
        {
            { 1, "The Silent River" },
            { 2, "Dreams of Tomorrow" },
            { 3, "Whispers in the Wind" },
            { 4, "The Iron Fortress" },
            { 5, "Shadows of the Past" },
            { 6, "Journey to the Stars" },
            { 7, "Echoes of Eternity" },
            { 8, "The Last Harvest" },
            { 9, "Beyond the Horizon" },
            { 10, "Crimson Skies" }
        };

        [Route("book/{id:int?}/{isloggedin:bool?}")]
        public IActionResult Index([FromRoute] int? id, [FromQuery] bool? isloggedin, Book? book)
        {
            if (id == null)
            {
                return BadRequest("No book id passed");
            }
            if (id < 1 || id > books.Keys.Count)
            {
                return NotFound("Invalid book id");
            }
            if (isloggedin == null)
            {
                return BadRequest("Authentication state undefined");
            }
            if (isloggedin is false)
            {
                return Unauthorized();
            }

            return Content($"{book}", "text/plain");
        }
    }
}

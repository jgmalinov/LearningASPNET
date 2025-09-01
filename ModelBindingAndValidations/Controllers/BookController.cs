using Microsoft.AspNetCore.Mvc;
using ModelBinding.CustomModelBinders;
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
        public IActionResult Index([FromRoute] int? id, [FromQuery] bool? isloggedin,
            //Customize default binding behavior
            /*[Bind(nameof(Book.Name), nameof(Book.Author))]
            
            // Or use a custom model binder
            [ModelBinder(BinderType = typeof(BookModelBinder))]*/

            // Or use a custom model binder provider

            // To Bind to a collection parameter/property, index its multiple entries into the form client-side
            // E.g. Tags[0], Tags[1] etc. for a List<string> Tags property.
            Book? book, List<string?> Tags, [FromHeader(Name = "User-Agent")] string UserAgent)
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

            return Content($"{book}, Client: {UserAgent}", "text/plain");
        }

        [Route("/json/{id:int}")]
        // FromBody resolves to BodyModelBinder which invokes the configured input formatters to parse the request body
        public IActionResult JsonBookRequest([FromBody] Book book)
        {
            return Content($"{book}", "text/plain");
        }
    }
}

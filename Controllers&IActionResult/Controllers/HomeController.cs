using Microsoft.AspNetCore.Mvc;
using Controllers_IActionResult.Models;
using System.IO;

namespace Controllers_IActionResult.Controllers
{
    public class HomeController: Controller
    {
        [Route("Index")]
        [Route("/")]
        public ContentResult Index()
        {
            //return new ContentResult
            //{
            //    Content = "This is the Index page.",
            //    ContentType = "text/plain",
            //    StatusCode = 200
            //};

            //return Content("This is the Index page", "text/plain");
            return Content("<h1>This is the Index page<h1>", "text/html");
        }

        [Route("About")]
        public string About()
        {
            return "This is the About page.";
        }

        [Route("Contact/{mobile:regex(^\\d{{10}}$)?}")]
        public string Contact()
        {
            return "This is the Contact page.";
        }

        [Route("Person")]
        public JsonResult Person()
        {
            Person person = new Person
            {
                Id = Guid.NewGuid(),
                FirstName = "John",
                LastName = "Doe",
                DateOfBirth = new DateTime(1990, 1, 1)
            };
            return Json(person);
            //return new JsonResult(person)
            //{
            //    StatusCode = 200,
            //    ContentType = "application/json"
            //};
        }

        // Used for fetching file content from files in wwwroot or the WebRoot folder
        [Route("FileDownloadRelative")]
        public VirtualFileResult FileDownload()
        {
            //return new VirtualFileResult("/ExampleRelative.txt", "text/plain");
            return File("/ExampleRelative.txt", "text/plain");
        }
        // Used for fetching file content from files outside of wwwroot or the WebRoot folder
        [Route("FileDownloadAbsolute")]
        public PhysicalFileResult FileDownloadAbsolute()
        {
            //return new PhysicalFileResult("C:\\Users\\gogot\\source\\repos\\jgmalinov\\LearningASPNET\\Controllers&IActionResult\\ExampleAbsolute.txt", "text/plain");
            return PhysicalFile("C:\\Users\\gogot\\source\\repos\\jgmalinov\\LearningASPNET\\Controllers&IActionResult\\ExampleAbsolute.txt", "text/plain");
        }
        // Used for fetching file content as bytes, e.g. from remote sources like databases
        [Route("FileDownloadBytes")]
        public FileContentResult FileDownloadBytes()
        {
            byte[] bytes = System.IO.File.ReadAllBytes("C:\\Users\\gogot\\source\\repos\\jgmalinov\\LearningASPNET\\Controllers&IActionResult\\ExampleAbsolute.txt");
            //return new FileContentResult(bytes, "text/plain");
            return File(bytes, "text/plain");
        }
    }
}

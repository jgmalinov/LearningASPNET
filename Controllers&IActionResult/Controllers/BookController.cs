using Microsoft.AspNetCore.Mvc;

namespace Controllers_IActionResult.Controllers
{
    public class BookController : Controller
    {
        [Route("Book")]
        public IActionResult Index()
        {
            if (!Request.Query.ContainsKey("id"))
            {
                Response.StatusCode = 400;
                return Content("Id is not passed as a query parameter");
            }
            if (string.IsNullOrEmpty(Convert.ToString(Request.Query["id"])))
            {
                Response.StatusCode = 400;
                return Content("Id value is empty or null");
            }
            int id = Convert.ToInt32(Request.Query["id"]);
            if (id < 0 || id > 1000)
            {
                //Response.StatusCode = 404;
                //return Content("Id must be in the range of 1-1000");

                // Output formatter will set this to text/plain:
                //return NotFound("Id must be in the range of 1-1000");
                

                // This will be formatted to JSON. ObjectResult goes through output formatting, ContentResult 
                // does not. Format needs to be set. 
                object responseObj = new { msg = "Id must be in the range of 1-1000" };
                return NotFound(responseObj);
            }
            if (!Request.Query.ContainsKey("authenticated") || 
                string.IsNullOrEmpty(Convert.ToString(Request.Query["authenticated"])))
            {
                Response.StatusCode = 400;
                return Content("Invalid authentication parameter");
            }
            bool isAuthenticated = Convert.ToBoolean(Request.Query["authenticated"]);
            if (!isAuthenticated)
            {
                //Response.StatusCode = 401;
                //return Content("401 - Unauthorized");
                //return StatusCode(401);
                return Unauthorized("Please go through the authentication process to gain access to this resource");
            }

            // TEMPORARY REDIRECT - 302 - FOUND

            // I. Passing Controller/Action names

            // Result object
            // return new RedirectToActionResult("Book", "Store",  new { id = id }, false);

            // Controller class method
            // return RedirectToAction("Book", "Store", new { id = id });

            // II. Passing URL

            // Local URL
            // return LocalRedirect($"/store/book/{id}");

            // Remote or local URL
            // return Redirect($"/store/book/{id}");

            // PERMANENT REDIRECT - 301 - MOVED PERMANENTLY

            // I. Passing Controller/Action names

            // Result Object
            // return new RedirectToActionResult("Book", "Store", new { id = id }, true);

            // Controller class method
            // return RedirectToActionPermanent("Book", "Store", new { id = id });

            // II. Passing URL

            // Local URL
            // return LocalRedirectPermanent($"/store/book/{id}");

            // Remote or Local URL
            return RedirectPermanent($"/store/book/{id}");
        }
    }
}

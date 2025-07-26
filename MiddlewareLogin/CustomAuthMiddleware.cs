using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;

namespace MiddlewareLogin
{
    public class CustomAuthMiddleware
    {
        private readonly RequestDelegate _next;
        public CustomAuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            var requestMethod = context.Request.Method;

            if (requestMethod == "GET")
            {
                context.Response.StatusCode = 200;
            }
            else if (requestMethod == "POST")
            {
                List<string> errorMessages = new List<string>();
                string? password, username;
                
                if (context.Request.Query.Count > 0)
                {
                    username = context.Request.Query["username"];
                    password = context.Request.Query["password"];
                } else
                {
                    StreamReader reader = new StreamReader(context.Request.Body);
                    string body = await reader.ReadToEndAsync();



                    Dictionary<string, StringValues> queryDict = QueryHelpers.ParseQuery(body);

                    username = queryDict.ContainsKey("username") ? queryDict["username"][0].ToString() : null;
                    password = queryDict.ContainsKey("password") ? queryDict["password"][0].ToString() : null;
                }

                bool usernameIsValid = true;
                bool passwordIsValid = true;

                if (username == null)
                {
                    errorMessages.Add("Invalid input for username");
                }
                else if (username != "admin@example.com")
                {
                    usernameIsValid = false;
                }

                if (password == null)
                {
                    errorMessages.Add("Invalid input for password");
                }
                else if (password != "admin1234")
                {
                    passwordIsValid = false;
                }

                if (errorMessages.Count == 0 && (!passwordIsValid || !usernameIsValid))
                {
                    errorMessages.Add("Invalid Login");
                }

                if (errorMessages.Count > 0)
                {
                    context.Response.StatusCode = 400; // Unauthorized
                    await context.Response.WriteAsync(string.Join("\n", errorMessages));
                    return;
                }
                else
                {
                    context.Response.StatusCode = 200; // OK
                    await context.Response.WriteAsync("Login successful");
                    return;
                } // Call the next middleware in the pipeline
            }
        }
    }

    public static class  CustomAuthMiddlewareExtension
    {
        public static void UseCustomAuthMiddleware(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<CustomAuthMiddleware>();
        }
    }
}

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
                string[] errorMessages = new string[] { };
                string? username = context.Request.Query["username"];
                string? password = context.Request.Query["password"];
                bool usernameIsValid = true;
                bool passwordIsValid = true;


                if (context.Request.Method == "GET")
                {
                    context.Response.StatusCode = 200;
                }
                else if (context.Request.Method == "POST")
                {
                    if (username == null)
                    {
                        errorMessages.Append("Invalid input for username");
                    }
                    else if (username != "admin@example.com")
                    {
                        usernameIsValid = false;
                    }

                    if (password == null)
                    {
                        errorMessages.Append("Invalid input for password");
                    }
                    else if (password != "admin1234")
                    {
                        passwordIsValid = false;
                    }

                    if (errorMessages.Length == 0 && (!passwordIsValid || !usernameIsValid))
                    {
                        errorMessages.Append("Invalid Login");
                    }

                    if (errorMessages.Length > 0)
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
                    }
                }
            }
        }
    }
}

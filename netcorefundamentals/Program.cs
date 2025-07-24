using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Middleware, all HTTP requests are piped through it coming from Kestrel
app.Run(async (HttpContext context) =>
{
    // Grabbing metadata about the HTTP transaction parsed as an HttpContext
    string requestMethod = context.Request.Method;
    string requestPath = context.Request.Path;
    string userAgent = context.Request.Headers["User-Agent"];
    string contentType = context.Request.ContentType;
    long contentLength = context.Request.ContentLength ?? 0;
    string id = string.Empty;
    var name = string.Empty;
    // Obtaining and parsing request body

    if (contentLength > 0)
    {
        using var reader = new StreamReader(context.Request.Body);
        string requestBody = await reader.ReadToEndAsync();

        if (contentType == "text")
        {
            Dictionary<string, StringValues> queryParsedBody = QueryHelpers.ParseQuery(requestBody);
            queryParsedBody.TryGetValue("id", out StringValues nameValue);
            name = nameValue[0] ?? String.Empty;
        }
        else if (contentType == "application/json")
        {
            Person person = JsonSerializer.Deserialize<Person>(requestBody);
            name = person?.Name ?? string.Empty;
            // Handle JSON parsing here if needed
            // For example, using System.Text.Json or Newtonsoft.Json
            // var jsonData = JsonSerializer.Deserialize<YourType>(requestBody);
        }
        else
        {
            await context.Response.WriteAsync("Unsupported content type for request body parsing.");
            return;
        }
    }

    // Assigning Response Headers
    context.Response.Headers["MyKey"] = "MyValue";
    context.Response.Headers["Server"] = "MyKestrel";
    context.Response.Headers["Content-Type"] = "text/html";


    // Writing Response Body
    await context.Response.WriteAsync("<html><body><h1>Hello, World!</h1>");
    await context.Response.WriteAsync($"<p>Request Method: {requestMethod}</p>");
    await context.Response.WriteAsync($"<p>Request Path: {requestPath}</p></body></html>");
    await context.Response.WriteAsync($"<p>User Agent: {userAgent}</p>");

    if (context.Request.Query.ContainsKey("id") && requestMethod == "GET")
    {
        id = context.Request.Query["id"];
        await context.Response.WriteAsync($"<p>Id obtained from query string: {id}</p>");
    }
    else
    {
        await context.Response.WriteAsync($"<p>No Id in query params or request method is not GET");
    }

    if (name != String.Empty && requestMethod == "POST")
    {
        await context.Response.WriteAsync($"<p>Name obtained from request body: {name}</p>");
    }
    else if (name == String.Empty && requestMethod == "POST")
    {
        await context.Response.WriteAsync("<p>No Name in request body</p>");
    }
});

app.Run();

public class Person
{
    public string Name { get; set; }
    public int Age { get; set; }
}

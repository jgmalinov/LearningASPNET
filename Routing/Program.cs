var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// It is only after the UseRouting call that endpoints become recognized
app.UseRouting();

app.Use(async (context, next) =>
{
    Endpoint? endpoint = context.GetEndpoint();
    if (endpoint != null)
    {
        // Log the endpoint name
        context.Response.WriteAsync($"Endpoint: {endpoint.DisplayName}");
    }
    else
    {
        // Log that no endpoint was found
        context.Response.WriteAsync("No endpoint found for this request.");
    }

    // Call the next middleware in the pipeline
    await next.Invoke();
});


app.UseEndpoints(endpoints =>
{
    endpoints.MapGet("/hello", (HttpContext context) => context.Response.WriteAsync("Hello from /hello endpoint!"));
    endpoints.MapGet("/goodbye", (HttpContext context) => context.Response.WriteAsync("Goodbye from /goodbye endpoint!"));
    endpoints.Map("/employee/profile/{name}", (HttpContext context) => context.Response.WriteAsync($"Hi,{context.Request.RouteValues["name"]}"));
    endpoints.Map("/files/{filename}.{extension}", async (HttpContext context) =>
    {
        string? filename = context.Request.RouteValues["filename"]?.ToString();
        string? extension = context.Request.RouteValues["extension"]?.ToString();
        if (filename != null && extension != null)
        {
            await context.Response.WriteAsync($"File requested: {filename}.{extension}");
        }
        else
        {
            await context.Response.WriteAsync("Invalid file request");
        }
    });
    endpoints.Map("/court/{defendant=Ivan}-{plaintiff=Simeon}", (HttpContext context) =>
    {
        string? defendant = context.Request.RouteValues["defendant"]?.ToString();
        string? plaintiff = context.Request.RouteValues["plaintiff"]?.ToString();
        if (defendant != null && plaintiff != null)
        {
            return context.Response.WriteAsync($"Court case between {defendant} and {plaintiff}");
        }
        else
        {
            return context.Response.WriteAsync("Invalid court case request");
        }
    });
});

app.Run(async (HttpContext context) =>
{
    await context.Response.WriteAsync("No other route matched");
});

app.Run();

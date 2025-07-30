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
        context.Response.WriteAsync($"Endpoint: {endpoint.DisplayName}\n ");
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
    endpoints.MapGet("/optional/{id?}", (HttpContext context) =>
    {
        int id = Convert.ToInt32(context.Request.RouteValues["id"]);
        return context.Response.WriteAsync($"Optional ID: {id}");
    });
});

// Map a route with a DateTime parameter
app.Map("/dates/{date:datetime}", (HttpContext context) =>
{
    DateTime date = Convert.ToDateTime(context.Request.RouteValues["date"]);
    return context.Response.WriteAsync($"Date requested: {date.ToShortDateString()}");
});

// Map a route with a GUID parameter
app.Map("/guid/{id:guid}", (HttpContext context) =>
{
    Guid id = Guid.Parse(context.Request.RouteValues["id"].ToString()!);
    return context.Response.WriteAsync($"GUID requested: {id}");
});

app.Map("/username/{username:alpha:minlength(3):maxlength(5)}", (HttpContext context) => 
{
    string? username = context.Request.RouteValues["username"]?.ToString()!;
    if (username != null)
    {
        return context.Response.WriteAsync($"Username requested: {username}");
    }
    else
    {
        return context.Response.WriteAsync("Invalid username request");
    }
});

app.Map("/age/{age:int:range(18,100)}", (HttpContext context) =>
{
    //int age = Convert.ToInt32(context.Request.RouteValues["age"]);
    float age = float.Parse(context.Request.RouteValues["age"]?.ToString()!);
    return context.Response.WriteAsync($"Age requested: {age}");
});

app.Run(async (HttpContext context) =>
{
    await context.Response.WriteAsync("No other route matched");
});

app.Run();

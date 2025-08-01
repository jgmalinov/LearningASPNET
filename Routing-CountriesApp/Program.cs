var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var countries = new Dictionary<int, string>
{
    {1, "United States"},
    {2, "Canada"},
    {3, "United Kingdom"},
    {4, "India"},
    {5, "Japan" }
};

app.UseRouting();
app.MapGet("/countries", async (HttpContext context) =>
{
    var response = string.Join("\n", countries.Select(c => $"{c.Key}, {c.Value}"));
    await context.Response.WriteAsync(response);
});

app.MapGet("/countries/{id:int:range(1,100)}", async (HttpContext context) =>
{
    int countryId = Convert.ToInt32(context.Request.RouteValues["id"]?.ToString()!);
    if (countryId > countries.Count)
    {
        context.Response.StatusCode = 404;
    }
    else
    {
        string country = countries[countryId];
        await context.Response.WriteAsync($"{country}");
    }
});

app.MapFallback(async (HttpContext context) =>
{
    context.Response.StatusCode = 400;
    await context.Response.WriteAsync("The CountryID should be between 1 and 100");
});

app.Run();

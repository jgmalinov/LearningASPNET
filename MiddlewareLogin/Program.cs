using MiddlewareLogin;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseCustomAuthMiddleware();

app.Run();

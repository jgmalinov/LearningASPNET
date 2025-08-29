using ModelBinding.CustomModelBinders;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers(options =>
{
    options.ModelBinderProviders.Insert(0, new BookModelBinderProvider());
});
var app = builder.Build();

app.MapControllers();
app.MapGet("/", () => "Hello World!");

app.Run();

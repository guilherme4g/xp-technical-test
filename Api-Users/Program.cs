using Api_Users.Utils;
using Api_Users.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("ServerSettings"));

builder.Services.AddSingleton(typeof(KafkaService<>));

builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "api_users");
        options.RoutePrefix = "docs";
    });

    app.MapGet("/", () => Results.Redirect("/docs")).ExcludeFromDescription();

} else
{
    app.MapGet("/", () => "Running.");
}


app.MapControllers();


app.Run();

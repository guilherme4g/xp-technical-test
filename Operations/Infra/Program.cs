using Common.Helpers;
using Infra.Consumers;
using Infra.Controllers;
using Infra.EntityFramework;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DatabaseConnection")));

builder.Services.AddSingleton<IKafkaConsumerService, DebitOperationConsumer>();
builder.Services.AddSingleton<IKafkaConsumerService, CreditOperationConsumer>();

builder.Services.AddControllers();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

var cts = new CancellationTokenSource();
var services = app.Services.GetServices<IKafkaConsumerService>();
var tasks = services.Select(s => s.Consume(cts.Token)).ToArray();
Task.WaitAll(tasks);

app.MapControllers();

app.Run();

using CommandsService.AsyncDataServices;
using CommandsService.Data;
using CommandsService.EventProcessing;
using CommandsService.SyncDataServices.Grpc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddHostedService<MessageBusSubscriber>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("InMen"));
builder.Services.AddScoped<ICommandRepo, CommandRepo>();
builder.Services.AddSingleton<IEventProcessor, EventProcessor>();
builder.Services.AddScoped<IPlatformDataClient, PlatformDataClient>();

var app = builder.Build();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

PrepDb.PrepPopulation(app);

app.Run();
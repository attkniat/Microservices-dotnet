using Microsoft.EntityFrameworkCore;
using PlatformService.AsyncDataServices;
using PlatformService.Data;
using PlatformService.SyncDataServices.Grpc;
using PlatformService.SyncDataServices.Http;

var builder = WebApplication.CreateBuilder(args);
var settings = builder.Configuration;
var env = builder.Environment;

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

if (env.IsProduction())
{
    Console.WriteLine("---> Using SQL Server DB");
    builder.Services.AddDbContext<AppDBContext>(opt => opt.UseSqlServer(settings.GetConnectionString("PlatformsConn")));
}
else
{
    Console.WriteLine("---> Using InMemery DB");
    builder.Services.AddDbContext<AppDBContext>(opt => opt.UseInMemoryDatabase("InMem"));
}

builder.Services.AddScoped<IPlatformRepo, PlatformRepo>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddHttpClient<ICommandDataClient, HttpCommandDataClient>();
builder.Services.AddSingleton<IMessageBusClient, MessageBusClient>();
builder.Services.AddGrpc();

Console.WriteLine($"---> CommandService Endpoint {settings["CommandService"]}");

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();
app.MapGrpcService<GrpcPlatformService>();

app.MapGet("/protos/platforms.proto", async context =>
{
    await context.Response.WriteAsync(File.ReadAllText("/protos/platforms.proto"));
});

PrepDb.PrepPolulation(app, env.IsProduction());

app.Run();
using CommandsService.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("InMen"));
builder.Services.AddScoped<ICommandRepo, CommandRepo>();

var app = builder.Build();
//app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
using Microsoft.EntityFrameworkCore;
using PlatformService.Model;

namespace PlatformService.Data
{
    public static class PrepDb
    {
        public static void PrepPolulation(IApplicationBuilder app, bool isprod)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<AppDBContext>(), isprod);
            }
        }

        private static void SeedData(AppDBContext context, bool isprod)
        {
            if (isprod)
            {
                Console.WriteLine("--> Attempting to apply migrations...");
                try
                {
                    context.Database.Migrate();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Could not run migrations: {ex.Message}");
                }
            }

            if (!context.Platforms.Any())
            {
                Console.WriteLine("--> Seeding Data...");

                context.Platforms.AddRange(
                    new Platform() { Name = "DOTNET", Publlisher = "Microsoft", Cost = "free" },
                    new Platform() { Name = "SQL Server Express", Publlisher = "Microsoft", Cost = "free" },
                    new Platform() { Name = "Kubernets", Publlisher = "Cloud Native Computing Foundation", Cost = "free" }
                );
                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("--> We already have data");
            }
        }
    }
}

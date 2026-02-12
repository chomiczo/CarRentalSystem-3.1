using CarRentalSystem.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Identity;
using CarRentalSystem;
using Microsoft.Extensions.Logging; // Dodane dla logowania
using System;
using System.Threading.Tasks;

public class Program
{
    public static async Task Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();

        using (var scope = host.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var logger = services.GetRequiredService<ILogger<Program>>();

            try
            {
                logger.LogInformation("--> Rozpoczynam migracjê bazy danych...");

                var dbContext = services.GetRequiredService<ApplicationDbContext>();

                // 1. To wykonuje migracje (tworzy bazê jeœli nie istnieje)
                await dbContext.Database.MigrateAsync();

                logger.LogInformation("--> Migracja zakoñczona pomyœlnie!");

                // 2. Logika seedowania ról i u¿ytkowników
                var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

                var roles = new string[] { "Administrator", "InnaRola" };
                foreach (var roleName in roles)
                {
                    var roleExist = await roleManager.RoleExistsAsync(roleName);
                    if (!roleExist)
                    {
                        await roleManager.CreateAsync(new IdentityRole(roleName));
                        logger.LogInformation($"--> Utworzono rolê: {roleName}");
                    }
                }

                // 3. Uruchomienie Twojego SeedData (pamiêtaj, ¿eby SeedData te¿ by³o async, jeœli to mo¿liwe)
                // Zak³adam, ¿e SeedData.Initialize jest metod¹ async. Jeœli nie, usuñ 'await'.
                await SeedData.Initialize(services, userManager, roleManager);

                logger.LogInformation("--> Seedowanie danych zakoñczone.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Wyst¹pi³ b³¹d krytyczny podczas migracji lub seedowania bazy.");
            }
        }

        await host.RunAsync();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
}
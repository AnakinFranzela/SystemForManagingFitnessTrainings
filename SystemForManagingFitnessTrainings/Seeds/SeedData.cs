using Microsoft.AspNetCore.Identity;
using SystemForManagingFitnessTrainings.Data;
using SystemForManagingFitnessTrainings.Entities;

namespace SystemForManagingFitnessTrainings.Seeds
{
    public class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            // Seed Roles (existing code)
            await SeedRolesAsync(roleManager);

            // Seed Admin User (existing code)
            await SeedAdminUserAsync(userManager);

            // Seed Exercises (new code)
            await SeedExercisesAsync(context);
        }

        public static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            if (!await roleManager.RoleExistsAsync("Customer"))
            {
                await roleManager.CreateAsync(new IdentityRole("Customer"));
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }
        }

        // Seed Admin User (existing method)
        public static async Task SeedAdminUserAsync(UserManager<ApplicationUser> userManager)
        {
            var adminUser = await userManager.FindByEmailAsync("admin@example.com");
            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    Email = "admin@example.com",
                    UserName = "admin@example.com",
                    EmailConfirmed = true,
                    FirstName = "Admin",
                    LastName = "User"
                };
                await userManager.CreateAsync(adminUser, "YourAdminPassword123!");
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }
        }

        private static async Task SeedExercisesAsync(ApplicationDbContext context)
        {
            // Check if exercises already exist to avoid duplicates
            if (!context.Exercises.Any())
            {
                var exercises = new List<Exercise>
                    {
                        new Exercise { Name = "Лицеви опори", Category = "Калистеника", Description = "Упражнение използващо главно тежестта на тялото, имащо ефект върху ръцете и гърдите." },
                        new Exercise { Name = "Клекове", Category = "Силови", Description = "Силово упражнение за долната част на тялото." },
                        new Exercise { Name = "Планк", Category = "Издръжливост", Description = "Упражнение за издръжливост на коремните мускули." },
                        new Exercise { Name = "Скачане на въже", Category = "Кардио", Description = "Аеробно упражнение за сърдечносъдовото здраве." },
                        new Exercise { Name = "Лежанка", Category = "Силови", Description = "Упражнение за силата на ръцете и гърдите." },
                        new Exercise { Name = "Бягане", Category = "Кардио", Description = "Аеробно упражнение за сърдечносъдовото здраве." },
                        new Exercise { Name = "Набирания", Category = "Калистеника", Description = "Упражнение използващо главно тежестта на тялото, за трицепс." },
                        new Exercise { Name = "Бърпита", Category = "Издръжливост", Description = "Упражнение за издръжливостта на цялото тяло." },
                        new Exercise { Name = "Мъртва тяга", Category = "Силови", Description = "Комбинирано упражнение за сила, насочено към множество мускулни групи." },
                        new Exercise { Name = "Кофички", Category = "Калистеника", Description = "Упражнение с тежестта на тялото за гърди и трицепс." }
                    };

                context.Exercises.AddRange(exercises);
                await context.SaveChangesAsync();
            }
        }
    }
}

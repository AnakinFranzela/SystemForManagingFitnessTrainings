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
                        new Exercise { Name = "Pushups", Category = "Strength", Description = "Upper body strength exercise." },
                        new Exercise { Name = "Squats", Category = "Strength", Description = "Lower body strength exercise." },
                        new Exercise { Name = "Plank", Category = "Core", Description = "Core strengthening exercise." },
                        new Exercise { Name = "Running", Category = "Cardio", Description = "Aerobic exercise for cardiovascular health." },
                        new Exercise { Name = "Bench Press", Category = "Strength", Description = "Chest and arm strength exercise." },
                        new Exercise { Name = "Cycling", Category = "Cardio", Description = "Low-impact aerobic exercise." },
                        new Exercise { Name = "Lunges", Category = "Strength", Description = "Leg strength and stability exercise." },
                        new Exercise { Name = "Burpees", Category = "Endurance", Description = "Full-body endurance exercise." },
                        new Exercise { Name = "Deadlifts", Category = "Strength", Description = "Compound strength exercise targeting multiple muscle groups." },
                        new Exercise { Name = "Swimming", Category = "Cardio", Description = "Full-body aerobic exercise." }
                    };

                context.Exercises.AddRange(exercises);
                await context.SaveChangesAsync();
            }
        }
    }
}

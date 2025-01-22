using Microsoft.AspNetCore.Identity;

namespace SystemForManagingFitnessTrainings.Seeds
{
    public class RolesSeed
    {
        public static async void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var scope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetService<RoleManager<IdentityRole>>();
                if (!await roleManager.RoleExistsAsync("Customer"))
                {
                    await roleManager.CreateAsync(new IdentityRole("Customer"));
                    await roleManager.CreateAsync(new IdentityRole("Admin"));
                }
            }
        }
    }
}

using Microsoft.EntityFrameworkCore;
using SystemForManagingFitnessTrainings.Data;
using SystemForManagingFitnessTrainings.Entities;

namespace SystemForManagingFitnessTrainings.Seeds
{
    public class ExercisesSeed
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
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
                    context.SaveChangesAsync();
                }
            }
        }
    }
}

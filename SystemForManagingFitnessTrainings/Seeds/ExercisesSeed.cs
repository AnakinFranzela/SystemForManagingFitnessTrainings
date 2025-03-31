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
                    context.SaveChangesAsync();
                }
            }
        }
    }
}

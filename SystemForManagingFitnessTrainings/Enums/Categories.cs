using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace SystemForManagingFitnessTrainings.Enums
{
    public enum Categories
    {
        [Display(Name = "Кардио")]
        Cardio,
        [Display(Name = "Силови тренировки")]
        Strength,
        [Display(Name = "Издръжливост")]
        Endurance,
        [Display(Name = "Калистеника")]
        Calisthenics,
        [Display(Name = "Разтягане")]
        Stretching
    }
}

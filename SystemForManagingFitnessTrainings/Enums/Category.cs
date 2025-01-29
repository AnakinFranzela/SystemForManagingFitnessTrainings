using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace SystemForManagingFitnessTrainings.Enums
{
    public enum Category
    {
        [Display(Name = "Кардио")]
        Cardio,
        [Display(Name = "Силови тренировки")]
        PowerTraining,
        [Display(Name = "Издръжливост")]
        Endurance,
        [Display(Name = "Калистеника")]
        Calisthenics,
        [Display(Name = "Разтягане")]
        Stretching
    }
}

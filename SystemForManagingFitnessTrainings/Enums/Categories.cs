using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace SystemForManagingFitnessTrainings.Enums
{
    public enum Categories
    {
        [Display(Name = "Кардио")]
        Кардио,
        [Display(Name = "Силови")]
        Силови,
        [Display(Name = "Издръжливост")]
        Издръжливост,
        [Display(Name = "Калистеника")]
        Калистеника
    }
}

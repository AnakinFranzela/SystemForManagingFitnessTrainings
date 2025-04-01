using System.ComponentModel.DataAnnotations;

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

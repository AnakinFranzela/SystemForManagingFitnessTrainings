using System.ComponentModel.DataAnnotations;
using SystemForManagingFitnessTrainings.Enums;

namespace SystemForManagingFitnessTrainings.ViewModels
{
    public class ExerciseViewModel
    {
        [Required(ErrorMessage = "Полето \"Име\" е задължително")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Полето \"Категория\" е задължително")]
        public Categories Category { get; set; }
        public string Description { get; set; }
    }
}

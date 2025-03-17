using System.ComponentModel.DataAnnotations;

namespace SystemForManagingFitnessTrainings.ViewModels
{
    public class ProgressViewModel
    {
        public int? Id { get; set; }
        public string? UserId { get; set; }
        [Required(ErrorMessage = "Полето \"Упражнение\" е задължително")]
        public int ExerciseId { get; set; }
        [Required(ErrorMessage = "Полето \"Дата\" е задължително")]
        public DateOnly Date { get; set; }
        [Required(ErrorMessage = "Полето \"Резултати\" е задължително")]
        public string Results { get; set; }
    }
}

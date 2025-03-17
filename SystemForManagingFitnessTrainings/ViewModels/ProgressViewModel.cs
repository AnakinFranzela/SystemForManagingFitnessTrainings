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
        [Required]
        [Range(0, 1500)]
        public int Weight { get; set; }
        [Required]
        [Range(0, 1000)]
        public int Repetition { get; set; }
        [Required]
        [Range(0, 3600)]
        public int TimeInSeconds { get; set; }
    }
}

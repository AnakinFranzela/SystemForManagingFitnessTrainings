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
        public DateOnly? Date { get; set; }
        [Required(ErrorMessage = "Полето \"Тежести\" е задължително")]
        [Range(0, 1500, ErrorMessage = "Може да изберете от 0 до 1500 килограма")]
        public int? Weight { get; set; }
        [Required(ErrorMessage = "Полето \"Повторения\" е задължително")]
        [Range(1, 1000, ErrorMessage = "Може да изберете от 0 до 1000 повторения")]
        public int? Repetition { get; set; }
        [Required(ErrorMessage = "Полето \"Време\" е задължително")]
        [Range(1, 86400, ErrorMessage = "Може да изберете от 1 до 86400 секунди")]
        public int? TimeInSeconds { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace SystemForManagingFitnessTrainings.ViewModels
{
    public class CalendarViewModel
    {
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public ICollection<int> ExerciseIds { get; set; } = new List<int>();
    }
}

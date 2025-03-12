using System.ComponentModel.DataAnnotations;

namespace SystemForManagingFitnessTrainings.ViewModels
{
    public class CalendarViewModel
    {
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public TimeSpan Time { get; set; }
    }
}

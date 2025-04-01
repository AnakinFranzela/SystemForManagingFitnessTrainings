using System.ComponentModel.DataAnnotations;
using SystemForManagingFitnessTrainings.Entities;

namespace SystemForManagingFitnessTrainings.ViewModels
{
    public class TrainingPlanViewModel
    {
        public int? Id { get; set; }
        [Required(ErrorMessage = "Полето \"Име\" е задължително")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Полето \"Честота\" е задължително")]
        [Range(1, 7, ErrorMessage = "Трябва да е число от 1 до 7")]
        public int Frequency { get; set; }
        public ICollection<Exercise> Exercises { get; set; } = new List<Exercise>();
        public ICollection<int> SelectedExercisesIds { get; set; } = new List<int>();
        //public string? UserId { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace SystemForManagingFitnessTrainings.Entities
{
    public class TrainingPlan
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Frequency { get; set; } // Пъти в седмицата

        // List of exercises (not normalized, can be handled differently)
        public ICollection<Exercise> Exercises { get; set; }

        // One-to-One relationship with User
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}

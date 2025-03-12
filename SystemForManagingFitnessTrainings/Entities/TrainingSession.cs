using System.ComponentModel.DataAnnotations;

namespace SystemForManagingFitnessTrainings.Entities
{
    public class TrainingSession
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime ScheduledDate { get; set; }

        // Link to User
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        // Exercises planned for this session
        public ICollection<Exercise> Exercises { get; set; } = new List<Exercise>();
    }
}

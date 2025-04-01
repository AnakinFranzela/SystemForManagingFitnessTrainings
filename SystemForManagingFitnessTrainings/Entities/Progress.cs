using System.ComponentModel.DataAnnotations;

namespace SystemForManagingFitnessTrainings.Entities
{
    public class Progress
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        [Required]
        public int ExerciseId { get; set; }
        public Exercise Exercise { get; set; }
        [Required]
        public DateOnly Date { get; set; }
        [Required]
        public string Results { get; set; }

    }
}

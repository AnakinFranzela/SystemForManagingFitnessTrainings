using System.ComponentModel.DataAnnotations;

namespace SystemForManagingFitnessTrainings.Entities
{
    public class TrainingPlans
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int FrequencyOfTrainigns { get; set; }
        [Required]
        public string Frequency { get; set; } // e.g., "3 times per week"

        // List of exercises (not normalized, can be handled differently)
        public ICollection<Exercises> Exercises { get; set; }

        // One-to-One relationship with User
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SystemForManagingFitnessTrainings.Entities
{
    public class Progress
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public int ExerciseId { get; set; }
        public Exercises Exercise { get; set; }
        public DateOnly Date { get; set; }
        public string Results { get; set; }

    }
}

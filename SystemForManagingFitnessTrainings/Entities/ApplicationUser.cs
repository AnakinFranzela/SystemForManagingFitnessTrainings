using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SystemForManagingFitnessTrainings.Entities
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        // One-to-One relationship with TrainingPlan
        public TrainingPlan TrainingPlan { get; set; }

        // Many-to-Many relationship with Exercises
        public ICollection<Exercise> Exercises { get; set; }
    }
}

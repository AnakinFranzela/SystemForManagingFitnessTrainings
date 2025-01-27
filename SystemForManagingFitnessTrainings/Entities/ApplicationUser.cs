using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SystemForManagingFitnessTrainings.Entities
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public int FirstName { get; set; }
        [Required]
        public int LastName { get; set; }
        // One-to-One relationship with TrainingPlan
        public TrainingPlans TrainingPlan { get; set; }

        // Many-to-Many relationship with Exercises
        public ICollection<Exercises> Exercises { get; set; }
    }
}

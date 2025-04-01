using System.ComponentModel.DataAnnotations;

namespace SystemForManagingFitnessTrainings.Entities
{
    public class Exercise
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Category { get; set; }
        public string Description { get; set; }
        public ICollection<ApplicationUser> Users { get; set; }
    }
}

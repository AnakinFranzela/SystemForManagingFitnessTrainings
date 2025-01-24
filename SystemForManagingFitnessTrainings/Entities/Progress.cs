using System.ComponentModel.DataAnnotations.Schema;

namespace SystemForManagingFitnessTrainings.Entities
{
    public class Progress
    {
        public int Id { get; set; }
        [ForeignKey("User")]
        public UserInformation User { get; set; }
        [ForeignKey("Exercise")]
        public Exercises Exercise { get; set; }
        public DateOnly Date { get; set; }
        public string Results { get; set; }

    }
}

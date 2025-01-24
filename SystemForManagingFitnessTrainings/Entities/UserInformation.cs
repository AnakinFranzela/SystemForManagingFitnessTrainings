using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace SystemForManagingFitnessTrainings.Entities
{
    public class UserInformation : IdentityUser
    {
        public int FirstName { get; set; }
        public int LastName { get; set; }
        [ForeignKey("Exercise")]
        public UserExercises Exercise {  get; set; } 
    }
}

using Microsoft.AspNetCore.Identity;

namespace SystemForManagingFitnessTrainings.Entities
{
    public class UserInformation : IdentityUser
    {
        public int FirstName { get; set; }
        public int LastName { get; set; }
    }
}

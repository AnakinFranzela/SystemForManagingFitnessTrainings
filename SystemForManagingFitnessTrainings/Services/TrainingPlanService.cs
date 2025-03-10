using Microsoft.EntityFrameworkCore;
using SystemForManagingFitnessTrainings.Data;
using SystemForManagingFitnessTrainings.Entities;
using SystemForManagingFitnessTrainings.Services.IServices;

namespace SystemForManagingFitnessTrainings.Services
{
    public class TrainingPlanService : ITrainingPlanService
    {
        private readonly ApplicationDbContext _context;

        public TrainingPlanService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Check if a user already has a training plan
        public async Task<bool> UserHasTrainingPlanAsync(string userId)
        {
            return await _context.TrainingPlans.AnyAsync(tp => tp.UserId == userId);
        }

        // Create a new training plan for a user
        public async Task CreateTrainingPlanAsync(string userId, string name, int frequency, ICollection<int> exercises)
        {
            ApplicationUser? user = await _context.Users.Include(u => u.Exercises).FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
                throw new InvalidOperationException($"User with ID {userId} not found.");
            }
            var trainingPlan = new TrainingPlan
            {
                Name = name,
                Frequency = frequency,
                UserId = userId
            };

            _context.TrainingPlans.Add(trainingPlan);

            user.Exercises = await _context.Exercises.Where(e => exercises.Contains(e.Id)).ToListAsync();
            _context.Update(user);
            await _context.SaveChangesAsync();
        }

        // Get a user's training plan
        public async Task<TrainingPlan> GetUserTrainingPlanAsync(string userId)
        {
            return await _context.TrainingPlans
                .Include(tp => tp.User.Exercises)
                .FirstOrDefaultAsync(tp => tp.UserId == userId);
        }

        // Update a user's training plan
        public async Task<bool> UpdateTrainingPlanAsync(int id, string name, int frequency, ICollection<Exercise> exercises)
        {
            var trainingPlan = await _context.TrainingPlans.FindAsync(id);
            var user = await _context.Users.Include(u => u.Exercises).FirstOrDefaultAsync(u => u.TrainingPlan.Id == id);

            if (trainingPlan == null)
            {
                return false;
            }

            trainingPlan.Name = name;
            trainingPlan.Frequency = frequency;
            user.Exercises = exercises;

            _context.Update(trainingPlan);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}

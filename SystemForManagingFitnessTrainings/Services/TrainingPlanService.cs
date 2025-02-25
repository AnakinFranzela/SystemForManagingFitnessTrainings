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
        public async Task<TrainingPlan> CreateTrainingPlanAsync(string userId, string name, int frequency)
        {
            var trainingPlan = new TrainingPlan
            {
                UserId = userId,
                Name = name,
                Frequency = frequency
            };

            _context.TrainingPlans.Add(trainingPlan);
            await _context.SaveChangesAsync();

            return trainingPlan;
        }

        // Get a user's training plan
        public async Task<TrainingPlan> GetUserTrainingPlanAsync(string userId)
        {
            return await _context.TrainingPlans
                .Include(tp => tp.Exercises)
                .FirstOrDefaultAsync(tp => tp.UserId == userId);
        }

        // Update a user's training plan
        public async Task<bool> UpdateTrainingPlanAsync(int id, string name, int frequency)
        {
            var trainingPlan = await _context.TrainingPlans.FindAsync(id);

            if (trainingPlan == null)
            {
                return false;
            }

            trainingPlan.Name = name;
            trainingPlan.Frequency = frequency;

            _context.Update(trainingPlan);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}

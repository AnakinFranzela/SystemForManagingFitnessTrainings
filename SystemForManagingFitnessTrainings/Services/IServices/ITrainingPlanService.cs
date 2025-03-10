using SystemForManagingFitnessTrainings.Entities;

namespace SystemForManagingFitnessTrainings.Services.IServices
{
    public interface ITrainingPlanService
    {
        public Task<bool> UserHasTrainingPlanAsync(string userId);
        Task CreateTrainingPlanAsync(string userId, string name, int frequency, ICollection<int> exercises);
        public Task<TrainingPlan> GetUserTrainingPlanAsync(string userId);
        public Task<bool> UpdateTrainingPlanAsync(int id, string name, int frequency, ICollection<Exercise> exercises);

    }
}

using SystemForManagingFitnessTrainings.Entities;
using SystemForManagingFitnessTrainings.ViewModels;

namespace SystemForManagingFitnessTrainings.Services.IServices
{
    public interface ITrainingPlanService
    {
        public Task<bool> UserHasTrainingPlanAsync(string userId);
        Task CreateTrainingPlanAsync(string userId, string name, int frequency, ICollection<int> exercises);
        public Task<TrainingPlan> GetUserTrainingPlanAsync(string userId);
        public Task UpdateTrainingPlanAsync(int id, string name, int frequency, ICollection<int> exercisesIds);
        Task<TrainingPlan> GetTrainingPlanByIdAsync(int id);
        public Task DeleteTrainingPlanAsync(int id);
    }
}

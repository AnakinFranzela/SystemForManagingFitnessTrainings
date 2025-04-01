using SystemForManagingFitnessTrainings.Entities;

namespace SystemForManagingFitnessTrainings.Services.IServices
{
    public interface ITrainingPlanService
    {
        Task<bool> UserHasTrainingPlanAsync(string userId);
        Task CreateTrainingPlanAsync(string userId, string name, int frequency, ICollection<int> exercises);
        Task<TrainingPlan> GetUserTrainingPlanAsync(string userId);
        Task UpdateTrainingPlanAsync(int id, string name, int frequency, ICollection<int> exercisesIds);
        Task<TrainingPlan> GetTrainingPlanByIdAsync(int id);
        Task DeleteTrainingPlanAsync(int id);
    }
}

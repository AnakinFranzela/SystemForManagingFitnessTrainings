using SystemForManagingFitnessTrainings.Entities;

namespace SystemForManagingFitnessTrainings.Services.IServices
{
    public interface ITrainingPlanService
    {
		Task<IEnumerable<TrainingPlan>> GetAllAsync();
		Task<TrainingPlan> GetByIdAsync(int id);
		Task AddAsync(TrainingPlan plan);
		Task UpdateAsync(TrainingPlan plan);
		Task DeleteAsync(int id);
	}
}

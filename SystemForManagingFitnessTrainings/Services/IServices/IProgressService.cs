using SystemForManagingFitnessTrainings.Entities;

namespace SystemForManagingFitnessTrainings.Services.IServices
{
    public interface IProgressService
    {
        Task<Progress> LogProgressAsync(string userId, int exerciseId, DateOnly date, string results);
        Task<List<Progress>> GetUserProgressAsync(string userId);
        Task<List<Progress>> GetExerciseProgressAsync(string userId, int exerciseId);
        Task DeleteProgressAsync(int id, string userId);
    }
}

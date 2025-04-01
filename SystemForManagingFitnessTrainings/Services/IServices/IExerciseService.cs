using SystemForManagingFitnessTrainings.Entities;

namespace SystemForManagingFitnessTrainings.Services.IServices
{
    public interface IExerciseService
    {
        Task<List<Exercise>> GetAllExercisesAsync();
        Task<bool> CheckForExistingExercise(string exerciseName);
        Task<Exercise> GetExerciseByIdAsync(int id);
        Task<List<Exercise>> GetExercisesByCategoryAsync(string category);
        Task<Exercise> CreateExerciseAsync(string name, string category, string description);
        Task<bool> UpdateExerciseAsync(int id, string name, string category, string description);
        Task<bool> DeleteExerciseAsync(int id);

    }
}

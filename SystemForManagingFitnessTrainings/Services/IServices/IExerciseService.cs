using SystemForManagingFitnessTrainings.Entities;

namespace SystemForManagingFitnessTrainings.Services.IServices
{
    public interface IExerciseService
    {
        public Task<List<Exercise>> GetAllExercisesAsync();
        Task<Exercise> GetExerciseByIdAsync(int id);
        public Task<List<Exercise>> GetExercisesByCategoryAsync(string category);
        public Task<Exercise> CreateExerciseAsync(string name, string category, string description);
        public Task<bool> UpdateExerciseAsync(int id, string name, string category, string description);
        public Task<bool> DeleteExerciseAsync(int id);

    }
}

using Microsoft.EntityFrameworkCore;
using SystemForManagingFitnessTrainings.Data;
using SystemForManagingFitnessTrainings.Entities;
using SystemForManagingFitnessTrainings.Services.IServices;

namespace SystemForManagingFitnessTrainings.Services
{
    public class ExerciseService : IExerciseService
    {
        private readonly ApplicationDbContext _context;

        public ExerciseService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Get all exercises
        public async Task<List<Exercise>> GetAllExercisesAsync()
        {
            return await _context.Exercises.ToListAsync();
        }

        public async Task<Exercise> GetExerciseByIdAsync(int id)
        {
            return await _context.Exercises.FindAsync(id);
        }

        // Get exercises by category
        public async Task<List<Exercise>> GetExercisesByCategoryAsync(string category)
        {
            return await _context.Exercises
                .Where(e => e.Category == category)
                .ToListAsync();
        }

        // Create a new exercise
        public async Task<Exercise> CreateExerciseAsync(string name, string category, string description)
        {
            var exercise = new Exercise
            {
                Name = name,
                Category = category,
                Description = description
            };

            _context.Exercises.Add(exercise);
            await _context.SaveChangesAsync();

            return exercise;
        }

        // Update an existing exercise
        public async Task<bool> UpdateExerciseAsync(int id, string name, string category, string description)
        {
            var exercise = await _context.Exercises.FindAsync(id);

            if (exercise == null)
            {
                return false;
            }

            exercise.Name = name;
            exercise.Category = category;
            exercise.Description = description;

            _context.Update(exercise);
            await _context.SaveChangesAsync();

            return true;
        }

        // Delete an exercise
        public async Task<bool> DeleteExerciseAsync(int id)
        {
            var exercise = await _context.Exercises.FindAsync(id);

            if (exercise == null)
            {
                return false;
            }

            _context.Exercises.Remove(exercise);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}

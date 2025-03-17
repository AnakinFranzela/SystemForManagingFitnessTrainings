using SystemForManagingFitnessTrainings.Entities;
using SystemForManagingFitnessTrainings.Data;
using Microsoft.EntityFrameworkCore;
using SystemForManagingFitnessTrainings.Services.IServices;

namespace SystemForManagingFitnessTrainings.Services
{
    public class ProgressService : IProgressService
    {
        private readonly ApplicationDbContext _context;

        public ProgressService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Log a new progress entry
        public async Task<Progress> LogProgressAsync(string userId, int exerciseId, DateOnly date, string results)
        {
            var progress = new Progress
            {
                UserId = userId,
                ExerciseId = exerciseId,
                Date = date,
                Results = results
            };

            _context.ProgressRecords.Add(progress);
            await _context.SaveChangesAsync();

            return progress;
        }

        // Get progress entries for a user
        public async Task<List<Progress>> GetUserProgressAsync(string userId)
        {
            return await _context.ProgressRecords
                .Where(p => p.UserId == userId)
                .Include(p => p.Exercise)
                .OrderByDescending(p => p.Date)
                .ToListAsync();
        }

        public async Task<List<Progress>> GetExerciseProgressAsync(string userId, int exerciseId)
        {
            return await _context.ProgressRecords.Where(p => p.UserId == userId && p.ExerciseId == exerciseId).OrderBy(p => p.Date).ToListAsync();
        }

        public async Task DeleteProgressAsync(int id, string userId)
        {
            var record = await _context.ProgressRecords.FirstOrDefaultAsync(p => p.Id == id && p.UserId == userId);

            if (record != null)
            {
                _context.ProgressRecords.Remove(record);
                await _context.SaveChangesAsync();
            }
        }
    }
}

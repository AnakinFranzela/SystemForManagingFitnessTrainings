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

        public async Task<IEnumerable<Progress>> GetUserProgressAsync(string userId)
        {
            return await _context.ProgressRecords
                .Where(p => p.UserId == userId)
                .Include(p => p.Exercise)
                .ToListAsync();
        }

        public async Task AddProgressAsync(Progress progress)
        {
            _context.ProgressRecords.Add(progress);
            await _context.SaveChangesAsync();
        }
    }
}

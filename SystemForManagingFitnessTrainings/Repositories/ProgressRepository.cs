using SystemForManagingFitnessTrainings.Repositories.IRepositories;
using SystemForManagingFitnessTrainings.Entities;
using SystemForManagingFitnessTrainings.Data;
using Microsoft.EntityFrameworkCore;

namespace SystemForManagingFitnessTrainings.Repositories
{
    public class ProgressRepository : IProgressRepository
    {
        private readonly ApplicationDbContext _context;

        public ProgressRepository(ApplicationDbContext context)
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

using Microsoft.EntityFrameworkCore;
using SystemForManagingFitnessTrainings.Data;
using SystemForManagingFitnessTrainings.Entities;
using SystemForManagingFitnessTrainings.Services.IServices;

namespace SystemForManagingFitnessTrainings.Services
{
    public class TrainingPlanService : ITrainingPlanService
    {
		private readonly ApplicationDbContext _context;

		public TrainingPlanService(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<TrainingPlan>> GetAllAsync()
		{
			return await _context.TrainingPlans.Include(tp => tp.Exercises).ToListAsync();
		}

		public async Task<TrainingPlan> GetByIdAsync(int id)
		{
			return await _context.TrainingPlans.Include(tp => tp.Exercises).FirstOrDefaultAsync(tp => tp.Id == id);
		}
		
		public async Task AddAsync(TrainingPlan plan)
		{
			_context.TrainingPlans.Add(plan);
			await _context.SaveChangesAsync();
		}

		public async Task UpdateAsync(TrainingPlan plan)
		{
			_context.TrainingPlans.Update(plan);
			await _context.SaveChangesAsync();
		}

		public async Task DeleteAsync(int id)
		{
			var plan = await _context.TrainingPlans.FindAsync(id);
			if (plan != null)
			{
				_context.TrainingPlans.Remove(plan);
				await _context.SaveChangesAsync();
			}
		}
	}
}

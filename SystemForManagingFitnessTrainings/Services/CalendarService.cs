using Microsoft.EntityFrameworkCore;
using SystemForManagingFitnessTrainings.Data;
using SystemForManagingFitnessTrainings.Entities;
using SystemForManagingFitnessTrainings.Services.IServices;

namespace SystemForManagingFitnessTrainings.Services
{
    public class CalendarService : ICalendarService
    {
        private readonly ApplicationDbContext _context;
        public CalendarService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<object>> GetSessionsAsync(string userId)
        {
            var sessions =  await _context.TrainingSessions
                .Where(s => s.UserId == userId)
                .Select(s => new
                {
                    id = s.Id,
                    title = $"Тренировка {s.User.TrainingPlan.Name}",
                    start = s.ScheduledDate.ToString("yyyy-MM-ddTHH:mm")
                }).ToListAsync();

            return sessions.ToList<object>();
        }

        public async Task CreateSessionAsync(string userId, DateTime dateTime)
        {
            if (await SessionExistsAsync(userId, dateTime.Date))
                throw new InvalidOperationException("Имате вече тренировка за тази дата.");

            var session = new TrainingSession
            {
                UserId = userId,
                ScheduledDate = dateTime
            };

            _context.TrainingSessions.Add(session);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteSessionAsync(int sessionId)
        {
            var session = await _context.TrainingSessions.FindAsync(sessionId);
            if (session == null)
                throw new InvalidOperationException("Не е намерена тренировка.");

            _context.TrainingSessions.Remove(session);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> SessionExistsAsync(string userId, DateTime date)
        {
            return await _context.TrainingSessions.AnyAsync(ts => ts.UserId == userId && ts.ScheduledDate.Date == date.Date);
        }

        public async Task UpdateSessionAsync(int sessionId, DateTime newDateTime)
        {
            var session = await _context.TrainingSessions.FindAsync(sessionId);
            if (session == null)
                throw new InvalidOperationException("Сесията не е намерена.");

            session.ScheduledDate = newDateTime;
            await _context.SaveChangesAsync();
        }
    }
}

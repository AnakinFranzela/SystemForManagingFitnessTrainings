namespace SystemForManagingFitnessTrainings.Services.IServices
{
    public interface ICalendarService
    {
        Task<List<object>> GetSessionsAsync(string userId);
        Task<bool> SessionExistsAsync(string userId, DateTime date);
        Task CreateSessionAsync(string userId, DateTime dateTime);
        Task UpdateSessionAsync(int sessionId, DateTime newDateTime);
        Task DeleteSessionAsync(int sessionId);
    }
}

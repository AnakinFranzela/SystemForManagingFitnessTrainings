using SystemForManagingFitnessTrainings.Entities;

namespace SystemForManagingFitnessTrainings.Services.IServices
{
    public interface IProgressService
    {
        Task<IEnumerable<Progress>> GetUserProgressAsync(string userId);
        Task AddProgressAsync(Progress progress);
    }
}

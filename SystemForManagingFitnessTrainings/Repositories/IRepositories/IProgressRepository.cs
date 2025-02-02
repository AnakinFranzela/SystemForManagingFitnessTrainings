using SystemForManagingFitnessTrainings.Entities;

namespace SystemForManagingFitnessTrainings.Repositories.IRepositories
{
    public interface IProgressRepository
    {
        Task<IEnumerable<Progress>> GetUserProgressAsync(string userId);
        Task AddProgressAsync(Progress progress);
    }
}

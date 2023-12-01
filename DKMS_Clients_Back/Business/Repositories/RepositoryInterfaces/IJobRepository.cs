using DKMS_Clients_Back.Models;

namespace DKMS_Clients_Back.Business.Repositories.RepositoryInterfaces;

public interface IJobRepository
{
    Task<IEnumerable<Job>> GetAllAsync();
    Task<Job?> GetAsync(Guid jobId);
    Task<int> CreateAsync(Job job);
    Task<int> DeleteAsync(Guid jobId);
    Task<int> UpdateAsync(Job job);
    Task<int> UpdateJobCompleted(Guid jobId, bool isCompleted);
}
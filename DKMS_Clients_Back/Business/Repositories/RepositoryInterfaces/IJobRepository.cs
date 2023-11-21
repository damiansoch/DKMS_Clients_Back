using DKMS_Clients_Back.Models;

namespace DKMS_Clients_Back.Business.Repositories.RepositoryInterfaces;

public interface IJobRepository
{
    Task<IEnumerable<Job>> GetAllAsync();
}
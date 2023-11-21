using DKMS_Clients_Back.Models;

namespace DKMS_Clients_Back.Business.Services.ServiceInterfaces;

public interface IJobService
{
    Task<IEnumerable<Job>> GetAllAsync();
}
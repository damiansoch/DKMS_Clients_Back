using DKMS_Clients_Back.Models;

namespace DKMS_Clients_Back.Business.Repositories.RepositoryInterfaces;

public interface ICustomerRepository
{
    Task<IEnumerable<Customer>> GetAllAsync();
    Task<Customer?> GetById(Guid id);
}
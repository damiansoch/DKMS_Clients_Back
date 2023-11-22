using DKMS_Clients_Back.Models;

namespace DKMS_Clients_Back.Business.Repositories.RepositoryInterfaces;

public interface ICustomerRepository
{
    Task<IEnumerable<Customer>> GetAllAsync();
    Task<Customer?> GetByIdAsync(Guid id);
    Task<Guid?> AddAsync(Customer customer); 
    Task<int> DeleteAsync(Guid id);
    Task<int> UpdateAsync(Customer customer);
}
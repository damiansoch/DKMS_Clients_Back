using DKMS_Clients_Back.Models;

namespace DKMS_Clients_Back.Business.Repositories.RepositoryInterfaces;

public interface IAddressRepository
{
    Task<IEnumerable<Address>> GetAllAsync();
    Task<Address?> GetByIdAsync(Guid id);
    Task<int> AddAsync(Address address);
    Task<int> UpdateAsync(Address address);
    Task<int> DeleteAsync(Guid id);
}
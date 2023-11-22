using DKMS_Clients_Back.Dtos;
using DKMS_Clients_Back.Models;

namespace DKMS_Clients_Back.Business.Services.ServiceInterfaces;

public interface IAddressService
{
    Task<IEnumerable<Address>> GetAllAsync();
    Task<Address?> GetByIdAsync(Guid id);
    Task<Tuple<int,Address>?> AddAsync(Guid customerId, AddAddressRequestDto addAddressRequestDto);
    Task<int?> UpdateAsync(Guid addressId,UpdateAddressRequestDto updateAddressRequestDto);
    Task<int?> DeleteAsync(Guid id);
}
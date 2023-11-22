using DKMS_Clients_Back.Dtos;
using DKMS_Clients_Back.Models;

namespace DKMS_Clients_Back.Business.Services.ServiceInterfaces;

public interface ICustomerService
{
    Task<IEnumerable<Customer>> GetAllAsync();
    Task<Customer?> GetByIdAsync(Guid id);
    Task<Guid?> AddCustomerAndContactAsync(AddCustomerRequestDto addCustomerRequestDto);
    Task<int> DeleteAsync(Guid id);
    Task<int?> UpdateAsync(Guid customerId,UpdateCustomerRequestDto updateCustomerRequestDto);
}
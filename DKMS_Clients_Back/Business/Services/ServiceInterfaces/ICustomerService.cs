using DKMS_Clients_Back.Models;

namespace DKMS_Clients_Back.Business.Services.ServiceInterfaces;

public interface ICustomerService
{
    Task<IEnumerable<Customer>> GetAllAsync();
    Task<Customer?> GetByIdAsync(Guid id);
}
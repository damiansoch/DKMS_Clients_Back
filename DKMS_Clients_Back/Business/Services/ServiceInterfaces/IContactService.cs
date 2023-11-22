using DKMS_Clients_Back.Dtos;
using DKMS_Clients_Back.Models;

namespace DKMS_Clients_Back.Business.Services.ServiceInterfaces;

public interface IContactService
{
    Task<IEnumerable<Contact>> GetAllAsync();
    Task<int> AddAsync(Contact contact);
    Task<bool> DoesEmailExist(AddCustomerRequestDto addCustomerRequestDto);
    Task<int?> UpdateAsync(Guid contactId, UpdateContactRequestDto updateContactRequestDto);
}
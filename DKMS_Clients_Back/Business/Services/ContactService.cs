using DKMS_Clients_Back.Business.Repositories.RepositoryInterfaces;
using DKMS_Clients_Back.Business.Services.ServiceInterfaces;
using DKMS_Clients_Back.Dtos;
using DKMS_Clients_Back.Models;

namespace DKMS_Clients_Back.Business.Services
{
    public class ContactService:IContactService
    {
        private readonly IContactRepository _contactRepository;

        public ContactService(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }
        public async Task<IEnumerable<Contact>> GetAllAsync()
        {
            var results = await _contactRepository.GetAllAsync();
            return results;
        }

        public async Task<int> AddAsync(Contact contact)
        {
            var result = await _contactRepository.AddAsync(contact);
            return result;
        }

        public async Task<bool> DoesEmailExist(AddCustomerRequestDto addCustomerRequestDto)
        {
            var allCustomers = await GetAllAsync();

            return allCustomers.Any(x =>
                (x.Email == addCustomerRequestDto.Email || x.Email == addCustomerRequestDto.Email2) ||
                (x.Email2 != null && (x.Email2 == addCustomerRequestDto.Email || x.Email2 == addCustomerRequestDto.Email2)));

        }

        public async Task<int?> UpdateAsync(Guid contactId, UpdateContactRequestDto updateContactRequestDto)
        {
            var allContacts = await _contactRepository.GetAllAsync();
            var existingContact = allContacts.FirstOrDefault(x => x.Id == contactId);
            if (existingContact is null)
                return null;

            existingContact.PhoneNumber = updateContactRequestDto.PhoneNumber;
            existingContact.PhoneNumber2 = updateContactRequestDto.PhoneNumber2;
            existingContact.Email= updateContactRequestDto.Email;
            existingContact.Email2 = updateContactRequestDto.Email2;
            existingContact.ExtraDetails = updateContactRequestDto.ExtraDetails;

            var result = await _contactRepository.UpdateAsync(existingContact);
            return result;
        }
    }
}

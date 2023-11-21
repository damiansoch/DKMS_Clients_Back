using DKMS_Clients_Back.Business.Repositories.RepositoryInterfaces;
using DKMS_Clients_Back.Business.Services.ServiceInterfaces;
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
    }
}

using DKMS_Clients_Back.Business.Repositories.RepositoryInterfaces;
using DKMS_Clients_Back.Business.Services.ServiceInterfaces;
using DKMS_Clients_Back.Models;

namespace DKMS_Clients_Back.Business.Services
{
    public class AddressService : IAddressService
    {
        private readonly IAddressRepository _addressRepository;

        public AddressService(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }
        public async Task<IEnumerable<Address>> GetAllAsync()
        {
            var results = await _addressRepository.GetAllAsync();
            return results;
        }
    }
}

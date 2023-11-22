using DKMS_Clients_Back.Business.Repositories.RepositoryInterfaces;
using DKMS_Clients_Back.Business.Services.ServiceInterfaces;
using DKMS_Clients_Back.Dtos;
using DKMS_Clients_Back.Models;

namespace DKMS_Clients_Back.Business.Services
{
    public class AddressService : IAddressService
    {
        private readonly IAddressRepository _addressRepository;
        private readonly ILogger<AddressService> _logger;

        public AddressService(IAddressRepository addressRepository,  ILogger<AddressService> logger)
        {
            _addressRepository = addressRepository;
           
            _logger = logger;
        }
        public async Task<IEnumerable<Address>> GetAllAsync()
        {
            var results = await _addressRepository.GetAllAsync();
            return results;
        }

        public async Task<Address?> GetByIdAsync(Guid id)
        {
            var result = await _addressRepository.GetByIdAsync(id);
            return result;
        }

        public async Task<Tuple<int, Address>?> AddAsync(Guid customerId, AddAddressRequestDto addAddressRequestDto)
        {
            var address = new Address
            {
                Id = Guid.NewGuid(),
                CustomerId = customerId,
                HouseNumber = addAddressRequestDto.HouseNumber,
                HouseName = addAddressRequestDto.HouseName,
                AddressLine1 = addAddressRequestDto.AddressLine1,
                AddressLine2 = addAddressRequestDto.AddressLine2,
                AddressLine3 = addAddressRequestDto.AddressLine3,
                EirCode = addAddressRequestDto.EirCode,
            };

            var result = await _addressRepository.AddAsync(address);
            var resultTuple = new Tuple<int, Address>(result, address);
            return resultTuple;
        }

        public async Task<int?> UpdateAsync(Guid addressId, UpdateAddressRequestDto updateAddressRequestDto)
        {
            var existingAddress = await GetByIdAsync(addressId);
            if (existingAddress is null)
                return null;

            existingAddress.HouseNumber = updateAddressRequestDto.HouseNumber;
            existingAddress.HouseName = updateAddressRequestDto.HouseName;
            existingAddress.AddressLine1 = updateAddressRequestDto.AddressLine1;
            existingAddress.AddressLine2 = updateAddressRequestDto.AddressLine2;
            existingAddress.AddressLine3 = updateAddressRequestDto.AddressLine3;
            existingAddress.EirCode = updateAddressRequestDto.EirCode;

            var result = await _addressRepository.UpdateAsync(existingAddress);
            return result;
        }

        public async Task<int?> DeleteAsync(Guid id)
        {
            var existingAddress = await GetByIdAsync(id);
            if (existingAddress is null)
                return null;

            var result = await _addressRepository.DeleteAsync(id);
            return result;
        }
    }
}

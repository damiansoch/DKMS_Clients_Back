using System.Transactions;
using DKMS_Clients_Back.Business.Repositories.RepositoryInterfaces;
using DKMS_Clients_Back.Business.Services.ServiceInterfaces;
using DKMS_Clients_Back.Dtos;
using DKMS_Clients_Back.Models;
using Microsoft.AspNetCore.Authentication.OAuth;

namespace DKMS_Clients_Back.Business.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IAddressService _addressService;
        private readonly IContactService _contactService;
        private readonly IJobService _jobService;
        private readonly ILogger<CustomerService> _logger;

        public CustomerService(ICustomerRepository customerRepository, IAddressService addressService, IContactService contactService, IJobService jobService, ILogger<CustomerService> logger)
        {
            _customerRepository = customerRepository;
            _addressService = addressService;
            _contactService = contactService;
            _jobService = jobService;
            _logger = logger;
        }
        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            var results = await _customerRepository.GetAllAsync();
            return results;
        }

        public async Task<Customer?> GetByIdAsync(Guid id)
        {
            var customer = await _customerRepository.GetByIdAsync(id);
            if (customer is null) return null;

            //get all addresses for customer
            var allAddresses = await _addressService.GetAllAsync();
            if (allAddresses.Any())
            {
                var validAddresses = allAddresses.Where(x => x.CustomerId == id).ToList();
                if (validAddresses.Any())
                    customer.Addresses.AddRange(validAddresses);

            }

            //get all contact details for customer
            var allContacts = await _contactService.GetAllAsync();
            if (allContacts.Any())
            {
                var validContacts = allContacts.Where(x => x.CustomerId == id).ToList();
                if (validContacts.Any())
                    customer.Contacts.AddRange(validContacts);
            }

            //get all jobs for customer
            var allJobs = await _jobService.GetAllAsync();
            if (allJobs.Any())
            {
                var validJobs = allJobs.Where(x => x.CustomerId == id).ToList();
                if (validJobs.Any())
                    customer.Jobs.AddRange(validJobs);
            }

            return customer;

        }

        public async Task<Guid?> AddCustomerAndContactAsync(AddCustomerRequestDto addCustomerRequestDto)
        {
            //creating Customer
            var newCustomer = new Customer()
            {
                Id = Guid.NewGuid(),
                FirstName = addCustomerRequestDto.FirstName,
                LastName = addCustomerRequestDto.LastName,
                CompanyName = addCustomerRequestDto.CompanyName,
                Addresses = null,
                Contacts = null,
                Jobs = null,
                Created = DateTime.Now
            };
            //creating Contact
            var newContact = new Contact()
            {
                Id = Guid.NewGuid(),
                CustomerId = newCustomer.Id,
                PhoneNumber = addCustomerRequestDto.PhoneNumber,
                PhoneNumber2 = addCustomerRequestDto.PhoneNumber2,
                Email = addCustomerRequestDto.Email,
                Email2 = addCustomerRequestDto.Email2,
                ExtraDetails = addCustomerRequestDto.ExtraDetails,
            };
            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            try
            {
                var customerResult = await _customerRepository.AddAsync(newCustomer);
                await _contactService.AddAsync(newContact);
                // If both inserts are successful, commit the transaction
                scope.Complete();
                return customerResult;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }

        public async Task<int> DeleteAsync(Guid id)
        {
            var result = await _customerRepository.DeleteAsync(id);
            return result;
        }

        public async Task<int?> UpdateAsync(Guid customerId, UpdateCustomerRequestDto updateCustomerRequestDto)
        {
            var existingCustomer = await GetByIdAsync(customerId);
            if (existingCustomer is null)
                return null;

            existingCustomer.FirstName = updateCustomerRequestDto.FirstName;
            existingCustomer.LastName = updateCustomerRequestDto.LastName;
            existingCustomer.CompanyName = updateCustomerRequestDto.CompanyName;

            var result = await _customerRepository.UpdateAsync(existingCustomer);
            return result;
        }
    }
}

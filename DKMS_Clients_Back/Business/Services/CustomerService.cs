using DKMS_Clients_Back.Business.Repositories.RepositoryInterfaces;
using DKMS_Clients_Back.Business.Services.ServiceInterfaces;
using DKMS_Clients_Back.Models;

namespace DKMS_Clients_Back.Business.Services
{
    public class CustomerService:ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IAddressService _addressService;
        private readonly IContactService _contactService;
        private readonly IJobService _jobService;

        public CustomerService(ICustomerRepository customerRepository,IAddressService addressService,IContactService contactService,IJobService jobService)
        {
            _customerRepository = customerRepository;
            _addressService = addressService;
            _contactService = contactService;
            _jobService = jobService;
        }
        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            var results = await _customerRepository.GetAllAsync();
            return results;
        }

        public async Task<Customer?> GetByIdAsync(Guid id)
        {
            var customer = await _customerRepository.GetById(id);
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
                if(validJobs.Any())
                    customer.Jobs.AddRange(validJobs);
            }
           
            return customer;

        }

     
    }
}

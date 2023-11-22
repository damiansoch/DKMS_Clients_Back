using DKMS_Clients_Back.Business.Services.ServiceInterfaces;
using DKMS_Clients_Back.Dtos;
using DKMS_Clients_Back.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DKMS_Clients_Back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly ILogger<CustomerController> _logger;
        private readonly IContactService _contactService;

        public CustomerController(ICustomerService customerService,ILogger<CustomerController> logger,IContactService contactService)
        {
            _customerService = customerService;
            _logger = logger;
            _contactService = contactService;
        }

       
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> Get()
        {
            try
            {
                var results = await _customerService.GetAllAsync();
                if (!results.Any())
                    return NotFound("No Customers found in database");
                return Ok(results);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }

       
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Customer>> GetByIdAsync([FromRoute] Guid id)
        {
            try
            {
                var result = await _customerService.GetByIdAsync(id);
                if (result is null)
                    return NotFound("Customer not found");
                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }

        [HttpPost("create")]
        public async Task<ActionResult> AddAsync([FromBody]AddCustomerRequestDto addRequestDto)
        {
            try
            {
                var emailExist = await _contactService.DoesEmailExist(addRequestDto);
                if (emailExist)
                    return BadRequest("Email already exists in the database");

                var result = await _customerService.AddCustomerAndContactAsync(addRequestDto);
                if (result is null)
                    return BadRequest("There was an error when adding customer");
                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }

        [HttpDelete("remove/{customerId:guid}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid customerId)
        {
            try
            {
                var existingCustomer = await _customerService.GetByIdAsync(customerId);
                if (existingCustomer is null)
                    return NotFound("Customer not found in the database");
                var result = await _customerService.DeleteAsync(customerId);
                if (result < 1)
                    return BadRequest("Something went wrong");
                return Ok("Customer Deleted");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }

        [HttpPut("update/{customerId:guid}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid customerId, [FromBody]UpdateCustomerRequestDto updateCustomerRequestDto)
        {
            try
            {
                var result = await _customerService.UpdateAsync(customerId, updateCustomerRequestDto);
                if (result is null)
                    return NotFound("Customer not found in the database");
                return result > 0 ? Ok("Customer data has been updated") : BadRequest("Something went wrong");

            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }
    }
}

using DKMS_Clients_Back.Business.Services.ServiceInterfaces;
using DKMS_Clients_Back.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DKMS_Clients_Back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly ILogger<CustomersController> _logger;

        public CustomersController(ICustomerService customerService,ILogger<CustomersController> logger)
        {
            _customerService = customerService;
            _logger = logger;
        }

        // GET: api/<CustomersController>
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

        // GET api/<CustomersController>/5
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

        // POST api/<CustomersController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<CustomersController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CustomersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

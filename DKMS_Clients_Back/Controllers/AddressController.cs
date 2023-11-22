using DKMS_Clients_Back.Business.Services.ServiceInterfaces;
using DKMS_Clients_Back.Dtos;
using DKMS_Clients_Back.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DKMS_Clients_Back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly ILogger<AddressController> _logger;
        private readonly IAddressService _addressService;

        public AddressController(ILogger<AddressController> logger,IAddressService addressService)
        {
            _logger = logger;
            _addressService = addressService;
        }

        [HttpGet("{id:guid}")]
        [ActionName("GetByIdAsync")]
        public async Task<ActionResult<Address>> GetByIdAsync([FromRoute] Guid id)
        {
            try
            {
                var result = await _addressService.GetByIdAsync(id);
                return result is null ? NotFound("Address not found in database") : result;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }

        [HttpPost("{customerId:guid}")]
        public async Task<ActionResult> AddAsync([FromRoute] Guid customerId,
            [FromBody] AddAddressRequestDto addAddressRequestDto)
        {
            try
            {
                var result = await _addressService.AddAsync(customerId, addAddressRequestDto);

                if (result is null)
                    return NotFound("Customer not found in the database");
                
                if (result.Item1 < 0)
                    return BadRequest("Something went wrong");

                return CreatedAtAction(nameof(GetByIdAsync), new { id = result.Item2.Id }, result.Item2);

            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }

        [HttpPut("update/{addressId:guid}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid addressId,
            [FromBody] UpdateAddressRequestDto updateAddressRequestDto)
        {
            try
            {
                var result = await _addressService.UpdateAsync(addressId, updateAddressRequestDto);

                return result is null ? NotFound("Address not found in the database") :
                    result < 0 ? BadRequest("Something went wrong") : Ok("Address updated");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }

        [HttpDelete("delete/{addressId:guid}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid addressId)
        {
            try
            {
                var result = await _addressService.DeleteAsync(addressId);

                return result is null ? NotFound("Address not found in the database") :
                    result < 0 ? BadRequest("Something went wrong") : Ok("Address deleted");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }
    }
}

using DKMS_Clients_Back.Business.Services.ServiceInterfaces;
using DKMS_Clients_Back.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DKMS_Clients_Back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly ILogger<ContactController> _logger;
        private readonly IContactService _contactService;

        public ContactController(ILogger<ContactController> logger,IContactService contactService)
        {
            _logger = logger;
            _contactService = contactService;
        }

        [HttpPut("update/{id:guid}")]
        public async Task<IActionResult> UpdateContactAsync([FromRoute] Guid id,
            [FromBody] UpdateContactRequestDto updateContactRequestDto)
        {
            try
            {
                var result = await _contactService.UpdateAsync(id, updateContactRequestDto);

                return result is null ? NotFound("Contact details not found in database") :
                    result < 1 ? BadRequest("Something went wrong") : Ok("Contact details updated");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }

    }
}

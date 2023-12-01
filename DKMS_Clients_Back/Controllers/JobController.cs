using DKMS_Clients_Back.Business.Services.ServiceInterfaces;
using DKMS_Clients_Back.Dtos;
using DKMS_Clients_Back.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DKMS_Clients_Back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobController : ControllerBase
    {
        private readonly ILogger<JobController> _logger;
        private readonly IJobService _jobService;

        public JobController(ILogger<JobController> logger, IJobService jobService)
        {
            _logger = logger;
            _jobService = jobService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Job>>> GetAllAsync()
        {
            try
            {
                var result = await _jobService.GetAllAsync();
                if (!result.Any())
                    return NotFound("No jobs found in the database");
                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }

        [HttpGet("{jobId:guid}")]
        [ActionName("GetByIdAsync")]
        public async Task<ActionResult<Job>> GetByIdAsync([FromRoute] Guid jobId)
        {
            try
            {
                var result = await _jobService.GetAsync(jobId);
                return result is null ? NotFound("Job not found in database") : Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }

        [HttpPost("{customerId:guid}")]
        public async Task<IActionResult> AddAsync([FromRoute] Guid customerId,
            [FromBody] AddJobRequestDto addJobRequestDto)
        {
            try
            {
                var result = await _jobService.CreateAsync(customerId, addJobRequestDto);

                if (result.Item1 < 1)
                    return BadRequest("Something went wrong");
                return CreatedAtAction(nameof(GetByIdAsync), new { jobId = result.Item2.Id }, result.Item2);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }

        [HttpDelete("delete/{jobId:guid}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid jobId)
        {
            try
            {
                var result = await _jobService.DeleteAsync(jobId);

                return result is null ? NotFound("Job not found in the database")
                    : result < 1 ? BadRequest("Something went wrong")
                    : Ok("Job has been deleted");
            }
            catch (Exception e)
            {

                _logger.LogError(e.Message);
                throw;
            }
        }

        [HttpPut("update/{jobId:guid}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid jobId,
            [FromBody] UpdateJobRequestDto updateJobRequestDto)
        {
            try
            {
                var response = await _jobService.UpdateAsync(jobId, updateJobRequestDto);
                return response is null ? NotFound("Job not found") :
                    response < 1 ? BadRequest("Something went wrong") : Ok("Job updated");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }

        [HttpPut("update/{jobId:guid}/{isCompleted:bool}")]
        public async Task<IActionResult> UpdateJobCompleted([FromRoute] Guid jobId,
            [FromRoute] bool isCompleted)
        {
            try
            {
                var response = await _jobService.UpdateJobCompleted(jobId, isCompleted);
                return response is null ? NotFound("Job not found") :
                    response < 1 ? BadRequest("Something went wrong") : Ok($"Job marked as " + (isCompleted? "completed":"not completed"));
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }
    }
}

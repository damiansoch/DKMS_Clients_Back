using DKMS_Clients_Back.Dtos;
using DKMS_Clients_Back.Models;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace DKMS_Clients_Back.Business.Services.ServiceInterfaces;

public interface IJobService
{
    Task<IEnumerable<Job>> GetAllAsync();
    Task<Job?> GetAsync(Guid jobId);
    Task<Tuple<int, Job>> CreateAsync(Guid customerId,AddJobRequestDto addJobRequestDto);
    Task<int?> DeleteAsync(Guid jobId);
    Task<int?> UpdateAsync(Guid jobId,UpdateJobRequestDto updateJobRequestDto);
    Task<int?> UpdateJobCompleted(Guid jobId, bool isCompleted);
}
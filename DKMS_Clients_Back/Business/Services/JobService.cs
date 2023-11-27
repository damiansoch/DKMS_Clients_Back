using DKMS_Clients_Back.Business.Repositories.RepositoryInterfaces;
using DKMS_Clients_Back.Business.Services.ServiceInterfaces;
using DKMS_Clients_Back.Dtos;
using DKMS_Clients_Back.Models;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace DKMS_Clients_Back.Business.Services
{
    public class JobService:IJobService
    {
        private readonly IJobRepository _jobRepository;

        public JobService(IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }
        public async Task<IEnumerable<Job>> GetAllAsync()
        {
            var results = await _jobRepository.GetAllAsync();
            return results;
        }

        public async Task<Job?> GetAsync(Guid jobId)
        {
            var result = await _jobRepository.GetAsync(jobId);
            return result;
        }

        public async Task<Tuple<int,Job>> CreateAsync(Guid customerId, AddJobRequestDto addJobRequestDto)
        {
            var job = new Job()
            {
                Id = Guid.NewGuid(),
                CustomerId = customerId,
                Name = addJobRequestDto.Name,
                Description = addJobRequestDto.Description,
                Price = addJobRequestDto.Price,
                Deposit = addJobRequestDto.Deposit,
                ToBeCompleted = addJobRequestDto.ToBeCompleted,
            };

            var result = await _jobRepository.CreateAsync(job);
            var resultTuple = new Tuple<int, Job>(result, job);
            return resultTuple;
        }

        public async Task<int?> DeleteAsync(Guid jobId)
        {
            var job = await GetAsync(jobId);
            if (job is null)
                return null;

            var response = await _jobRepository.DeleteAsync(jobId);
            return response;
        }

        public async Task<int?> UpdateAsync(Guid jobId,UpdateJobRequestDto updateJobRequestDto)
        {
            var existingJob = await GetAsync(jobId);
            if(existingJob is null) return null;

            existingJob.Name = updateJobRequestDto.Name;
            existingJob.Description = updateJobRequestDto.Description;
            existingJob.Price = updateJobRequestDto.Price;
            existingJob.Deposit = updateJobRequestDto.Deposit;
            existingJob.ToBeCompleted = updateJobRequestDto.ToBeCompleted;
            existingJob.Completed = updateJobRequestDto.Completed;

            var response = await _jobRepository.UpdateAsync(existingJob);
            return response;
        }
    }
}

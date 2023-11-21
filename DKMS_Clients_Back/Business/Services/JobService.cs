using DKMS_Clients_Back.Business.Repositories.RepositoryInterfaces;
using DKMS_Clients_Back.Business.Services.ServiceInterfaces;
using DKMS_Clients_Back.Models;

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
    }
}

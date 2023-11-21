using DKMS_Clients_Back.Business.Repositories.RepositoryInterfaces;
using DKMS_Clients_Back.Models;
using System.Data.SqlClient;
using Dapper;

namespace DKMS_Clients_Back.Business.Repositories
{
    public class JobRepository:IJobRepository
    {
        private readonly ILogger<CustomerRepository> _logger;
        private readonly string _connectionString;
        public JobRepository(IConfiguration configuration, ILogger<CustomerRepository> logger)
        {
            _logger = logger;
            _connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }
        public async Task<IEnumerable<Job>> GetAllAsync()
        {
            const string query = "SELECT * FROM Jobs";

            try
            {
                await using var connection = new SqlConnection(_connectionString);
                var results = await connection.QueryAsync<Job>(query);
                return results;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }
    }
}

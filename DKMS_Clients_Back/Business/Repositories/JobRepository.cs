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

        public async Task<Job?> GetAsync(Guid jobId)
        {
            const string query = "SELECT * FROM Jobs WHERE Id = @Id";

            try
            {
                await using var connection = new SqlConnection(_connectionString);
                var results = await connection.QuerySingleOrDefaultAsync<Job>(query,new{Id = jobId});
                return results;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }

        public async Task<int> CreateAsync(Job job)
        {
            const string query = @"
                                    INSERT INTO [dbo].[Jobs]
                                               ([Id]
                                               ,[CustomerId]
                                               ,[Name]
                                               ,[Description]
                                               ,[Price]
                                               ,[Deposit]
                                               ,[Created]
                                               ,[ToBeCompleted]
                                               ,[Completed])
                                         VALUES
                                               (@Id
                                               ,@CustomerId
                                               ,@Name
                                               ,@Description
                                               ,@Price
                                               ,@Deposit
                                               ,@Created
                                               ,@ToBeCompleted
                                               ,@Completed)
                                    ";
            try
            {
                await using var connection = new SqlConnection(_connectionString);
                var result = await connection.ExecuteAsync(query, new { job.Id,job.CustomerId,job.Name,job.Description,job.Price,job.Deposit,Created = DateTime.Now ,job.ToBeCompleted,Completed = false});
                return result;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }

        public async Task<int> DeleteAsync(Guid jobId)
        {
            const string query = "Delete FROM Jobs WHERE Id = @Id";

            try
            {
                await using var connection = new SqlConnection(_connectionString);
                var results = await connection.ExecuteAsync(query, new { Id = jobId });
                return results;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }

        public async Task<int> UpdateAsync(Job job)
        {
            const string query = @"
                                   UPDATE [dbo].[Jobs]
                                   SET [Name] = @Name
                                      ,[Description] = @Description
                                      ,[Price] = @Price
                                      ,[Deposit] = @Deposit
                                      ,[ToBeCompleted] = @ToBeCompleted
                                      ,[Completed] = @Completed
                                 WHERE Id = @Id
                                    ";
            try
            {
                await using var connection = new SqlConnection(_connectionString);
                var result = await connection.ExecuteAsync(query, new { job.Id, job.Name, job.Description, job.Price, job.Deposit, job.ToBeCompleted, job.Completed });
                return result;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }
    }
}

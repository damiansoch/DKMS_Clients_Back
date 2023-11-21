using DKMS_Clients_Back.Business.Repositories.RepositoryInterfaces;
using DKMS_Clients_Back.Models;
using System.Data.SqlClient;
using Dapper;

namespace DKMS_Clients_Back.Business.Repositories
{
    public class CustomerRepository:ICustomerRepository
    {
        private readonly ILogger<CustomerRepository> _logger;
        private readonly string _connectionString;
        public CustomerRepository(IConfiguration configuration,ILogger<CustomerRepository> logger)
        {
            _logger = logger;
            _connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }
        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            const string query = "SELECT * FROM Customers";

            try
            {
                await using var connection = new SqlConnection(_connectionString);
                var results = await connection.QueryAsync<Customer>(query);
                return results;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }

        public async Task<Customer?> GetById(Guid id)
        {
            const string query = "SELECT * FROM Customers WHERE Id = @id";
            try
            {
                await using var connection = new SqlConnection(_connectionString);
                var results = await connection.QueryFirstOrDefaultAsync<Customer>(query, new {id});
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

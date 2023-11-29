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
            const string query = "SELECT * FROM Customers order by Created desc";

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

        public async Task<Customer?> GetByIdAsync(Guid id)
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

        public async Task<Guid?> AddAsync(Customer customer)
        {
            const string query = @"
                                    INSERT INTO [dbo].[Customers]
                                               ([Id]
                                               ,[FirstName]
                                               ,[LastName]
                                               ,[CompanyName]
                                                ,[Created])
                                         VALUES
                                               (@Id
                                               ,@FirstName
                                               ,@LastName
                                               ,@CompanyName
                                                ,@Created)
                                    ";
            try
            {
                await using var connection = new SqlConnection(_connectionString);
                var results = await connection.ExecuteAsync(query, new { customer.Id,customer.FirstName,customer.LastName,customer.CompanyName,customer.Created });
                if(results>0)
                    return customer.Id;
                return null;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }

        public async Task<int> DeleteAsync(Guid id)
        {
            const string query = "Delete FROM Customers where Id = @Id";

            try
            {
                await using var connection = new SqlConnection(_connectionString);
                var results = await connection.ExecuteAsync(query,new {Id = id});
                return results;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }

        public async Task<int> UpdateAsync(Customer customer)
        {
            const string query = @"
                                    UPDATE [dbo].[Customers]
                                       SET [FirstName] = @FirstName
                                          ,[LastName] = @LastName
                                          ,[CompanyName] = @CompanyName
                                     WHERE Id = @Id
                                    ";

            try
            {
                await using var connection = new SqlConnection(_connectionString);
                var results = await connection.ExecuteAsync(query, new { customer.FirstName,customer.LastName,customer.CompanyName,customer.Id });
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

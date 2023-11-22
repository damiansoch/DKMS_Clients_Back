using DKMS_Clients_Back.Models;
using System.Data.SqlClient;
using Dapper;
using DKMS_Clients_Back.Business.Repositories.RepositoryInterfaces;
using System.Net;

namespace DKMS_Clients_Back.Business.Repositories
{
    public class AddressRepository : IAddressRepository
    {
        private readonly ILogger<CustomerRepository> _logger;
        private readonly string _connectionString;

        public AddressRepository(IConfiguration configuration, ILogger<CustomerRepository> logger)
        {
            _logger = logger;
            _connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }
        public async Task<IEnumerable<Address>> GetAllAsync()
        {
            const string query = "SELECT * FROM Addresses";

            try
            {
                await using var connection = new SqlConnection(_connectionString);
                var results = await connection.QueryAsync<Address>(query);
                return results;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }

        public async Task<Address?> GetByIdAsync(Guid id)
        {
            const string query = "SELECT * FROM Addresses WHERE Id = @Id";

            try
            {
                await using var connection = new SqlConnection(_connectionString);
                var result = await connection.QueryFirstOrDefaultAsync<Address>(query,new {Id = id});
                return result;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }

        public async Task<int> AddAsync(Address address)
        {
            const string query = @"
                                INSERT INTO [dbo].[Addresses]
                                           ([Id]
                                           ,[CustomerId]
                                           ,[HouseNumber]
                                           ,[HouseName]
                                           ,[AddressLine1]
                                           ,[AddressLine2]
                                           ,[AddressLine3]
                                           ,[EirCode])
                                     VALUES
                                           (@Id
                                           ,@CustomerId
                                           ,@HouseNumber
                                           ,@HouseName
                                           ,@AddressLine1
                                           ,@AddressLine2
                                           ,@AddressLine3
                                           ,@EirCode)
";

            try
            {
                await using var connection = new SqlConnection(_connectionString);
                var result = await connection.ExecuteAsync(query, new {address.Id,address.CustomerId,address.HouseNumber,address.HouseName,address.AddressLine1,address.AddressLine2,address.AddressLine3,address.EirCode});
                return result;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }

        public async Task<int> UpdateAsync(Address address)
        {
            const string query = @"
                               UPDATE [dbo].[Addresses]
                                   SET [HouseNumber] = @HouseNumber
                                      ,[HouseName] = @HouseName
                                      ,[AddressLine1] = @AddressLine1
                                      ,[AddressLine2] = @AddressLine2
                                      ,[AddressLine3] = @AddressLine3
                                      ,[EirCode] = @EirCode
                                 WHERE Id = @Id
                                    ";
            try
            {
                await using var connection = new SqlConnection(_connectionString);
                var result = await connection.ExecuteAsync(query, new { address.Id, address.HouseNumber, address.HouseName, address.AddressLine1, address.AddressLine2, address.AddressLine3, address.EirCode });
                return result;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }

        public async Task<int> DeleteAsync(Guid id)
        {
            const string query = @"
                                DELETE FROM [dbo].[Addresses]
                                WHERE Id = @Id
                                   ";
            try
            {
                await using var connection = new SqlConnection(_connectionString);
                var result = await connection.ExecuteAsync(query, new { Id = id });
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

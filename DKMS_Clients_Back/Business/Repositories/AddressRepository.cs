using DKMS_Clients_Back.Models;
using System.Data.SqlClient;
using Dapper;
using DKMS_Clients_Back.Business.Repositories.RepositoryInterfaces;

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
    }
}

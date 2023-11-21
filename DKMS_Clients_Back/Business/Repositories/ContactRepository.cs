using DKMS_Clients_Back.Business.Repositories.RepositoryInterfaces;
using DKMS_Clients_Back.Models;
using System.Data.SqlClient;
using Dapper;

namespace DKMS_Clients_Back.Business.Repositories
{
    public class ContactRepository:IContactRepository
    {
        private readonly ILogger<CustomerRepository> _logger;
        private readonly string _connectionString;
        public ContactRepository(IConfiguration configuration, ILogger<CustomerRepository> logger)
        {
            _logger = logger;
            _connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }
        public async Task<IEnumerable<Contact>> GetAllAsync()
        {
            const string query = "SELECT * FROM Contacts";

            try
            {
                await using var connection = new SqlConnection(_connectionString);
                var results = await connection.QueryAsync<Contact>(query);
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

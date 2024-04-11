using BankManagementSystem.Data;
using System.Threading.Tasks;

namespace BankManagementSystem.Services
{
    public class AdminService
    {
        private readonly AppDbContext _dbContext;

        public AdminService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> AuthenticateAdmin(string adminPassword)
        {
            
            return await Task.FromResult(adminPassword == "admin");
        }
    }
}

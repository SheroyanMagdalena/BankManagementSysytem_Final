using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

namespace BankManagementSystem.Data
{
    public class AppDbContext : DbContext
    {
        string _connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=Banking;Trusted_Connection=True;MultipleActiveResultSets=true";

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // DbSet for your RegisterModel entity
        public DbSet<RegisterModel> RegisterModels { get; set; }


        // DbSet for your Account entity
        public DbSet<Account> Accounts { get; set; }

        private readonly AppDbContext _dbContext;

       

    }

}




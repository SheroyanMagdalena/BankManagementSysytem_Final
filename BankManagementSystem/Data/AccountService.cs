using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankManagementSystem.Data
{
    public class AccountService
    {
        private readonly AppDbContext _context;

        public AccountService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Account>> GetAllAccountsAsync()
        {
            return await _context.Accounts.ToListAsync();
        }

        public async Task TransferRegisterModelToAccountAsync()
        {
          
            var registerEntries = await _context.RegisterModels.ToListAsync();

            // Convert each RegisterModel entry to an Account object.
            var accounts = registerEntries.Select(rm => new Account
            {
               
                AccountId = rm.AccountId,
                Name = rm.Name,
                Surname = rm.Surname,
                Age = rm.Age,
                DateOfBirth = rm.DateOfBirth,
                Occupation = rm.Occupation,
                Balance = rm.Balance
               
            });

            // Add to the Accounts DbSet
            await _context.Accounts.AddRangeAsync(accounts);

            // Save the changes to the database
            await _context.SaveChangesAsync();
        }
    }
}

using BankManagementSystem.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace BankManagementSystem
{
    public class BankingFunctions
    {
        private readonly AppDbContext _dbContext;

        public BankingFunctions(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<(bool IsValid, string Message, string Color)> VerifyAccountIdAsync(string accountId)
        {
            if (string.IsNullOrWhiteSpace(accountId))
            {
                return (false, "Account ID is required.", "red");
            }

            var accountExists = await _dbContext.Accounts.AnyAsync(a => a.AccountId == accountId);
            return accountExists ? (true, "Account ID is valid.", "green") : (false, "Account ID not found.", "red");
        }

        public async Task<(string Message, string Color, decimal NewBalance)> DepositFundsAsync(string accountId, decimal depositAmount)
        {
            if (string.IsNullOrWhiteSpace(accountId))
            {
                return ("Account ID is required.", "red", 0);
            }

            var account = await _dbContext.Accounts.FirstOrDefaultAsync(a => a.AccountId == accountId);
            if (account == null)
            {
                return ("Account not found!", "red", 0);
            }

            if (depositAmount <= 0)
            {
                return ("Deposit amount must be greater than zero.", "red", account.Balance);
            }

            account.Balance += depositAmount;
            await _dbContext.SaveChangesAsync();
            return ("Deposit successful!", "green", account.Balance);
        }

        public async Task<(string Message, string Color, decimal NewBalance)> WithdrawFundsAsync(string accountId, decimal withdrawAmount)
        {
            if (string.IsNullOrWhiteSpace(accountId))
            {
                return ("Account ID is required.", "red", 0);
            }

            var account = await _dbContext.Accounts.FirstOrDefaultAsync(a => a.AccountId == accountId);
            if (account == null)
            {
                return ("Account not found!", "red", 0);
            }

            if (withdrawAmount <= 0)
            {
                return ("Withdrawal amount must be greater than zero.", "red", account.Balance);
            }

            if (account.Balance < withdrawAmount)
            {
                return ("Insufficient balance!", "red", account.Balance);
            }

            account.Balance -= withdrawAmount;
            await _dbContext.SaveChangesAsync();
            return ("Withdrawal successful!", "green", account.Balance);
        }

        public async Task<(string Message, string Color, decimal NewSourceBalance)> TransferFundsAsync(string sourceAccountId, string recipientAccountId, decimal transferAmount)
        {
            if (string.IsNullOrWhiteSpace(sourceAccountId) || string.IsNullOrWhiteSpace(recipientAccountId))
            {
                return ("Both Account ID and Recipient Account ID are required.", "red", 0);
            }

            var sourceAccount = await _dbContext.Accounts.FirstOrDefaultAsync(a => a.AccountId == sourceAccountId);
            var recipientAccount = await _dbContext.Accounts.FirstOrDefaultAsync(a => a.AccountId == recipientAccountId);

            if (sourceAccount == null || recipientAccount == null)
            {
                return ("One or both accounts not found!", "red", 0);
            }

            if (transferAmount <= 0)
            {
                return ("Transfer amount must be greater than zero.", "red", sourceAccount.Balance);
            }

            if (sourceAccount.Balance < transferAmount)
            {
                return ("Insufficient balance!", "red", sourceAccount.Balance);
            }

            sourceAccount.Balance -= transferAmount;
            recipientAccount.Balance += transferAmount;
            await _dbContext.SaveChangesAsync();
            return ("Transfer successful!", "green", sourceAccount.Balance);
        }

    }
}

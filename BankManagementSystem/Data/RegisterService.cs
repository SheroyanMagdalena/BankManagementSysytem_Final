using BankManagementSystem;
using BankManagementSystem.Data;
using System;
using System.Threading.Tasks;

public class RegisterService
{
    private readonly AppDbContext _dbContext;

    public RegisterService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    // Method to register a new user and create an account
    public async Task<string> RegisterUser(RegisterModel registerModel)
    {
        if (!Validate(registerModel))
        {
            throw new ArgumentException("Invalid registration data.");
        }

        // Generate Account ID
        string accountId = registerModel.GenerateAccountId();
        registerModel.AccountId = accountId;

        // Create a database transaction to ensure both operations are atomic
        using (var transaction = await _dbContext.Database.BeginTransactionAsync())
        {
            try
            {
                // Save user registration data to the RegisterModels table
                await _dbContext.RegisterModels.AddAsync(registerModel);
                await _dbContext.SaveChangesAsync();

                // Create an Account entity from the validated registration data
                var account = new Account
                {
                    AccountId = accountId,
                    Name = registerModel.Name,
                    Surname = registerModel.Surname,
                    Age = registerModel.Age,
                    DateOfBirth = registerModel.DateOfBirth,
                    Occupation = registerModel.Occupation,
                    Balance = registerModel.Balance
                    
                };

                // Save the account entity to the Accounts table
                await _dbContext.Accounts.AddAsync(account);
                await _dbContext.SaveChangesAsync();

                // Commit the transaction
                await transaction.CommitAsync();

                return accountId; 
            }
            catch
            {
                
                await transaction.RollbackAsync();
                throw; 
            }
        }
    }

    // Method to validate user registration data
    private bool Validate(RegisterModel registerModel)
    {
        // Check if the account type is provided
        if (string.IsNullOrWhiteSpace(registerModel.AccountType))
        {
            return false;
        }

        // Check if the name is provided
        if (string.IsNullOrWhiteSpace(registerModel.Name))
        {
            return false;
        }

        // Check if the surname is provided
        if (string.IsNullOrWhiteSpace(registerModel.Surname))
        {
            return false;
        }

        // Check if the age is valid (assuming age should be between 18 and 100)
        if (registerModel.Age < 18 || registerModel.Age > 100)
        {
            return false;
        }

        // Check if the date of birth is provided
        if (registerModel.DateOfBirth == DateTime.MinValue)
        {
            return false;
        }

        // Check if the occupation is provided
        if (string.IsNullOrWhiteSpace(registerModel.Occupation))
        {
            return false;
        }

        // Check if the balance is provided and non-negative
        if (registerModel.Balance <= 0)
        {
            return false;
        }

        // If all checks pass, return true
        return true;
    }
}

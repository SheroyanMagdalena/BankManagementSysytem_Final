using BankManagementSystem.Data;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace BankManagementSystem
{
    public class RegisterModel

    {
        public int Id { get; set; }

       [Required(ErrorMessage = "Account Type is required")]
        public string AccountType { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Surname is required")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Age is required")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Date of Birth is required")]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Occupation is required")]
        public string Occupation { get; set; }

        [Required(ErrorMessage = "Balance is required")]
        public decimal Balance { get; set; }

        public string AccountId { get; set; }

        // Method to generate AccountId based on AccountType
        public string GenerateAccountId()
        {
            Random random = new Random();
            string accountId = "";

            // Get the first three letters of the account type
            string accountTypeLetters = AccountType.Substring(0, Math.Min(AccountType.Length, 3));

            // Append five random digits
            for (int i = 0; i < 5; i++)
            {
                accountId += random.Next(0, 10);
            }

            return accountTypeLetters + accountId;
        }




    }


}

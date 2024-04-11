// Account.cs
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BankManagementSystem.Data
{
    public class Account
    {
        [Key] // Define primary key
        public int Id { get; set; }


        public string AccountId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Occupation { get; set; }
        public decimal Balance { get; set; }
    }
}


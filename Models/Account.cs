using System;

namespace AbcBankDalLayer.Models
{

    public class Account
    {
        public string AccountNumber { get; set; }
        public string AccountStatus { get; set; }
        public DateTime? AccountOpenDate { get; set; }
        public DateTime? AccountCloseDate { get; set; }
        public DateTime AccountAppliedDate { get; set; }
        public decimal Balance { get; set; }
        public AccountType AccountType { get; set; } = new AccountType();
        public BankBranch BankBranch { get; set; } = new BankBranch();
        public ClientProfile ClientProfile { get; set; } = new ClientProfile();

    }
}



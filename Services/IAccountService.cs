using AbcBankDalLayer.Models;
using System.Collections.Generic;

namespace ABCBankWebApi.Services
{
    public interface IAccountService
    {
        void AddAccount(Account account);
        IEnumerable<Account> GetAllAccounts(string status);
        Account GetAccountByAccountNumber(string stringAccountNumber);
        IEnumerable<AccountType> GetAllAccountTypes();
        IEnumerable<BankBranch> GetAllBranch();
        IEnumerable<Account> GetAccountsByClientId(long longClientId, string status);
        Account GetAppliedAccountByAccountNumber(string accountNumber);
        IEnumerable<Account> GetAllAccountStatusByClientId(long longClientId);

    }
}

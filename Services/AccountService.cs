using AbcBankDalLayer.Models;
using ABCBankWebApi.Models;
using ABCBankWebApi.Sp_Results;
using AutoMapper;
using BankDAL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace ABCBankWebApi.Services
{
    public class AccountService : IAccountService
    {
        private readonly IMapper mapper;
        public AccountService(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public void AddAccount(Account account)
        {
            Account accounts = null;
            do
            {
                account.AccountNumber = account.BankBranch.BranchCode.ToString() + account.AccountType.AccountTypeId + new Random().Next(1000000, 10000000);
                accounts = StoredProcedure.ExecuteStoredProcedureWithResult<Account>("Sp_CheckAccountNumber", new List<SqlParameter> { new SqlParameter("@AccountNumber", account.AccountNumber) }).FirstOrDefault();
            } while (accounts != null);
            List<SqlParameter> sqlParameters = new List<SqlParameter>();
            sqlParameters.Add(new SqlParameter("@AccountNumber", account.AccountNumber));
            sqlParameters.Add(new SqlParameter("@AccountTypeId", account.AccountType.AccountTypeId));
            sqlParameters.Add(new SqlParameter("@BranchCode", account.BankBranch.BranchCode));
            sqlParameters.Add(new SqlParameter("@ClientId", account.ClientProfile.ClientId));
            StoredProcedure.ExecuteStoredProcedure("Sp_AddAccount", sqlParameters);
        }

        public Account GetAccountByAccountNumber(string stringAccountNumber)
        {
            var getAccountByAccountNumberResult = StoredProcedure.ExecuteStoredProcedureWithResult<GetAccountByAccountNumberResult>("Sp_GetAccountByAccountNumber", new List<SqlParameter> { new SqlParameter("@AccountNumber", stringAccountNumber) }).FirstOrDefault();
            return mapper.Map<Account>(getAccountByAccountNumberResult);
        }


        public IEnumerable<Account> GetAllAccounts(string status)
        {
            IEnumerable<GetAllAccountsResult> accounts = StoredProcedure.ExecuteStoredProcedureWithResult<GetAllAccountsResult>("Sp_GetAllAccounts", new List<SqlParameter> { new SqlParameter("@AccountStatus", status) });
            return mapper.Map<List<Account>>(accounts);
        }

        public IEnumerable<AccountType> GetAllAccountTypes()
        {
            var accountTypeResult = StoredProcedure.ExecuteStoredProcedureWithResult<GetAllAccountTypeResult>("Sp_GetAllAccountType", null);
            return mapper.Map<List<AccountType>>(accountTypeResult);
        }

        public IEnumerable<BankBranch> GetAllBranch()
        {
            var allbranchResult = StoredProcedure.ExecuteStoredProcedureWithResult<GetAllBranchResult>("Sp_GetAllBranch", null);
            return mapper.Map<List<BankBranch>>(allbranchResult);
        }

        public IEnumerable<Account> GetAccountsByClientId(long longClientId, string status)
        {
            var getAccountByClientIdResult = StoredProcedure.ExecuteStoredProcedureWithResult<GetAccountsByClientIdResult>("Sp_GetAccountsByClientId", new List<SqlParameter> { new SqlParameter("@ClientId", longClientId), new SqlParameter("@Status", status) });
            return mapper.Map<List<Account>>(getAccountByClientIdResult);
        }

        public Account GetAppliedAccountByAccountNumber(string accountNumber)
        {
            var getAccountByAccountNumberResult = StoredProcedure.ExecuteStoredProcedureWithResult<GetAppliedAccountByAccountNumberResult>("Sp_GetAppliedAccountByAccountNumber", new List<SqlParameter> { new SqlParameter("@AccountNumber", accountNumber) }).FirstOrDefault();
            return mapper.Map<Account>(getAccountByAccountNumberResult);
        }








    }
}

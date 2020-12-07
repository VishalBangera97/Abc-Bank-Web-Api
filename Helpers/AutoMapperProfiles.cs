using AbcBankDalLayer.Models;
using ABCBankWebApi.Models;
using ABCBankWebApi.Sp_Results;
using AutoMapper;

namespace ABCBankWebApi.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<GetAllAccountsResult, Account>()
                  .AfterMap((account, accountType) =>
                {
                    accountType.AccountType = new AccountType { AccountTypeName = account.AccountTypeName };
                    accountType.ClientProfile = new ClientProfile { Name = account.ClientName };
                    accountType.BankBranch = new BankBranch { BranchName = account.BranchName };
                });
            CreateMap<LoginEntryResult, ClientProfile>()
                  .AfterMap((result, clientProfile) =>
                clientProfile.ClientLogin = new ClientLogin { LoginId = result.LoginId });
            CreateMap<LoginEntryResult, AdminProfile>()
                  .AfterMap((result, adminProfile) =>
                adminProfile.AdminLogin = new AdminLogin { LoginId = result.LoginId });
            CreateMap<ClientLoginResult, ClientProfile>();
            CreateMap<AdminLoginResult, AdminProfile>();
            CreateMap<LastLoginResult, ClientProfile>()
            .AfterMap((lastLogin, clientProfile) =>
            clientProfile.ClientLogin = new ClientLogin { LoginDate = lastLogin.LoginDate, LogoutDate = lastLogin.LogoutDate });
            CreateMap<LastLoginResult, AdminProfile>()
                 .AfterMap((lastLogin, adminProfile) =>
            adminProfile.AdminLogin = new AdminLogin { LoginDate = lastLogin.LoginDate, LogoutDate = lastLogin.LogoutDate });
            CreateMap<GetClientByClientIdResult, ClientProfile>();
            CreateMap<GetAdminByAdminIdResult, AdminProfile>();
            CreateMap<GetAllAccountTypeResult, AccountType>();
            CreateMap<GetAllBranchResult, BankBranch>();
            CreateMap<GetClientBasedOnClientStatusResult, ClientProfile>();
            CreateMap<GetAccountsByClientIdResult, Account>()
                .AfterMap((getAccount, account) => account.AccountType = new AccountType { AccountTypeName = getAccount.AccountTypeName });
            CreateMap<GetAccountByAccountNumberResult, Account>()
                .AfterMap((accountResult, account) =>
                {
                    account.AccountType = new AccountType { AccountTypeName = accountResult.AccountTypeName };
                    account.BankBranch = new BankBranch { BranchName = accountResult.BranchName, IFSC = accountResult.IFSC };
                    account.ClientProfile = new ClientProfile { Name = accountResult.ClientName };
                });
            CreateMap<GetAppliedAccountByAccountNumberResult, Account>()
               .AfterMap((accountResult, account) =>
               {
                   account.AccountType = new AccountType { AccountTypeName = accountResult.AccountTypeName };
                   account.BankBranch = new BankBranch { BranchName = accountResult.BranchName };
                   account.ClientProfile = new ClientProfile { Name = accountResult.ClientName };
               });
            CreateMap<GetAccountSummaryResult, Transaction>()
                .AfterMap((accountSummary, transaction) => transaction.Account = new Account { Balance = accountSummary.Balance });
            CreateMap<GetAllTransactionByAccountNumberResult, Transaction>()
                .AfterMap((transactions, transaction) =>
                {
                    transaction.Account = new Account { AccountNumber = transactions.AccountNumber };
                    transaction.ReceiverBank = new ReceiverBank { ReceiverBankName = transactions.ReceiverBankName };
                    transaction.TransactionMode = new TransactionMode { TransactionInstrument = transactions.TransactionInstrument };
                });
        }
    }
}

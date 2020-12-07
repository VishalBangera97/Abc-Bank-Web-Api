using AbcBankDalLayer.Models;
using ABCBankWebApi.Sp_Results;
using AutoMapper;
using BankDAL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace ABCBankWebApi.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IMapper mapper;

        public TransactionService(IMapper mapper)
        {
            this.mapper = mapper;
        }
        public long AddTransaction(Transaction transaction)
        {
            string stringFlag = "false";
            var getAccountByAccountNumberResult = StoredProcedure.ExecuteStoredProcedureWithResult<GetAccountByAccountNumberResult>("Sp_GetAccountByAccountNumber", new List<SqlParameter> { new SqlParameter("@AccountNumber", transaction.ReceiverAccountNumber) }).FirstOrDefault();
            if (getAccountByAccountNumberResult != null)
                if (getAccountByAccountNumberResult.IFSC != transaction.ReceiverBank.ReceiverBankIFSC)
                    throw new ArgumentException("Invalid Bank Details");
                else
                    stringFlag = "true";
            var getBankNameByIFSCResult = StoredProcedure.ExecuteStoredProcedureWithResult<GetBankNameFromIFSCResult>("Sp_GetBankNameFromIFSC", new List<SqlParameter> { new SqlParameter("@IFSC", transaction.ReceiverBank.ReceiverBankIFSC) }).FirstOrDefault();
            if (getBankNameByIFSCResult == null || (getBankNameByIFSCResult.ReceiverBankName == "ABC Bank" && stringFlag == "false"))
                throw new ArgumentException("Invalid Bank Details");
            var getBalanceByAccountNumberResult = StoredProcedure.ExecuteStoredProcedureWithResult<GetBalanceByAccountNumberResult>("Sp_GetBalanceByAccountNumber", new List<SqlParameter> { new SqlParameter("@AccountNumber", transaction.Account.AccountNumber) }).FirstOrDefault();
            transaction.Status = transaction.Amount > getBalanceByAccountNumberResult.Balance ? "failed" : "success";
            var sqlParameters = new List<SqlParameter>();
            sqlParameters.Add(new SqlParameter("@Amount", transaction.Amount));
            sqlParameters.Add(new SqlParameter("@AccountNumber", transaction.Account.AccountNumber));
            sqlParameters.Add(new SqlParameter("@ReceiverAccountNumber", transaction.ReceiverAccountNumber));
            sqlParameters.Add(new SqlParameter("@TransactionModeId", 4));
            sqlParameters.Add(new SqlParameter("@ReceiverBankIFSC", transaction.ReceiverBank.ReceiverBankIFSC));
            sqlParameters.Add(new SqlParameter("@ReceiverName", transaction.ReceiverName));
            sqlParameters.Add(new SqlParameter("@Status", transaction.Status));
            sqlParameters.Add(new SqlParameter("@Flag", stringFlag));
            sqlParameters.Add(new SqlParameter("@SenderBankIFSC", transaction.Account.BankBranch.IFSC));
            sqlParameters.Add(new SqlParameter("@SenderName", transaction.Account.ClientProfile.Name));
            return StoredProcedure.ExecuteStoredProcedureWithResult<DoTransaction>("Sp_DoTransaction", sqlParameters).FirstOrDefault().TransactionId; ;
        }

        public IEnumerable<Transaction> GetAccountSummary(string accountNumber, DateTime beginDate, DateTime endDate)
        {
            var getAccountSummaryResult = StoredProcedure.ExecuteStoredProcedureWithResult<GetAccountSummaryResult>("Sp_GetAccountStatementByAccountNumber", new List<SqlParameter> { new SqlParameter("@AccountNumber", accountNumber), new SqlParameter("@BeginDate", beginDate), new SqlParameter("@EndDate", endDate) });
            return mapper.Map<IEnumerable<Transaction>>(getAccountSummaryResult);
        }

        public IEnumerable<Transaction> GetAllTransactionByAccountNumber(string stringAccountNumber)
        {
            var getAllTransactionByAccountNumberResult = StoredProcedure.ExecuteStoredProcedureWithResult<GetAllTransactionByAccountNumberResult>("Sp_GetAllTransactionByAccountNumber", new List<SqlParameter> { new SqlParameter("@AccountNumber", stringAccountNumber) });
            return mapper.Map<IEnumerable<Transaction>>(getAllTransactionByAccountNumberResult);
        }

        public Transaction GetTransactionByTransactionId(long longTransactionId)
        {
            var getTransactionByTransactionIdResult = StoredProcedure.ExecuteStoredProcedureWithResult<GetAllTransactionByAccountNumberResult>("Sp_GetTransactionByTransactionId", new List<SqlParameter> { new SqlParameter("@TransactionId", longTransactionId) });
            return mapper.Map<Transaction>(getTransactionByTransactionIdResult.FirstOrDefault());
        }

        public IEnumerable<Transaction> GetLastThreeTransactions(long longAccountNumber)
        {
            var getLastThreeTransactionResult = StoredProcedure.ExecuteStoredProcedureWithResult<GetAllTransactionByAccountNumberResult>("Sp_GetLastThreeTransactions", new List<SqlParameter> { new SqlParameter("@AccountNumber", longAccountNumber) });
            return mapper.Map<IEnumerable<Transaction>>(getLastThreeTransactionResult);
        }
    }
}

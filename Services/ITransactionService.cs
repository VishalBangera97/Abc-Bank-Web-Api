using AbcBankDalLayer.Models;
using System;
using System.Collections.Generic;

namespace ABCBankWebApi.Services
{
    public interface ITransactionService
    {
        long AddTransaction(Transaction transaction);
        IEnumerable<Transaction> GetAllTransactionByAccountNumber(string stringAccountNumber);
        IEnumerable<Transaction> GetAccountSummary(string accountNumber, DateTime beginDate, DateTime endDate);
        Transaction GetTransactionByTransactionId(long longTransactionId);
        IEnumerable<Transaction> GetLastThreeTransactions(long longAccountNumber);

    }
}

using System;

namespace ABCBankWebApi.Sp_Results
{
    public class GetAccountSummaryResult
    {
        public long TransactionId { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
        public string TransactionType { get; set; }
        public decimal Balance { get; set; }
    }
}

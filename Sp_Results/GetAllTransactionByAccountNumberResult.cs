using System;

namespace ABCBankWebApi.Sp_Results
{
    public class GetAllTransactionByAccountNumberResult
    {
        public string AccountNumber { get; set; }
        public decimal Amount { set; get; }
        public DateTime Date { set; get; }
        public string ReceiverAccountNumber { set; get; }
        public string ReceiverBankName { set; get; }
        public string ReceiverName { set; get; }
        public string Status { set; get; }
        public string TransactionInstrument { set; get; }
        public string TransactionType { set; get; }
        public string SenderBankIFSC { set; get; }
        public string SenderName { set; get; }
        public string SenderAccountNumber { set; get; }

    }
}

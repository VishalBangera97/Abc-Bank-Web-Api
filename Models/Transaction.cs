using System;

namespace AbcBankDalLayer.Models
{
    public class Transaction
    {
        public long TransactionId { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string TransactionType { get; set; }
        public string ReceiverAccountNumber { get; set; }
        public string ReceiverName { get; set; }
        public string Status { get; set; }
        public string SenderName { get; set; }
        public string SenderAccountNumber { get; set; }
        public string SenderBankIFSC { get; set; }
        public ReceiverBank ReceiverBank { get; set; } = new ReceiverBank();
        public TransactionMode TransactionMode { get; set; } = new TransactionMode();
        public Account Account { get; set; } = new Account();
    }
}

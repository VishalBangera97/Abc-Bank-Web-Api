using System;

namespace AbcBankDalLayer.Models
{
    public class ChequeBook
    {
        public long ChequeBookNumber { get; set; }
        public string AccountNumber { get; set; }
        public DateTime ChequeBookIssuedDate { get; set; }
        public long LeafCountId { get; set; }
    }
}

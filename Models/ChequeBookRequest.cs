using System;

namespace AbcBankDalLayer.Models
{
    public class ChequeBookRequest
    {
        public long ChequeBookRequestId { get; set; }

        public int ClientId { get; set; }
        public string AccountNumber { get; set; }
        public string Status { get; set; }
        public DateTime RequestDate { get; set; }



    }
}

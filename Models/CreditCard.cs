using System;

namespace AbcBankDalLayer.Models
{
    public class CreditCard
    {
        public string CreditCardNumber { get; set; }
        public short CreditCardTypeId { get; set; }
        public short CreditCardCvv { get; set; }
        public DateTime CreditCardExpiryDate { get; set; }
    }
}

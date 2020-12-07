using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AbcBankDalLayer.Models
{
    public class DebitCard
    {
        public string DebitCardNumber { get; set; }
        public short DebitCardTypeId { get; set; }
        public short DebitCardCvv { get; set; }
        public DateTime DebitCardExpiryDate { get; set; }
    }
}

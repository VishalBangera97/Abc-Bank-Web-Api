using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AbcBankDalLayer.Models
{
    public class ClientLogin
    {
        public long LoginId { get; set; }
        public long ClientId { get; set; }
        public DateTime LoginDate { get; set; }
        public DateTime? LogoutDate { get; set; }
    }
}

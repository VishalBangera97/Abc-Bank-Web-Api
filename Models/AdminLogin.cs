using System;

namespace AbcBankDalLayer.Models
{
    public class AdminLogin
    {
        public long LoginId { get; set; }
        public long AdminId { get; set; }
        public DateTime LoginDate { get; set; }
        public DateTime? LogoutDate { get; set; }
    }
}

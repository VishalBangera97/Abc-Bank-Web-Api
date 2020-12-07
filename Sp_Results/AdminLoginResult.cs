using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ABCBankWebApi.Sp_Results
{
    public class AdminLoginResult
    {
        public long AdminId { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}

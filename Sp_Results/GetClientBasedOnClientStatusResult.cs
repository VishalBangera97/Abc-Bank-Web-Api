using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ABCBankWebApi.Sp_Results
{
    public class GetClientBasedOnClientStatusResult
    {
        public long ClientId { get; set; }
        public string Name { get; set; }
        public string ClientStatus { get; set; }
    }
}

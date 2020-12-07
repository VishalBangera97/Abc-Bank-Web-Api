using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ABCBankWebApi.Sp_Results
{
    public class GetAppliedAccountByAccountNumberResult
    {
        public DateTime AccountCloseDate { get; set; }
        public string AccountNumber { get; set; }
        public DateTime AccountOpenDate { get; set; }
        public string AccountStatus { get; set; }
        public DateTime AccountAppliedDate { get; set; }
        public string AccountTypeName { get; set; }
        public string BranchName { get; set; }
      
        public string ClientName { get; set; }
    }
}

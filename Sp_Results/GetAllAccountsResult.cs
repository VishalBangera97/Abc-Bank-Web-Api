using System;

namespace ABCBankWebApi.Models
{
    public class GetAllAccountsResult
    {
        public DateTime? AccountCloseDate { get; set; }
        public long AccountNumber { get; set; }
        public DateTime? AccountOpenDate { get; set; }
        public string AccountStatus { get; set; }
        public string AccountTypeName { get; set; }
        public string BranchName { get; set; }
        public DateTime AccountAppliedDate { get; set; }
        public string ClientName { get; set; }
    }
}

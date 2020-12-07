namespace AbcBankDalLayer.Models
{
    public class AccountType
    {
        public int AccountTypeId { get; set; }
        public string AccountTypeName { get; set; }
        public float? InterestRate { get; set; }
        public long? MinimumBalance { get; set; }
        public long? MaximumWithdrawalAmount { get; set; }
        public short? MaximumTransactionCount { get; set; }
        public double MonthlyMaintinanceCharge { get; set; }
        public double MinimumBalanceMaintananceCharge { get; set; }


    }
}

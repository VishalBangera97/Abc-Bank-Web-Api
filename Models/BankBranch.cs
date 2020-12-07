namespace AbcBankDalLayer.Models
{
    public class BankBranch
    {
        public int BranchCode { get; set; }
        public string BranchName { get; set; }
        public string BranchAddress { get; set; }
        public long MICRCode { get; set; }
        public string IFSC { get; set; }
    }
}

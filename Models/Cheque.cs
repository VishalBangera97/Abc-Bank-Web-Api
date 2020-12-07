namespace AbcBankDalLayer.Models
{
    public class Cheque
    {
        public long ChequeNumber { get; set; }
        public long ChequeBookNumber { get; set; }
        public long MICRCode { get; set; }
        public string ChequeStatus { get; set; }
    }
}

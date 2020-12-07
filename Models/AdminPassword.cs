namespace AbcBankDalLayer.Models
{
    public class AdminPassword
    {
        public long AdminId { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}

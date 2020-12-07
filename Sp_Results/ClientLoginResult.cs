namespace ABCBankWebApi.Sp_Results
{
    public class ClientLoginResult
    {
        public long ClientId { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}

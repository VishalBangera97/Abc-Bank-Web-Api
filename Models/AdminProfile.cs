using System;

namespace AbcBankDalLayer.Models
{
    public class AdminProfile
    {
        public long AdminId { get; set; }
        public string Name { get; set; }
        public DateTime Dob { get; set; }
        public string PhoneNumber { get; set; }
        public long AdhaarNumber { get; set; }
        public string Email { get; set; }
        public string AdminStatus { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Password { get; set; }
        public string PanCardNumber { get; set; }
        public string Address { get; set; }
        public string CityName { get; set; }
        public int ZipCode { get; set; }
        public string Gender { get; set; }
        public DateTime AdminActivationDate { get; set; }
        public DateTime? AdminDeactivationDate { get; set; }
        public AdminLogin AdminLogin { get; set; }
    }
}

using System;

namespace ABCBankWebApi.Sp_Results
{
    public class GetClientByClientIdResult
    {

        public long ClientId { get; set; }
        public string Name { get; set; }
        public DateTime Dob { get; set; }
        public string PhoneNumber { get; set; }
        public long AdhaarNumber { get; set; }
        public string Email { get; set; }
        public string NomineeName { get; set; }
        public string ClientStatus { get; set; }
        public string PanCardNumber { get; set; }
        public string NomineeRelation { get; set; }
        public long NomineeAdhaarNumber { get; set; }
        public string Address { get; set; }
        public string CityName { get; set; }
        public int ZipCode { get; set; }
        public string Gender { get; set; }
        public DateTime ClientRegistrationDate { get; set; }
        public DateTime? ClientActivationDate { get; set; }
        public DateTime? ClientDeactivationDate { get; set; }

        
    }
}

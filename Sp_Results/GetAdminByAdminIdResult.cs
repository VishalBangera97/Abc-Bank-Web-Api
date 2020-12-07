using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ABCBankWebApi.Sp_Results
{
    public class GetAdminByAdminIdResult
    {

        public long AdminId { get; set; }
        public string Name { get; set; }
        public DateTime Dob { get; set; }
        public string PhoneNumber { get; set; }
        public long AdhaarNumber { get; set; }
        public string Email { get; set; }
       
        public string AdminStatus { get; set; }
        public string PanCardNumber { get; set; }
       
        public string Address { get; set; }
        public string CityName { get; set; }
        public int ZipCode { get; set; }
        public string Gender { get; set; }
      
        public DateTime? AdminActivationDate { get; set; }
        public DateTime? AdminDeactivationDate { get; set; }
    }
}

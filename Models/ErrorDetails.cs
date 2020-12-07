using Newtonsoft.Json;
using System.Net;

namespace ABCBankWebApi.Models
{
    public class ErrorDetails
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }

        public ErrorDetails() { }

        public ErrorDetails(string message, HttpStatusCode statusCode)
        {
            Message = message;
            StatusCode = (int)statusCode;

        }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}

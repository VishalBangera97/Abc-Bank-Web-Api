using AbcBankDalLayer.Models;
using ABCBankWebApi.Helpers;
using ABCBankWebApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace AbcBankDalLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientService clientService;
        public ClientController(IClientService clientService)
        {
            this.clientService = clientService;
        }


        [HttpPut("SetClientId/{token}")]
        public IActionResult SetClientId(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token) as JwtSecurityToken;
            var stringClientId = jsonToken.Claims.First().Value;
            var longClientId = Int64.Parse(stringClientId);
            if (clientService.GetClientByClientId(longClientId) != null)
            {
                HttpContext.Session.Set("clientId", BitConverter.GetBytes(longClientId));
                return Ok();
            }
            else
            {
                return StatusCode(404);
            }
        }

        public long getClientId()
        {
            var clientId = HttpContext.Session.Get("clientId");
            return BitConverter.ToInt64(clientId);
        }

        [HttpDelete("ClearClientId")]
        public void ClearClientId()
        {
            HttpContext.Session.Clear();
        }

        [HttpPost("AddClient")]
        public IActionResult AddClient(ClientProfile client)
        {
            try
            {
                LogTraceFactory.LogInfo("Registering Client");
                clientService.AddClient(client);
                return StatusCode(201);
            }
            catch (Exception ex)
            {
                LogTraceFactory.LogError(400, ex.Message);
                return StatusCode(400, ex.Message);
            }
        }

        [HttpPost("ClientLogin")]
        public IActionResult ClientLogin(ClientProfile clientProfile)
        {
            try
            {
                LogTraceFactory.LogInfo("Logging in Client With Client Name " + clientProfile.Name);
                return Ok(clientService.ClientLogin(clientProfile));
            }
            catch (Exception ex)
            {

                LogTraceFactory.LogError(401, ex.Message);
                return StatusCode(401, ex.Message);
            }
        }


        [HttpGet("GetClient")]
        public IActionResult GetClientByClientId()
        {
            try
            {
                LogTraceFactory.LogInfo("Getting Client details with Client ID " + getClientId());
                var client = clientService.GetClientByClientId(getClientId());
                if (client == null)
                {
                    LogTraceFactory.LogInfo("No client found with Client  ID " + getClientId());
                    return StatusCode(404, "Client Not Found");
                }
                return Ok(client);
            }
            catch (Exception ex)
            {
                LogTraceFactory.LogError(500, ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("ClientLogout/{longLoginId}")]
        public IActionResult ClientLogout(long longLoginId)
        {
            try
            {
                LogTraceFactory.LogInfo("Logging out Client with Client Id " + longLoginId);
                clientService.ClientLogout(longLoginId);
                return Ok();
            }
            catch (Exception ex)
            {
                LogTraceFactory.LogError(401, ex.Message);
                return StatusCode(401, ex.Message);
            }
        }

        [HttpGet("GetClientsBasedOnStatus/{stringStatus}")]
        public IActionResult GetClientsBasedOnStatus(string stringStatus)
        {
            try
            {
                LogTraceFactory.LogInfo("Getting " + stringStatus + " clients");
                var clients = clientService.GetClientsBasedOnStatus(stringStatus);
                if (!clients.GetEnumerator().MoveNext())
                {
                    LogTraceFactory.LogInfo("No " + stringStatus + " clients found");
                    return StatusCode(404, "No clients found");
                }
                return Ok(clients);
            }
            catch (Exception ex)
            {

                LogTraceFactory.LogError(500, ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("ChangePhoneNumber/{longclientId}/{stringPhoneNumber}")]
        public IActionResult ChangePhoneNumber(long longClientId, string stringPhoneNumber)
        {
            try
            {
                LogTraceFactory.LogInfo("Changing Client Phone number with client id " + longClientId);
                clientService.ChangePhoneNumber(longClientId, stringPhoneNumber);
                return Ok();
            }
            catch (Exception ex)
            {
                LogTraceFactory.LogError(403, ex.Message);
                return StatusCode(403, ex.Message);
            }
        }

    }
}
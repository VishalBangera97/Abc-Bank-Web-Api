using AbcBankDalLayer.Models;
using ABCBankWebApi.Helpers;
using ABCBankWebApi.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace AbcBankDalLayer.Cont
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


        [HttpGet("GetClient/{longClientId}")]
        public IActionResult GetClientByClientId(long longClientId)
        {
            try
            {
                LogTraceFactory.LogInfo("Getting Client details with Client ID " + longClientId);
                var client = clientService.GetClientByClientId(longClientId);
                if (client == null)
                {
                    LogTraceFactory.LogInfo("No client found with Client  ID " + longClientId);
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
                if (clients == null)
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
    }
}
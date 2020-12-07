using AbcBankDalLayer.Models;
using ABCBankWebApi.Helpers;
using ABCBankWebApi.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ABCBankWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService adminService;
        public AdminController(IAdminService adminService)
        {
            this.adminService = adminService;
        }

        [HttpPost("AddAdmin")]
        public IActionResult AddAdmin(AdminProfile adminProfile)
        {
            try
            {
                LogTraceFactory.LogInfo("Registering new Admin with Name " + adminProfile.Name);
                adminService.AddAdmin(adminProfile);
                return StatusCode(201);
            }
            catch (Exception ex)
            {
                LogTraceFactory.LogError(400, ex.Message);
                return StatusCode(400, ex.Message);
            }
        }

        [HttpPost("AdminLogin")]
        public IActionResult AdminLogin(AdminProfile adminProfile)
        {
            try
            {
                LogTraceFactory.LogInfo("Logging Admin with Email " + adminProfile.Email);
                return Ok(adminService.AdminLogin(adminProfile));
            }
            catch (Exception ex)
            {
                LogTraceFactory.LogError(401, ex.Message);
                return StatusCode(401, ex.Message);

            }
        }

        [HttpPut("AdminLogout/{longAdminId}")]
        public IActionResult AdminLogout(long longAdminId)
        {
            try
            {
                LogTraceFactory.LogInfo("Logging out Admin with Admin ID " + longAdminId);
                adminService.AdminLogout(longAdminId);
                return Ok();
            }
            catch (Exception ex)
            {
                LogTraceFactory.LogError(401, ex.Message);
                return StatusCode(401, ex.Message);
            }
        }

        [HttpGet("GetAdminByAdminId/{longAdminId}")]
        public IActionResult GetAdmin(long longAdminId)
        {
            try
            {
                LogTraceFactory.LogInfo("Get Admin with Admin ID " + longAdminId);
                var admin = adminService.GetAdminByAdminId(longAdminId);
                if (admin == null)
                {
                    LogTraceFactory.LogInfo("Admin with Admin ID " + longAdminId+" Not found " );
                    return StatusCode(404, "Admin Not found");
                }
                return Ok(admin);
            }
            catch (Exception ex)
            {

                LogTraceFactory.LogError(500, ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("ApproveClient/{longClientId}/{stringStatus}")]
        public IActionResult ApproveClient(long longClientId, string stringStatus)
        {
            try
            {
                LogTraceFactory.LogInfo("Admin Approving Client with Client ID " + longClientId);
                adminService.ApproveClient(longClientId, stringStatus);
                return Ok();
            }
            catch (Exception ex)
            {
                LogTraceFactory.LogError(401, ex.Message);
                return StatusCode(401, ex.Message);
            }
        }

        [HttpPut("ApproveAccount/{stringAccountNumber}/{stringStatus}/{decimalBalance}")]
        public IActionResult ApproveAccount(string stringAccountNumber, string stringStatus, decimal decimalBalance)
        {
            try
            {
                LogTraceFactory.LogInfo("Admin " + stringStatus + " Client Account with Account Number " + stringAccountNumber);
                adminService.ApproveAccount(stringAccountNumber, stringStatus, decimalBalance);
                return Ok();
            }
            catch (Exception ex)
            {

                LogTraceFactory.LogError(401, ex.Message);
                return StatusCode(401, ex.Message);
            }
        }

        [HttpPut("CloseAccount/{stringAccountNumber}")]
        public IActionResult CloseAccount(string stringAccountNumber)
        {
            try
            {
                LogTraceFactory.LogInfo("Admin Closing Account with Account Number " + stringAccountNumber);
                adminService.CloseAccount(stringAccountNumber);
                return Ok();
            }
            catch (Exception ex)
            {
                LogTraceFactory.LogError(500, ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

    }
}

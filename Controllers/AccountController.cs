using AbcBankDalLayer.Models;
using ABCBankWebApi.Helpers;
using ABCBankWebApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ABCBankWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService service;
        public AccountController(IAccountService service)
        {
            this.service = service;
        }

        [HttpGet("GetAccounts/{status}")]
        public IActionResult GetAllAccounts(string status)
        {
            try
            {
                LogTraceFactory.LogInfo("Getting all" + status + " Account Details");
                var accounts = service.GetAllAccounts(status);
                if (!accounts.GetEnumerator().MoveNext())
                {
                    LogTraceFactory.LogInfo("No " + status + " Accounts found");
                    return StatusCode(404, "No accounts Found");
                }
                return Ok(accounts);
            }
            catch (Exception ex)
            {
                LogTraceFactory.LogError(500, ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("AddAccount")]
        public IActionResult AddAccount(Account account)
        {
            try
            {
                LogTraceFactory.LogInfo("Adding New " + account.AccountType + " Account");
                service.AddAccount(account);
                return Ok();
            }
            catch (Exception ex)
            {
                LogTraceFactory.LogError(400, ex.Message);
                return StatusCode(400, ex.Message);
            }
        }

        [HttpGet("GetAllAccountType")]
        public IActionResult GetAllAccountType()
        {
            try
            {
                LogTraceFactory.LogInfo("Getting All Account Details");
                var accountTypes = service.GetAllAccountTypes();
                if (!accountTypes.GetEnumerator().MoveNext())
                {
                    LogTraceFactory.LogInfo("No Account Types found");
                    return StatusCode(404, "No Account Types found");
                }
                return Ok(accountTypes);
            }
            catch (Exception ex)
            {
                LogTraceFactory.LogError(500, ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("GetAllBranch")]
        public IActionResult GetAllBranch()
        {
            try
            {
                LogTraceFactory.LogInfo("Getting All Branches");
                var branches = service.GetAllBranch();
                if (!branches.GetEnumerator().MoveNext())
                {
                    LogTraceFactory.LogInfo("Getting All Branches");
                    return StatusCode(404, "No branches found");
                }
                return Ok(branches);
            }
            catch (Exception ex)
            {
                LogTraceFactory.LogError(500, ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("GetAccountsByClientId/{status}")]
        public IActionResult GetAccountsByClientId(string status)
        {
            try
            {
                var longClientId = GetClientId();
                LogTraceFactory.LogInfo("Getting all Client Accounts with Client ID " + longClientId);
                var accounts = service.GetAccountsByClientId(longClientId, status);
                if (!accounts.GetEnumerator().MoveNext())
                {
                    LogTraceFactory.LogInfo("No Accounts found for Client ID " + longClientId);
                    return StatusCode(404, "No Accounts Found");
                }
                return Ok(accounts);
            }
            catch (Exception ex)
            {
                LogTraceFactory.LogError(500, ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("GetAccountByAccountNumber/{stringAccountNumber}")]
        public IActionResult GetAccountByAccountNumber(string stringAccountNumber)
        {
            try
            {
                LogTraceFactory.LogInfo("Getting Account with AccountNumber " + stringAccountNumber);
                var account = service.GetAccountByAccountNumber(stringAccountNumber);
                if (account == null)
                {
                    LogTraceFactory.LogInfo("No account found for account Number " + stringAccountNumber);
                    return StatusCode(404, "No account found for account Number");
                }
                return Ok(account);
            }
            catch (Exception ex)
            {
                LogTraceFactory.LogError(500, ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("GetAppliedAccountByAccountNumber/{stringAccountNumber}")]
        public IActionResult GetAppliedAccountByAccountNumber(string stringAccountNumber)
        {
            try
            {
                LogTraceFactory.LogInfo("Getting Applied Account With AccountNumber " + stringAccountNumber);
                var account = service.GetAppliedAccountByAccountNumber(stringAccountNumber);
                if (account == null)
                {
                    LogTraceFactory.LogInfo("No Applied Account found With AccountNumber " + stringAccountNumber);
                    return StatusCode(404, "No accounts found");
                }
                return Ok(account);
            }
            catch (Exception ex)
            {
                LogTraceFactory.LogError(500, ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("GetAllAccountStatusByClientId")]
        public IActionResult GetAllAccountStatusByClientId()
        {
            try
            {
                var accounts = service.GetAllAccountStatusByClientId(GetClientId());
                if (!accounts.GetEnumerator().MoveNext())
                {
                    return StatusCode(404, "No Accounts Found");
                }
                return Ok(accounts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        public long GetClientId()
        {
            return BitConverter.ToInt64(HttpContext.Session.Get("clientId"));
        }
    }
}
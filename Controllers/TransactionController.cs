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
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            this.transactionService = transactionService;
        }

        [HttpPost("AddTransaction")]
        public IActionResult AddTransaction(Transaction transaction)
        {
            try
            {
                LogTraceFactory.LogInfo("Adding Transaction with Account Number " + transaction.Account.AccountNumber);
                return StatusCode(201, transactionService.AddTransaction(transaction));
            }
            catch (Exception ex)
            {
                LogTraceFactory.LogError(400, ex.Message);
                return StatusCode(400, ex.Message);
            }
        }

        [HttpGet("GetAccountSummary/{stringAccountNumber}/{beginDate}/{endDate}")]
        public IActionResult GetAccountSummary(string stringAccountNumber, DateTime beginDate, DateTime endDate)
        {
            try
            {
                LogTraceFactory.LogInfo("Getting Account Summary for Account Number " + stringAccountNumber);
                var accountSummary = transactionService.GetAccountSummary(stringAccountNumber, beginDate, endDate);
                if (!accountSummary.GetEnumerator().MoveNext())
                {
                    LogTraceFactory.LogInfo("No Account Summary found for Account Number " + stringAccountNumber);
                    return StatusCode(404, "No account Summary found");
                }
                return Ok(accountSummary);
            }
            catch (Exception ex)
            {
                LogTraceFactory.LogError(500, ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("GetAllTransactions/{stringAccountNumber}")]
        public IActionResult GetAllTransactionByAccountNumber(string stringAccountNumber)
        {
            try
            {
                LogTraceFactory.LogInfo("Getting all transaction by Account Number " + stringAccountNumber);
                var transactions = transactionService.GetAllTransactionByAccountNumber(stringAccountNumber);
                if (!transactions.GetEnumerator().MoveNext())
                {
                    LogTraceFactory.LogInfo("No transactions found by Account Number " + stringAccountNumber);
                    return StatusCode(404, "No transactions found");
                }
                return Ok(transactions);
            }
            catch (Exception ex)
            {
                LogTraceFactory.LogError(500, ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("GetTransaction/{longTransactionId}")]
        public IActionResult GetTransactionByTransactionId(long longTransactionId)
        {
            try
            {
                LogTraceFactory.LogInfo("Getting Transaction by Transaction Id " + longTransactionId);
                var transaction = transactionService.GetTransactionByTransactionId(longTransactionId);
                if (transaction == null)
                {
                    LogTraceFactory.LogInfo("No Transaction found with Transaction Id " + longTransactionId);
                    return StatusCode(404, "Mo transaction found");
                }
                return Ok(transaction);
            }
            catch (Exception ex)
            {
                LogTraceFactory.LogError(404, ex.Message);
                return StatusCode(404, ex.Message);
            }
        }

        [HttpGet("GetLastTenTransactions/{stringAccountType}")]
        public IActionResult GetLastTenTransactions(string stringAccountType)
        {
            try
            {
                LogTraceFactory.LogInfo("Getting Last 10 transactions of Client");
                var longClientId = GetClientId();
                var transactions = transactionService.GetLastTenTransactions(longClientId,stringAccountType);
                if (!transactions.GetEnumerator().MoveNext())
                {
                    LogTraceFactory.LogInfo("No transactions found ");
                    return StatusCode(404, "No transactions found");
                }
                return Ok(transactions);
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

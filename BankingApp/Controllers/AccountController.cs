using BankingApp.BLL.Services;
using BankingApp.DAL.Models;
using BankingApp.DTO;
using Microsoft.AspNetCore.Mvc;

namespace BankingApp.Controllers
{
    [Route("api/accounts")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        public async Task<ActionResult<Account>> CreateAccount([FromBody] CreateAccountDto createAccountDto)
        {
            if (createAccountDto.InitialBalance <= 0)
            {
                return BadRequest("Initial balance must be greater than zero.");
            }

            var account = await _accountService.CreateAccount(createAccountDto.Owner, createAccountDto.InitialBalance);
            return CreatedAtAction(nameof(GetAccountDetails), new { accountNumber = account.AccountNumber }, account);
        }

        [HttpGet("{accountNumber}")]
        public async Task<ActionResult<Account>> GetAccountDetails(string accountNumber)
        {
            var account = await _accountService.GetAccountDetails(accountNumber);
            if (account == null)
            {
                return NotFound("Account not found.");
            }

            return Ok(account);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Account>>> GetAllAccounts()
        {
            var accounts = await _accountService.GetAllAccounts();
            return Ok(accounts);
        }
    }
}

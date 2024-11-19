using BankingApp.BLL.Services;
using BankingApp.DTO;
using Microsoft.AspNetCore.Mvc;

namespace BankingApp.Controllers
{
    [Route("api/transactions")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpPost("deposit")]
        public async Task<ActionResult> Deposit([FromBody] DepositDto depositDto)
        {
            if (depositDto.Amount <= 0)
            {
                return BadRequest("Deposit amount must be greater than zero.");
            }

            var success = await _transactionService.Deposit(depositDto.AccountNumber, depositDto.Amount);
            if (!success)
            {
                return NotFound("Account not found.");
            }

            return Ok("Deposit successful.");
        }

        [HttpPost("withdraw")]
        public async Task<ActionResult> Withdraw([FromBody] WithdrawDto withdrawDto)
        {
            if (withdrawDto.Amount <= 0)
            {
                return BadRequest("Withdrawal amount must be greater than zero.");
            }

            var success = await _transactionService.Withdraw(withdrawDto.AccountNumber, withdrawDto.Amount);
            if (!success)
            {
                return NotFound("Account not found or insufficient funds.");
            }

            return Ok("Withdrawal successful.");
        }

        [HttpPost("transfer")]
        public async Task<ActionResult> Transfer([FromBody] TransferDto transferDto)
        {
            if (transferDto.Amount <= 0)
            {
                return BadRequest("Transfer amount must be greater than zero.");
            }

            var success = await _transactionService.Transfer(transferDto.FromAccountNumber, transferDto.ToAccountNumber, transferDto.Amount);
            if (!success)
            {
                return NotFound("One or both accounts not found or insufficient funds.");
            }

            return Ok("Transfer successful.");
        }
    }
}

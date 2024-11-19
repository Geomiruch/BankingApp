using Microsoft.AspNetCore.Mvc;
using BankingApp.DTO;

namespace BankingAppTests
{
    public class TransactionControllerTests : IClassFixture<BankingAppTestFixture>
    {
        private readonly BankingAppTestFixture _fixture;

        public TransactionControllerTests(BankingAppTestFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task Deposit_ShouldReturnOk_WhenSuccessful()
        {
            // Arrange
            var account = await _fixture.AccountService.CreateAccount("Test Owner", 1000);
            var depositDto = new DepositDto
            {
                AccountNumber = account.AccountNumber,
                Amount = 500
            };

            // Act
            var result = await _fixture.TransactionController.Deposit(depositDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Deposit successful.", okResult.Value);

            var updatedAccount = await _fixture.AccountService.GetAccountDetails(account.AccountNumber);
            Assert.Equal(1500, updatedAccount.Balance);  
        }

        [Fact]
        public async Task Deposit_ShouldReturnBadRequest_WhenInvalidAmount()
        {
            // Arrange
            var account = await _fixture.AccountService.CreateAccount("Test Owner", 1000);
            var depositDto = new DepositDto
            {
                AccountNumber = account.AccountNumber,
                Amount = -100
            };

            // Act
            var result = await _fixture.TransactionController.Deposit(depositDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Deposit amount must be greater than zero.", badRequestResult.Value);
        }

        [Fact]
        public async Task Withdraw_ShouldReturnOk_WhenSuccessful()
        {
            // Arrange
            var account = await _fixture.AccountService.CreateAccount("Test Owner", 1000);
            var withdrawDto = new WithdrawDto
            {
                AccountNumber = account.AccountNumber,
                Amount = 300
            };

            // Act
            var result = await _fixture.TransactionController.Withdraw(withdrawDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Withdrawal successful.", okResult.Value);

            var updatedAccount = await _fixture.AccountService.GetAccountDetails(account.AccountNumber);
            Assert.Equal(700, updatedAccount.Balance);  
        }

        [Fact]
        public async Task Withdraw_ShouldReturnBadRequest_WhenInvalidAmount()
        {
            // Arrange
            var account = await _fixture.AccountService.CreateAccount("Test Owner", 1000);
            var withdrawDto = new WithdrawDto
            {
                AccountNumber = account.AccountNumber,
                Amount = -100
            };

            // Act
            var result = await _fixture.TransactionController.Withdraw(withdrawDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Withdrawal amount must be greater than zero.", badRequestResult.Value);
        }

        [Fact]
        public async Task Withdraw_ShouldReturnNotFound_WhenAccountDoesNotExist()
        {
            // Arrange
            var withdrawDto = new WithdrawDto
            {
                AccountNumber = "NonExistentAccount",
                Amount = 300
            };

            // Act
            var result = await _fixture.TransactionController.Withdraw(withdrawDto);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("Account not found or insufficient funds.", notFoundResult.Value);
        }

        [Fact]
        public async Task Withdraw_ShouldReturnNotFound_WhenInsufficientFunds()
        {
            // Arrange
            var account = await _fixture.AccountService.CreateAccount("Test Owner", 100);
            var withdrawDto = new WithdrawDto
            {
                AccountNumber = account.AccountNumber,
                Amount = 200
            };

            // Act
            var result = await _fixture.TransactionController.Withdraw(withdrawDto);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("Account not found or insufficient funds.", notFoundResult.Value);
        }

        [Fact]
        public async Task Transfer_ShouldReturnOk_WhenSuccessful()
        {
            // Arrange
            var sender = await _fixture.AccountService.CreateAccount("Sender", 1000);
            var receiver = await _fixture.AccountService.CreateAccount("Receiver", 500);

            var transferDto = new TransferDto
            {
                FromAccountNumber = sender.AccountNumber,
                ToAccountNumber = receiver.AccountNumber,
                Amount = 300
            };

            // Act
            var result = await _fixture.TransactionController.Transfer(transferDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Transfer successful.", okResult.Value);

            var updatedSender = await _fixture.AccountService.GetAccountDetails(sender.AccountNumber);
            var updatedReceiver = await _fixture.AccountService.GetAccountDetails(receiver.AccountNumber);

            Assert.Equal(700, updatedSender.Balance);  
            Assert.Equal(800, updatedReceiver.Balance);  
        }

        [Fact]
        public async Task Transfer_ShouldReturnNotFound_WhenAccountDoesNotExist()
        {
            // Arrange
            var senderAccount = await _fixture.AccountService.CreateAccount("Sender", 1000);
            var nonExistentAccountNumber = "NonExistentAccountNumber";  

            var transferDto = new TransferDto
            {
                FromAccountNumber = senderAccount.AccountNumber,
                ToAccountNumber = nonExistentAccountNumber, 
                Amount = 100
            };

            // Act
            var result = await _fixture.TransactionController.Transfer(transferDto);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("One or both accounts not found or insufficient funds.", notFoundResult.Value);
        }

    }
}

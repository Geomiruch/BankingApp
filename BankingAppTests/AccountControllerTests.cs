using Microsoft.AspNetCore.Mvc;
using BankingApp.DAL.Models;
using BankingApp.DTO;

namespace BankingAppTests
{
    public class AccountControllerTests : IClassFixture<BankingAppTestFixture>
    {
        private readonly BankingAppTestFixture _fixture;

        public AccountControllerTests(BankingAppTestFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task CreateAccount_ShouldReturnCreated_WhenSuccessful()
        {
            // Arrange
            var createAccountDto = new CreateAccountDto
            {
                Owner = "Test Owner",
                InitialBalance = 1000
            };

            // Act
            var result = await _fixture.AccountController.CreateAccount(createAccountDto);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnedAccount = Assert.IsType<Account>(createdResult.Value);
            Assert.Equal(createAccountDto.Owner, returnedAccount.OwnerName);
            Assert.Equal(createAccountDto.InitialBalance, returnedAccount.Balance);
        }

        [Fact]
        public async Task CreateAccount_ShouldReturnBadRequest_WhenInvalidBalance()
        {
            // Arrange
            var createAccountDto = new CreateAccountDto
            {
                Owner = "Test Owner",
                InitialBalance = -100
            };

            // Act
            var result = await _fixture.AccountController.CreateAccount(createAccountDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal("Initial balance must be greater than zero.", badRequestResult.Value);
        }

        [Fact]
        public async Task GetAccountDetails_ShouldReturnAccount_WhenAccountExists()
        {
            // Arrange
            var createAccountDto = new CreateAccountDto
            {
                Owner = "Test Owner",
                InitialBalance = 1000
            };

            var account = await _fixture.AccountService.CreateAccount(createAccountDto.Owner, createAccountDto.InitialBalance);

            // Act
            var result = await _fixture.AccountController.GetAccountDetails(account.AccountNumber);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedAccount = Assert.IsType<Account>(okResult.Value);
            Assert.Equal(account.AccountNumber, returnedAccount.AccountNumber);
            Assert.Equal(account.Balance, returnedAccount.Balance);
        }

        [Fact]
        public async Task GetAccountDetails_ShouldReturnNotFound_WhenAccountDoesNotExist()
        {
            // Arrange
            var nonExistentAccountNumber = "NonExistentAccountNumber";

            // Act
            var result = await _fixture.AccountController.GetAccountDetails(nonExistentAccountNumber);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
            Assert.Equal("Account not found.", notFoundResult.Value);
        }

        [Fact]
        public async Task GetAllAccounts_ShouldReturnAllAccounts_WhenAccountsExist()
        {
            // Arrange
            var account1 = await _fixture.AccountService.CreateAccount("Owner 1", 1000);
            var account2 = await _fixture.AccountService.CreateAccount("Owner 2", 1500);

            // Act
            var result = await _fixture.AccountController.GetAllAccounts();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var accounts = Assert.IsType<List<Account>>(okResult.Value);

            Assert.NotEmpty(accounts);

            Assert.Contains(accounts, account => account.AccountNumber == account1.AccountNumber);
            Assert.Contains(accounts, account => account.AccountNumber == account2.AccountNumber);
        }
    }
}

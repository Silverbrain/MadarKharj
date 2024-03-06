using FluentAssertions;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.Extensions.Logging;
using PersonalFinance.Repository;

namespace PersonalFinance.Test;

public class AccountRepositoryTests
{
    [Fact]
    public async void AccountRepository_GetAccounts_ReturnList()
    {
        //Arrange
        using ILoggerFactory factory = LoggerFactory.Create(builder =>
        {
            builder.AddConsole();
        });
        var logger = factory.CreateLogger(nameof(AccountRepository_GetAccounts_ReturnList));
        var dbContext = new PFDbContext();
        var accRepo = new AccountRepository(dbContext, logger);

        //Act
        var res = await accRepo.GetAccountsAsync();

        //Assert
        res.Should().NotBeNull();
    }
}

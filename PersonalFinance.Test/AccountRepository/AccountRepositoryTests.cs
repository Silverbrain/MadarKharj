using FakeItEasy;
using FluentAssertions;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.Extensions.Logging;
using PersonalFinance.Repository;
using PersonalFinance.Shared;

namespace PersonalFinance.Test;

public class AccountRepositoryTests
{
    private readonly ILogger logger;
    private readonly PFDbContext dbContext;
    private readonly IAccountRepository accRepo;
    public AccountRepositoryTests()
    {
        //Dependencies
        // logger = A.Fake<ILogger>();
        // dbContext = A.Fake<PFDbContext>();
        accRepo = A.Fake<IAccountRepository>(x =>
        {
            //x.WithArgumentsForConstructor(() => new AccountRepository(dbContext, logger));
        });
    }

    [Fact]
    public async void AccountRepository_GetAccounts_ReturnList()
    {
        //Arrange
        A.CallTo(() => accRepo.GetAccountsAsync())
            .Returns(Task.FromResult(A.CollectionOfDummy<Account>(10).AsEnumerable()));

        //Act
        var res = await accRepo.GetAccountsAsync();

        //Assert
        res.Should().BeOfType<List<Account>>();
        res.Should().HaveCount(10);
    }

    [Theory]
    [InlineData(0)]
    public async void AccountRepository_GetAccount_ReturnAccount(int accountId)
    {
        //Arrange
        //A.CallTo(() => accRepo.GetAccountAsync(accountId))
            //.Returns(Task.FromResult(A.Dummy<Account>()));

        //Act
        var res = await accRepo.GetAccountAsync(accountId);

        //Assert
        res.Should().BeOfType<Account?>();
        res.Should().NotBeNull();
    }
}

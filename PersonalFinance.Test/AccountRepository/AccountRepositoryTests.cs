using System.Runtime.CompilerServices;
using FakeItEasy;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.Extensions.Logging;
using PersonalFinance.Repository;
using PersonalFinance.Shared;

namespace PersonalFinance.Test;

public class AccountRepositoryTests
{
    private readonly ILogger logger;

    private async Task<PFDbContext> GetDbContext()
    {
        var dbContextOptions = new DbContextOptionsBuilder<PFDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        var databaseContext = new PFDbContext(dbContextOptions);

        databaseContext.Database.EnsureCreated();

        var accs = new List<Account>(){
            new Account{ Id = 1, Name = "Income"},
            new Account{ Id = 2, Name = "Expence"},
            new Account{ Id = 3, Name = "Sina"}
        };

        if (await databaseContext.Accounts.CountAsync() <= 0)
        {
            await databaseContext.Accounts.AddRangeAsync(accs);
            await databaseContext.SaveChangesAsync();
        }

        var trans = new List<Transaction>(){
            new Transaction{ Id = Guid.NewGuid(), Date = DateTime.Now, Description = "Test1"},
            new Transaction{ Id = Guid.NewGuid(), Date = DateTime.Now, Description = "Test2"},
            new Transaction{ Id = Guid.NewGuid(), Date = DateTime.Now, Description = "Test3"}
        };

        if (await databaseContext.Transactions.CountAsync() <= 0)
        {
            await databaseContext.Transactions.AddRangeAsync(trans);
            await databaseContext.SaveChangesAsync();
        }

        if (await databaseContext.Entries.CountAsync() <= 0)
        {
            await databaseContext.Entries.AddAsync(new Entry
            { TransactionId = trans[0].Id, Amount = -1500, AccountId = 1, Id = Guid.NewGuid() });
            await databaseContext.Entries.AddAsync(new Entry
            { TransactionId = trans[0].Id, Amount = 1500, AccountId = 3, Id = Guid.NewGuid() });
            await databaseContext.Entries.AddAsync(new Entry
            { TransactionId = trans[1].Id, Amount = -500, AccountId = 3, Id = Guid.NewGuid() });
            await databaseContext.Entries.AddAsync(new Entry
            { TransactionId = trans[1].Id, Amount = 500, AccountId = 2, Id = Guid.NewGuid() });
            await databaseContext.Entries.AddAsync(new Entry
            { TransactionId = trans[2].Id, Amount = -200, AccountId = 3, Id = Guid.NewGuid() });
            await databaseContext.Entries.AddAsync(new Entry
            { TransactionId = trans[2].Id, Amount = 200, AccountId = 2, Id = Guid.NewGuid() });

            await databaseContext.SaveChangesAsync();
        }

        return databaseContext;
    }
    public AccountRepositoryTests()
    {
        //Dependencies
        logger = A.Fake<ILogger>();
    }

    [Fact]
    public async void AccountRepository_GetAccounts_ReturnList()
    {
        //Arrange
        var dbContext = await GetDbContext();
        var accRepo = new AccountRepository(dbContext, logger);

        // A.CallTo(() => accRepo.GetAccountsAsync())
        //     .Returns(Task.FromResult(A.CollectionOfDummy<Account>(10).AsEnumerable()));

        //Act
        var res = await accRepo.GetAccountsAsync();

        //Assert
        res.Should().BeOfType<List<Account>>();
        res.Should().NotBeNullOrEmpty();
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public async void AccountRepository_GetAccount_ReturnAccount(int accountId)
    {
        //Arrange
        var dbContext = await GetDbContext();
        var accRepo = new AccountRepository(dbContext, logger);

        // A.CallTo(() => accRepo.GetAccountAsync(accountId))
        //     .Returns(Task.FromResult(A.Dummy<Account>()));

        //Act
        var res = await accRepo.GetAccountAsync(accountId);

        //Assert
        res.Should().NotBeNull();
        res.Should().BeOfType<Account?>();
    }
}

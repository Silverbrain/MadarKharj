// See https://aka.ms/new-console-template for more information
using System.Net.Http.Headers;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using PersonalFinance.Repository;
using PersonalFinance.Shared;
using PersonalFinance.Test;

var dbContext = new PFDbContext();
ILogger logger;

using ILoggerFactory factory = LoggerFactory.Create(builder => {
            builder.AddConsole();
        });
        logger = factory.CreateLogger("Test Console");

var accRepo = new AccountRepository(dbContext, logger);
var tranRepo = new TransactionRepository(dbContext, logger);
var entRepo = new EntryRepository(dbContext, logger);

var repo = new RepositoryTest(accRepo, tranRepo, entRepo);

var accs = await repo.GetAccounts();
foreach(Account account in accs)
{
    Console.WriteLine(account);
}
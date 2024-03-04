using Microsoft.Extensions.Logging;

namespace PersonalFinance.Shared;

public class DataSeed
{
    public static List<Account> Accounts { get; } = new List<Account>();
    public static List<Entry> Entries { get; } = new List<Entry>();
    public static List<Transaction> Transactions { get; } = new List<Transaction>();

    private readonly ILogger logger;

    public DataSeed()
    {
        using ILoggerFactory factory = LoggerFactory.Create(builder => {
            builder.AddConsole();
        });
        logger = factory.CreateLogger("DataSeed");

        logger.LogInformation("Initializing database seed data.");
        
        seedAccounts();
        seedTransactions();
        seedEntries();

        logger.LogInformation("Database seed finished.");
    }

    private void seedAccounts()
    {
        logger.LogInformation("Seeding account table.");
        Accounts.AddRange(new[] {
            new Account{
                Id = 1,
                Name = "Sina"
            },
            new Account{
                Id = 2,
                Name = "Shayan"
            },
            new Account{
                Id = 3,
                Name = "Income"
            },
            new Account{
                Id = 4,
                Name = "Expences"
            }
        });
        logger.LogInformation("Account table seed data is ready.");
    }

    private void seedTransactions()
    {
        Transactions.AddRange(new[] {
            new Transaction{
                Id = Guid.NewGuid(),
                Date = DateTime.Now.Date,
                Description = "test transaction 1"
            },
            new Transaction{
                Id = Guid.NewGuid(),
                Date = DateTime.Now.Date,
                Description = "test transaction 2"
            },
            new Transaction{
                Id = Guid.NewGuid(),
                Date = DateTime.Now.Date,
                Description = "test transaction 3"
            }
        });
    }

    private void seedEntries()
    {
        Entries.AddRange(new[] {
            new Entry{
                Id = Guid.NewGuid(),
                AccountId = 4,
                Amount = -1500.0,
                TransactionId = Transactions[0].Id
            },
            new Entry{
                Id = Guid.NewGuid(),
                AccountId = 1,
                Amount = 1500.0,
                TransactionId = Transactions[0].Id
            },
            new Entry{
                    Id = Guid.NewGuid(),
                    AccountId = 1,
                    Amount = -500.0,
                    TransactionId = Transactions[1].Id
                },
            new Entry{
                    Id = Guid.NewGuid(),
                    AccountId = 2,
                    Amount = 500.0,
                    TransactionId = Transactions[1].Id
                },
            new Entry{
                    Id = Guid.NewGuid(),
                    AccountId = 1,
                    Amount = -400.0,
                    TransactionId = Transactions[2].Id
                },
            new Entry{
                    Id = Guid.NewGuid(),
                    AccountId = 5,
                    Amount = 400.0,
                    TransactionId = Transactions[2].Id
                }
        });
    }
}

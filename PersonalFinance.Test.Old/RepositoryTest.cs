using Microsoft.EntityFrameworkCore.Migrations.Operations;
using PersonalFinance.Repository;
using PersonalFinance.Shared;

namespace PersonalFinance.Test;

public class RepositoryTest
{
    private readonly IAccountRepository accountRepository;
    private readonly ITransactionRepository transactionRepository;
    private readonly IEntryRepository entryRepository;

    public RepositoryTest(
        IAccountRepository accountRepository,
        ITransactionRepository transactionRepository,
        IEntryRepository entryRepository)
    {
        this.accountRepository = accountRepository;
        this.transactionRepository = transactionRepository;
        this.entryRepository = entryRepository;
    }

    public async void AddAccount(string accName)
    {
        await accountRepository.CreateAccountAsync(new Shared.Account{Name = accName});
    }

    public async Task<Account> GetAccount(int accId)
    {
        return await accountRepository.GetAccountAsync(accId);
    }

    public async Task<List<Account>> GetAccounts()
    {
        return (await accountRepository.GetAccountsAsync()).ToList();
    }
}

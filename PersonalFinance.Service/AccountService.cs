using PersonalFinance.Repository;
using PersonalFinance.Shared;

namespace PersonalFinance.Service;

public class AccountService : IAccountService
{
    private readonly IAccountRepository accountRepository;

    public AccountService(IAccountRepository accountRepository)
    {
        this.accountRepository = accountRepository;
    }
    public async Task<Account> CreateAccountAsync(Account account)
    {
        if (account != null)
        {
            var res = await accountRepository.CreateAccountAsync(account);
            return res;
        }
        throw new Exception("Account object pased to this function is null", new NullReferenceException());
    }

    public async Task<Account> DeleteAccountAsync(int accountId)
    {
        return await accountRepository.DeleteAccountAsync(accountId);
    }

    public async Task<Account> GetAccountAsync(int accountId)
    {
        return await accountRepository.GetAccountAsync(accountId);
    }

    public async Task<IEnumerable<Account>> GetAccountsAsync()
    {
        return await accountRepository.GetAccountsAsync();
    }

    public Task<IEnumerable<Account>> SearchAccountAsync(string accountName)
    {
        return accountRepository.SearchAccountAsync(accountName);
    }

    public async Task<Account> UpdateAccountAsync(Account account)
    {
        if (account == null)
        {
            throw new Exception("Account object pased to this function is null", new NullReferenceException());
        }
        return await accountRepository.UpdateAccountAsync(account);
    }
}

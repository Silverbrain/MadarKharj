using PersonalFinance.Shared;

namespace PersonalFinance.Service;

public interface IAccountService
{
    public Task<IEnumerable<Account>> GetAccountsAsync();
    public Task<Account> GetAccountAsync(int accountId);
    public Task<Account> CreateAccountAsync(Account account);
    public Task<Account> UpdateAccountAsync(Account account);
    public Task<Account> DeleteAccountAsync(int accountId);
    public Task<IEnumerable<Account>> SearchAccountAsync(string accountName);
}

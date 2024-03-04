using System.Data.Common;
using PersonalFinance.Shared;

namespace PersonalFinance.Repository;

public interface IAccountRepository
{
    public Task<Account> CreateAccountAsync(Account account);
    public Task<Account> GetAccountAsync(int accountId);
    public Task<Account> UpdateAccountAsync(Account account);
    public Task<Account> DeleteAccountAsync(int accountId);
    public Task<IEnumerable<Account>> GetAccountsAsync();
    public Task<IEnumerable<Account>> SearchAccountAsync(int? accountId, string? name);
}

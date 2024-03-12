using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.Identity.Client;
using PersonalFinance.Shared;

namespace PersonalFinance.Service;

public class ClientAccountService : IAccountService
{
    private readonly HttpClient httpClient;

    public ClientAccountService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public Task<Account> CreateAccountAsync(Account account)
    {
        throw new NotImplementedException();
    }

    public Task<Account> DeleteAccountAsync(int accountId)
    {
        throw new NotImplementedException();
    }

    public Task<Account> GetAccountAsync(int accountId)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Account>> GetAccountsAsync()
    {
        return await httpClient.GetFromJsonAsync<IEnumerable<Account>>("/api/account");
    }

    public Task<IEnumerable<Account>> SearchAccountAsync(string accountName)
    {
        throw new NotImplementedException();
    }

    public Task<Account> UpdateAccountAsync(Account account)
    {
        throw new NotImplementedException();
    }
}

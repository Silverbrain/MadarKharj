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

    public async Task<Account> CreateAccountAsync(Account account)
    {
        var res = await httpClient.PostAsJsonAsync("/api/account", account);
        return (await res.Content.ReadFromJsonAsync<Account>())!;
    }

    public async Task<Account> DeleteAccountAsync(int accountId)
    {
        return (await httpClient.DeleteFromJsonAsync<Account>($"/api/account/{accountId}"))!;
    }

    public async Task<Account> GetAccountAsync(int accountId)
    {
        return (await httpClient.GetFromJsonAsync<Account>($"/api/account/{accountId}"))!;
    }

    public async Task<IEnumerable<Account>> GetAccountsAsync()
    {
        return (await httpClient.GetFromJsonAsync<IEnumerable<Account>>("/api/account"))!;
    }

    public Task<IEnumerable<Account>> SearchAccountAsync(string accountName)
    {
        throw new NotImplementedException();
    }

    public async Task<Account> UpdateAccountAsync(Account account)
    {
        if(account.Id == 0)
        {
            throw new NullReferenceException();
        }

        var res = await httpClient.PutAsJsonAsync($"/api/account/{account.Id}", account);

        return (await res.Content.ReadFromJsonAsync<Account>())!;
    }
}

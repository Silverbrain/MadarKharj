using System.Net.Http.Json;
using PersonalFinance.Shared;

namespace PersonalFinance.Service;

public class ClientTransactionService : ITransactionService
{
    private readonly HttpClient client;

    public ClientTransactionService(HttpClient client)
    {
        this.client = client;
    }

    public Task<Transaction> CreateTransactionAsync(Transaction transaction)
    {
        throw new NotImplementedException();
    }

    public Task<Transaction> DeleteTransactionAsync(Guid transactionId)
    {
        throw new NotImplementedException();
    }

    public async Task<Transaction> GetTransactionAsync(Guid transactionId)
    {
        return await client.GetFromJsonAsync<Transaction>($"/api/transaction/{transactionId}");
    }

    public async Task<IEnumerable<Transaction>> GetTransactionsAsync()
    {
        return await client.GetFromJsonAsync<IEnumerable<Transaction>>("/api/transaction");
    }

    public Task<IEnumerable<Transaction>> SearchTransactionsAsync(string description)
    {
        throw new NotImplementedException();
    }

    public Task<Transaction> UpdateTransactionAsync(Transaction transaction)
    {
        throw new NotImplementedException();
    }
}

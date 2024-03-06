using System.ComponentModel;
using PersonalFinance.Shared;

namespace PersonalFinance.Repository;

public interface ITransactionRepository
{
    public Task<Transaction> CreateTransactionAsync(Transaction transaction);
    public Task<Transaction> GetTransactionAsync(Guid transactionId);
    public Task<Transaction> UpdateTransactionAsync(Transaction transaction);
    public Task<Transaction> DeleteTransactionAsync(Guid transactionId);

    public Task<IEnumerable<Transaction>> GetTransactionsAsync();
    public Task<IEnumerable<Transaction>> SearchTransactionAsync(string? description);

}

using PersonalFinance.Shared;

namespace PersonalFinance.Service;

public interface ITransactionService
{
    public Task<IEnumerable<Transaction>> GetTransactionsAsync();
    public Task<Transaction> GetTransactionAsync(Guid transactionId);
    public Task<Transaction> CreateTransactionAsync(Transaction transaction);
    public Task<Transaction> UpdateTransactionAsync(Transaction transaction);
    public Task<Transaction> DeleteTransactionAsync(Guid transactionId);
    public Task<IEnumerable<Transaction>> SearchTransactionsAsync(string description);
}

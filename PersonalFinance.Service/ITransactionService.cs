using System.Transactions;

namespace PersonalFinance.Service;

public interface ITransactionService
{
    public Task<IEnumerable<Transaction>> GetTransactionsAsync();
    public Task<Transaction> GetTransactionAsync(int transactionId);
    public Task<Transaction> CreateTransactionAsync(Transaction transaction);
    public Task<Transaction> UpdateTransactionAsync(Transaction transaction);
    public Task<Transaction> DeleteTransactionAsync(int transactionId);
    public Task<IEnumerable<Transaction>> SearchTransactionsAsync(string description);
}

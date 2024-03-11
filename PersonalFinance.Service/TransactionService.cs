using PersonalFinance.Shared;
using PersonalFinance.Repository;

namespace PersonalFinance.Service;

public class TransactionService : ITransactionService
{
    private readonly ITransactionRepository transactionRepository;

    public TransactionService(ITransactionRepository transactionRepository)
    {
        this.transactionRepository = transactionRepository;
    }
    public async Task<Transaction> CreateTransactionAsync(Transaction transaction)
    {
        if (transaction == null)
        {
            throw new Exception("Transaction object pased to this function is null", new NullReferenceException());
        }
        return await transactionRepository.CreateTransactionAsync(transaction);
    }

    public async Task<Transaction> DeleteTransactionAsync(Guid transactionId)
    {
        return await transactionRepository.DeleteTransactionAsync(transactionId);
    }

    public async Task<Transaction> GetTransactionAsync(Guid transactionId)
    {
        return await transactionRepository.GetTransactionAsync(transactionId);
    }

    public async Task<IEnumerable<Transaction>> GetTransactionsAsync()
    {
        return await transactionRepository.GetTransactionsAsync();
    }

    public async Task<IEnumerable<Transaction>> SearchTransactionsAsync(string description)
    {
        return await transactionRepository.SearchTransactionAsync(description);
    }

    public async Task<Transaction> UpdateTransactionAsync(Transaction transaction)
    {
        if (transaction == null)
        {
            throw new Exception("Transaction object pased to this function is null", new NullReferenceException());
        }

        return await transactionRepository.UpdateTransactionAsync(transaction);
    }
}

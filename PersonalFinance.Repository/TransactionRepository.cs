using System.ComponentModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using PersonalFinance.Shared;

namespace PersonalFinance.Repository;

public class TransactionRepository : ITransactionRepository
{
    private readonly PFDbContext dbContext;
    private readonly ILogger logger;

    public TransactionRepository(PFDbContext dbContext, ILogger logger)
    {
        this.dbContext = dbContext;
        this.logger = logger;
    }

    public async Task<Transaction> CreateTransactionAsync(Transaction transaction)
    {
        try
        {
            EntityEntry<Transaction> newTransaction = await dbContext.Transactions.AddAsync(transaction);

            await dbContext.SaveChangesAsync();

            return newTransaction.Entity;
        }
        catch (Exception e)
        {
            await Task.Run(() =>
                        {
                            logger.LogError(e.Message);
                        });
            return null;
        }
    }

    public async Task<Transaction> DeleteTransactionAsync(Guid transactionId)
    {
        try
        {
            Transaction? tranToDelete = await dbContext.Transactions.FirstOrDefaultAsync(t => t.Id == transactionId);

            if (tranToDelete == null)
            {
                throw new Exception($"Transaction with Id = {transactionId} does not exist.", new NullReferenceException());
            }

            EntityEntry<Transaction> res = dbContext.Transactions.Remove(tranToDelete);
            await dbContext.SaveChangesAsync();

            return res.Entity;
        }
        catch (Exception e)
        {
            await Task.Run(() =>
            {
                logger.LogError(e.Message);
            });
            return null;
        }
    }

    public async Task<Transaction> GetTransactionAsync(Guid transactionId)
    {
        try
        {
            Transaction? tranToRet = await dbContext.Transactions.FindAsync(transactionId);

            return tranToRet;
        }
        catch (Exception e)
        {
            await Task.Run(() =>
            {
                logger.LogError(e.Message);
            });
            return null;
        }
    }

    public async Task<IEnumerable<Transaction>> GetTransactionsAsync()
    {
        try
        {
            return await dbContext.Transactions.ToListAsync();
        }
        catch (Exception e)
        {
            await Task.Run(() =>
            {
                logger.LogError(e.Message);
            });
            return null;
        }
    }

    public async Task<IEnumerable<Transaction>> SearchTransactionAsync(string? description)
    {
        try
        {
            IQueryable<Transaction> trans = dbContext.Transactions.AsQueryable();

            if (description != null)
            {
                trans = trans.Where(t => t.Description.Contains(description));
            }

            return await trans.ToListAsync();
        }
        catch (Exception e)
        {
            await Task.Run(() =>
            {
                logger.LogError(e.Message);
            });
            return null;
        }
    }

    public async Task<Transaction> UpdateTransactionAsync(Transaction transaction)
    {
        try
        {
            Transaction? tranToUpdate = await dbContext.Transactions.FirstOrDefaultAsync(t => t.Id == transaction.Id);

            if(tranToUpdate == null)
            {
                throw new Exception($"Transaction with Id = {transaction.Id} does not exist.", new NullReferenceException());
            }

            tranToUpdate.Date = transaction.Date;
            tranToUpdate.Description = transaction.Description;

            EntityEntry<Transaction> res = dbContext.Transactions.Update(tranToUpdate);
            await dbContext.SaveChangesAsync();

            return res.Entity;
        }
        catch (Exception e)
        {
            await Task.Run(() => 
            {
                logger.LogError(e.Message);
            });
            return null;
        }
    }
}

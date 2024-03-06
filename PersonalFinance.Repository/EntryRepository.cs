using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using PersonalFinance.Shared;

namespace PersonalFinance.Repository;

public class EntryRepository : IEntryRepository
{
    private readonly PFDbContext dbContext;
    private readonly ILogger logger;

    public EntryRepository(PFDbContext dbContext, ILogger logger)
    {
        this.dbContext = dbContext;
        this.logger = logger;
    }

    public async Task<Entry> CreateEntryAsync(Entry entry)
    {
        try
        {
            if (entry == null)
            {
                throw new Exception("Error creating new Entry object.", new NullReferenceException());
            }

            EntityEntry<Entry> res = await dbContext.Entries.AddAsync(entry);
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

    public async Task<Entry> DeleteEntryAsync(Guid entryId)
    {
        try
        {
            Entry? entryToDelete = dbContext.Entries.FirstOrDefault(e => e.Id == entryId);

            if (entryToDelete == null)
            {
                throw new Exception($"Error deleting Entry object with Id = {entryId}.", new NullReferenceException());
            }

            EntityEntry<Entry> res = dbContext.Entries.Remove(entryToDelete);
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

    public async Task<IEnumerable<Entry>> GetEntries()
    {
        try
        {
            return await dbContext.Entries.ToListAsync();
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

    public async Task<Entry> GetEntryAsync(Guid entryId)
    {
        try
        {
            Entry? entryToRet = await dbContext.Entries.FirstOrDefaultAsync(e => e.Id == entryId);

            return entryToRet;
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

    public async Task<IEnumerable<Entry>> SearchEntries(
        DateTime? fromDate = null,
        DateTime? toDate = null,
        double? lowerBoundAmount = null,
        double? upperBoundAmount = null,
        string? accountName = null)
    {
        try
        {
            IQueryable<Entry> entries = dbContext.Entries
                .Include(e => e.Transaction)
                .Include(e => e.Account)
                .AsQueryable();

            //Date filter
            if (fromDate != null)
            {
                entries = entries.Where(e => e.Transaction.Date >= fromDate);
            }
            if (toDate != null)
            {
                entries = entries.Where(e => e.Transaction.Date < toDate);
            }

            // Amount filter
            if (lowerBoundAmount != null)
            {
                entries = entries.Where(e => e.Amount >= lowerBoundAmount);
            }
            if (upperBoundAmount != null)
            {
                entries = entries.Where(e => e.Amount <= upperBoundAmount);
            }

            //account Name filter
            if (accountName != null)
            {
                entries = entries.Where(e => e.Account.Name.Contains(accountName));
            }

            return await entries.ToListAsync();
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

    public async Task<Entry> UpdateEntryAsync(Entry entry)
    {
        try
        {
            Entry? entryToUpdate = await dbContext.Entries.FindAsync(entry.Id);

            if (entryToUpdate == null)
            {
                throw new Exception($"Error updating Entry object with Id = {entry.Id}.", new NullReferenceException());
            }

            entryToUpdate.AccountId = entry.AccountId;
            entryToUpdate.Amount = entry.Amount;
            entryToUpdate.TransactionId = entry.TransactionId;

            EntityEntry<Entry> res = dbContext.Entries.Update(entryToUpdate);
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

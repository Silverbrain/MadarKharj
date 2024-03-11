using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PersonalFinance.Shared;

namespace PersonalFinance.Repository;

public class AccountRepository : IAccountRepository
{
    private readonly PFDbContext dbContext;
    private readonly ILogger<AccountRepository> logger;

    public AccountRepository(PFDbContext dbContext, ILogger<AccountRepository> logger)
    {
        this.dbContext = dbContext;
        this.logger = logger;
    }

    public async Task<Account> CreateAccountAsync(Account account)
    {
        try
        {
            if (account != null)
            {
                var res = await dbContext.Accounts.AddAsync(new Account { Name = account.Name });
                if (res != null)
                {
                    await dbContext.SaveChangesAsync();
                    return res.Entity;
                }
            }
            throw new Exception("Error creating new account.", new NullReferenceException());
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

    public async Task<Account> DeleteAccountAsync(int accountId)
    {
        try
        {
            var account = await dbContext.Accounts.FirstOrDefaultAsync(A => A.Id == accountId);

            if (account != null)
            {
                var res = dbContext.Accounts.Remove(account);
                await dbContext.SaveChangesAsync();

                return res.Entity;
            }
            throw new Exception($"Error deleting account with {{Id = {accountId}}}.", new NullReferenceException());
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

    public async Task<Account> GetAccountAsync(int accountId)
    {
        try
        {
            var account = await dbContext.Accounts.FirstOrDefaultAsync(A => A.Id == accountId);

            if (account != null)
            {
                return account;
            }
            throw new Exception($"Error retrieving account with {{Id = {accountId}}}.", new NullReferenceException());
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

    public async Task<IEnumerable<Account>> GetAccountsAsync()
    {
        try
        {
            return await dbContext.Accounts.ToListAsync();
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

    public async Task<IEnumerable<Account>> SearchAccountAsync(string? name)
    {
        try
        {

            var query = dbContext.Accounts.AsQueryable();

            await Task.Run(() =>
            {
                if (name != null)
                {
                    query = query.Where(a => a.Name.Contains(name));
                }
            });

            return await query.ToListAsync();
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

    public async Task<Account> UpdateAccountAsync(Account account)
    {
        try
        {
            var accountToEdit = await dbContext.Accounts.FindAsync(account.Id);

            if (accountToEdit == null)
            {
                throw new Exception($"Error updating account with {{Id = {account.Id}}}.", new NullReferenceException());
            }

            accountToEdit.Name = account.Name;

            var res = dbContext.Accounts.Update(accountToEdit);
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
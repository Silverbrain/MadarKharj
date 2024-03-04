using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using PersonalFinance.Shared;

namespace PersonalFinance.Repository;

public class DataAccessTest
{
    private readonly PFDbContext _dbContext;
    public DataAccessTest(PFDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public List<Account> GetAccounts()
    {
        return _dbContext.Accounts.ToList();
    }

    public string CreateAccount(Account account)
    {
        try
        {

            _dbContext.Accounts.Add(account);
            _dbContext.SaveChanges();
            return "Account added successfully.";
        }
        catch(Exception e)
        {
            return e.Message;
        }
    }
    public string DeleteAccount(int accountId)
    {
        try
        {
            var acc = _dbContext.Accounts.FirstOrDefault(a => a.Id == accountId);

            if(acc != null)
            {
                _dbContext.Accounts.Remove(acc);
                _dbContext.SaveChanges();
                return $"Account [{acc.Id}, {acc.Name}] deleted successfully.";
            }

            return $"Account with Id: {accountId} could not be found.";
        }
        catch (Exception e)
        {
            return e.Message;
        }
    }

    public string CreateEntry(Entry entry)
    {
        try
        {
            _dbContext.Entries.Add(entry);
            _dbContext.SaveChanges();
            return $"Entry successfully added. [Id : {entry.Id}, Amount: {entry.Amount}]";
        }
        catch (Exception e)
        {
            return e.Message;
        }
    }

    public string CreateTransaction(Transaction transaction)
    {
        try
        {
            _dbContext.Transactions.Add(transaction);
            _dbContext.SaveChanges();
            return $"Entry successfully added. [Id : {transaction.Id}, Description: {transaction.Description}]";
        }
        catch (Exception e)
        {
            return e.Message;
        }
    }

    public List<Transaction> GetTransactions()
    {
        return _dbContext.Transactions.Include(T => 
            T.Entries).ThenInclude(E => E.Account).ToList();
    }
}

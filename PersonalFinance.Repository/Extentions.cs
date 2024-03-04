using Microsoft.EntityFrameworkCore;
using PersonalFinance.Shared;

namespace PersonalFinance.Repository;

public static class Extentions
{
    public static void SeedWithData(this ModelBuilder modelBuilder, DataSeed dataSeed)
    {
        //Accounts seed data
        modelBuilder.Entity<Account>().HasData(DataSeed.Accounts);

        //Entries seed data
        modelBuilder.Entity<Entry>().HasData(DataSeed.Entries);

        //Transactions seed data
        modelBuilder.Entity<Transaction>().HasData(DataSeed.Transactions);
    }
}

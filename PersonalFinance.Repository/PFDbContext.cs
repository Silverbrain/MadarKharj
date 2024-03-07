using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PersonalFinance.Shared;

namespace PersonalFinance.Repository;

public class PFDbContext : DbContext
{
    private readonly string _connectionString = "Server= localhost,1433; Database = SPFPDataBase; User Id = SA; Password = S!na8481780; MultipleActiveResultSets=true; TrustServerCertificate=True";
    private readonly DbContextOptionsBuilder<PFDbContext> optionsBuilder = null;
    
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Entry> Entries { get; set; }
    public DbSet<Transaction> Transactions { get; set; }

    public PFDbContext(DbContextOptions<PFDbContext> options) : base(options)
    {
    }

    public PFDbContext()
    {
        optionsBuilder = new DbContextOptionsBuilder<PFDbContext>()
            .UseSqlServer(_connectionString)
            .EnableSensitiveDataLogging();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(this.optionsBuilder ?? optionsBuilder);
        // optionsBuilder.UseSqlServer(_connectionString);
        // optionsBuilder.EnableSensitiveDataLogging();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        //modelBuilder.SeedWithData(new DataSeed());
    }
}
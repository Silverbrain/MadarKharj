using Microsoft.Extensions.DependencyInjection;
using PersonalFinance.Repository;
using PersonalFinance.Service;

namespace PersonalFinance.Extention;

public static class Extention
{
    public static IServiceCollection AddAppRepositories(this IServiceCollection services)
    {
        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddScoped<IEntryRepository, EntryRepository>();
        services.AddScoped<ITransactionRepository, TransactionRepository>();

        return services;
    }

    public static IServiceCollection AddAppServices(this IServiceCollection services)
    {
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<IEntryService, EntryService>();
        services.AddScoped<ITransactionService, TransactionService>();

        return services;
    }
}
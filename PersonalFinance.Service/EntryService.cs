using PersonalFinance.Repository;
using PersonalFinance.Shared;

namespace PersonalFinance.Service;

public class EntryService : IEntryService
{
    private readonly IEntryRepository entryRepository;

    public EntryService(IEntryRepository entryRepository)
    {
        this.entryRepository = entryRepository;
    }
    public async Task<Entry> CreateEntryAsync(Entry entry)
    {
        if (entry == null)
        {
            throw new Exception("Entry object pased to this function is null", new NullReferenceException());
        }
        return await entryRepository.CreateEntryAsync(entry);
    }

    public async Task<Entry> DeleteEntryAsync(Guid entryId)
    {
        return await entryRepository.DeleteEntryAsync(entryId);
    }

    public async Task<Entry> GetEntryAsync(Guid entryId)
    {
        return await entryRepository.GetEntryAsync(entryId);
    }

    public async Task<IEnumerable<Entry>> GetEntriesAsync()
    {
        return await entryRepository.GetEntries();
    }

    public async Task<IEnumerable<Entry>> SearchEntriesAsync(DateTime? fromDate, DateTime? toDate, double? lowerBoundAmount, double? upperBoundAmount, string? accountName)
    {
        return await entryRepository.SearchEntries(fromDate, toDate, lowerBoundAmount, upperBoundAmount, accountName);
    }

    public async Task<Entry> UpdateEntryAsync(Entry entry)
    {
        if (entry == null)
        {
            throw new Exception("Entry object pased to this function is null", new NullReferenceException());
        }
        return await entryRepository.UpdateEntryAsync(entry);
    }
}

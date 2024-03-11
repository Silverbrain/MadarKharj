using PersonalFinance.Shared;

namespace PersonalFinance.Service;

public interface IEntryService
{
    public Task<IEnumerable<Entry>> GetEntrysAsync();
    public Task<Entry> GetEntryAsync(int entryId);
    public Task<Entry> CreateEntryAsync(Entry entry);
    public Task<Entry> UpdateEntryAsync(Entry entry);
    public Task<Entry> DeleteEntryAsync(int entryId);
    public Task<IEnumerable<Entry>> SearchEntriesAsync(
        DateTime? fromDate,
        DateTime? toDate,
        double? lowerBoundAmount,
        double? upperBoundAmount,
        string? accountName
    );
}

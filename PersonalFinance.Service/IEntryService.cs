using PersonalFinance.Shared;

namespace PersonalFinance.Service;

public interface IEntryService
{
    public Task<IEnumerable<Entry>> GetEntriesAsync();
    public Task<Entry> GetEntryAsync(Guid entryId);
    public Task<Entry> CreateEntryAsync(Entry entry);
    public Task<Entry> UpdateEntryAsync(Entry entry);
    public Task<Entry> DeleteEntryAsync(Guid entryId);
    public Task<IEnumerable<Entry>> SearchEntriesAsync(
        DateTime? fromDate,
        DateTime? toDate,
        double? lowerBoundAmount,
        double? upperBoundAmount,
        string? accountName
    );
}

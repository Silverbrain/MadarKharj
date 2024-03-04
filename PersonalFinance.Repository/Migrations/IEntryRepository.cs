﻿using Microsoft.Identity.Client;
using PersonalFinance.Shared;

namespace PersonalFinance.Repository;

public interface IEntryRepository
{
    public Task<Entry> CreateEntryAsync(Entry entry);
    public Task<Entry> GetEntryAsync(Guid entryId);
    public Task<Entry> UpdateEntryAsync(Entry entry);
    public Task<Entry> DeleteEntryAsync(Guid entryId);

    public Task<IEnumerable<Entry>> GetEntries();
    public Task<IEnumerable<Entry>> SearchEntries(
        Guid? entryId,
        DateTime? start,
        DateTime? end,
        double? lowerBoundAmount,
        double? upperBoundAmount,
        string? accountName,
        int accountId
    );
}

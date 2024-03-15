using System.Net.Http;
using System.Net.Http.Json;
using PersonalFinance.Shared;

namespace PersonalFinance.Service;

public class ClientEntryService : IEntryService
{
    private readonly HttpClient httpClient;

    public ClientEntryService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<Entry> CreateEntryAsync(Entry entry)
    {
        var res = await httpClient.PostAsJsonAsync("/api/entry", entry);
        
        return (await res.Content.ReadFromJsonAsync<Entry>())!;
    }

    public async Task<Entry> DeleteEntryAsync(Guid entryId)
    {
        var res = await httpClient.DeleteFromJsonAsync<Entry>($"/api/entry/{entryId}");

        return res!;
    }

    public async Task<IEnumerable<Entry>> GetEntriesAsync()
    {
        return (await httpClient.GetFromJsonAsync<IEnumerable<Entry>>("/api/entry"))!;
    }

    public async Task<Entry> GetEntryAsync(Guid entryId)
    {
        var res = await httpClient.GetFromJsonAsync<Entry>($"/api/entry/{entryId}");
        return res!;
    }

    public Task<IEnumerable<Entry>> SearchEntriesAsync(DateTime? fromDate, DateTime? toDate, double? lowerBoundAmount, double? upperBoundAmount, string? accountName)
    {
        throw new NotImplementedException();
    }

    public async Task<Entry> UpdateEntryAsync(Entry entry)
    {
        var res = await httpClient.PutAsJsonAsync($"/api/entry/{entry.Id}", entry);
        entry = (await res.Content.ReadFromJsonAsync<Entry>())!;
        return entry;
    }
}

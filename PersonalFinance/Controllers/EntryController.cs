using System.Runtime.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PersonalFinance.Service;
using PersonalFinance.Shared;

namespace PersonalFinance;

[Route("/api/[controller]")]
[ApiController]
public class EntryController : ControllerBase
{
    private readonly IEntryService entryService;

    public EntryController(IEntryService entryService)
    {
        this.entryService = entryService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Entry>>> GetEntries()
    {
        var entries = await entryService.GetEntriesAsync();

        return Ok(entries);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Entry>> GetEntry(Guid id)
    {
        var res = await entryService.GetEntryAsync(id);

        if (res == null)
        {
            return NotFound();
        }

        return Ok(res);
    }

    [HttpPost]
    public async Task<ActionResult<Entry>> CreateEntry(Entry entry)
    {
        Entry res = null!;

        if (ModelState.IsValid)
        {
            res = await entryService.CreateEntryAsync(entry);
        }

        if (res == null)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        return CreatedAtAction(nameof(GetEntry), new { id = res.Id }, res);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<Entry>> UpdateEntry(Guid id, Entry entry)
    {
        if (id != entry.Id)
        {
            return BadRequest();
        }

        var res = await entryService.UpdateEntryAsync(entry);

        if (res == null)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        return Accepted(res);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<Entry>> DeleteEntry(Guid id)
    {
        var res = await entryService.DeleteEntryAsync(id);

        if (res == null)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        return Accepted(res);
    }
}

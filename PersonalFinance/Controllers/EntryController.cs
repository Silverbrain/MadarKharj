using System.Runtime.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PersonalFinance.Service;
using PersonalFinance.Shared;

namespace PersonalFinance
{
    /// <summary>
    /// API Controller for managing entries.
    /// </summary>
    [Route("/api/[controller]")]
    [ApiController]
    public class EntryController : ControllerBase
    {
        private readonly IEntryService entryService;

        /// <summary>
        /// Initializes a new instance of the <see cref="EntryController"/> class.
        /// </summary>
        /// <param name="entryService">The entry service.</param>
        public EntryController(IEntryService entryService)
        {
            this.entryService = entryService;
        }

        /// <summary>
        /// Gets all entries.
        /// </summary>
        /// <returns>A list of entries.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Entry>>> GetEntries()
        {
            // Call the service to get all entries asynchronously
            var entries = await entryService.GetEntriesAsync();
            
            // Return the list of entries with 200 status code
            return Ok(entries);
        }

        /// <summary>
        /// Gets an entry by ID.
        /// </summary>
        /// <param name="id">The entry ID (GUID).</param>
        /// <returns>The requested entry.</returns>
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Entry>> GetEntry(Guid id)
        {
            // Call the service to get an entry by ID asynchronously
            var res = await entryService.GetEntryAsync(id);

            if (res == null)
            {
                return NotFound(); // Return 404 if the entry is not found
            }

            // Return the entry with 200 status code
            return Ok(res);
        }

        /// <summary>
        /// Creates a new entry.
        /// </summary>
        /// <param name="entry">The entry to create.</param>
        /// <returns>The created entry.</returns>
        [HttpPost]
        public async Task<ActionResult<Entry>> CreateEntry(Entry entry)
        {
            Entry res = null!;

            // Check if the model state is valid
            if (ModelState.IsValid)
            {
                // Call the service to create a new entry asynchronously
                res = await entryService.CreateEntryAsync(entry);
            }

            if (res == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError); // Return 500 if there was an error creating the entry
            }

            // Return the created entry with 201 status code and location header
            return CreatedAtAction(nameof(GetEntry), new { id = res.Id }, res);
        }

        /// <summary>
        /// Updates an existing entry.
        /// </summary>
        /// <param name="id">The entry ID (GUID).</param>
        /// <param name="entry">The updated entry object.</param>
        /// <returns>A status code indicating the result of the update operation.</returns>
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<Entry>> UpdateEntry(Guid id, Entry entry)
        {
            // Validate the entry ID
            if (id != entry.Id)
            {
                return BadRequest(); // Return 400 if the entry ID does not match
            }

            // Call the service to update the entry asynchronously
            var res = await entryService.UpdateEntryAsync(entry);

            if (res == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError); // Return 500 if there was an error updating the entry
            }

            // Return 202 status code indicating the update operation was accepted
            return Accepted(res);
        }

        /// <summary>
        /// Deletes an entry by ID.
        /// </summary>
        /// <param name="id">The entry ID (GUID).</param>
        /// <returns>A status code indicating the result of the delete operation.</returns>
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<Entry>> DeleteEntry(Guid id)
        {
            // Call the service to delete the entry asynchronously
            var res = await entryService.DeleteEntryAsync(id);

            if (res == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError); // Return 500 if there was an error deleting the entry
            }

            // Return 202 status code indicating the delete operation was accepted
            return Accepted(res);
        }
    }
}

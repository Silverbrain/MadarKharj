using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PersonalFinance.Components.Pages;
using PersonalFinance.Service;
using PersonalFinance.Shared;

namespace PersonalFinance
{
    /// <summary>
    /// API Controller for managing accounts.
    /// </summary>
    [Route("/api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService accountService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountController"/> class.
        /// </summary>
        /// <param name="accountService">The account service.</param>
        public AccountController(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        /// <summary>
        /// Gets all accounts.
        /// </summary>
        /// <returns>A list of accounts.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Account>>> GetAccounts()
        {
            // Call the service to get all accounts asynchronously
            return Ok(await accountService.GetAccountsAsync());
        }

        /// <summary>
        /// Gets an account by ID.
        /// </summary>
        /// <param name="id">The account ID.</param>
        /// <returns>The requested account.</returns>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Account>> GetAccount(int id)
        {
            // Call the service to get an account by ID asynchronously
            var acc = await accountService.GetAccountAsync(id);
            if (acc == null)
            {
                return NotFound(); // Return 404 if the account is not found
            }

            return Ok(acc); // Return the account with 200 status code
        }

        /// <summary>
        /// Creates a new account.
        /// </summary>
        /// <param name="account">The account to create.</param>
        /// <returns>The created account.</returns>
        [HttpPost]
        public async Task<ActionResult<Account>> CreateAccount(Account account)
        {
            // Validate the account object
            if (account == null)
            {
                return BadRequest("Account object cannot be null."); // Return 400 if the account object is null
            }

            // Call the service to create a new account asynchronously
            var res = await accountService.CreateAccountAsync(account);

            // Return the created account with 201 status code and location header
            return CreatedAtAction(nameof(GetAccount), new { id = res.Id }, res);
        }

        /// <summary>
        /// Updates an existing account.
        /// </summary>
        /// <param name="id">The account ID.</param>
        /// <param name="account">The updated account object.</param>
        /// <returns>A status code indicating the result of the update operation.</returns>
        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateAccount(int id, Account account)
        {
            // Validate the account ID
            if (id != account.Id)
            {
                return BadRequest(); // Return 400 if the account ID does not match
            }

            // Call the service to update the account asynchronously
            var res = await accountService.UpdateAccountAsync(account);

            // Return 202 status code indicating the update operation was accepted
            return StatusCode(StatusCodes.Status202Accepted, res);
        }

        /// <summary>
        /// Deletes an account by ID.
        /// </summary>
        /// <param name="id">The account ID.</param>
        /// <returns>A status code indicating the result of the delete operation.</returns>
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteAccount(int id)
        {
            // Call the service to delete the account asynchronously
            var res = await accountService.DeleteAccountAsync(id);

            // Return 202 status code indicating the delete operation was accepted
            return StatusCode(StatusCodes.Status202Accepted, res);
        }
    }
}

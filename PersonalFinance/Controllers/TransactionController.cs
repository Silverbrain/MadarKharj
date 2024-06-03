using PersonalFinance.Shared; // Importing necessary libraries
using Microsoft.AspNetCore.Mvc;
using PersonalFinance.Service;

namespace PersonalFinance
{
    /// <summary>
    /// API Controller for managing transactions.
    /// </summary>
    [Route("/api/[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService service;

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionController"/> class.
        /// </summary>
        /// <param name="service">The transaction service.</param>
        public TransactionController(ITransactionService service)
        {
            this.service = service;
        }

        /// <summary>
        /// Gets all transactions.
        /// </summary>
        /// <returns>A list of transactions.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Transaction>>> GetTransactions()
        {
            // Call the service to get all transactions asynchronously
            return Ok(await service.GetTransactionsAsync());
        }

        /// <summary>
        /// Gets a transaction by ID.
        /// </summary>
        /// <param name="id">The transaction ID (GUID).</param>
        /// <returns>The requested transaction.</returns>
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Transaction>> GetTransaction(Guid id)
        {
            // Call the service to get a transaction by ID asynchronously
            var res = await service.GetTransactionAsync(id);

            if (res == null)
            {
                return NotFound(); // Return 404 if the transaction is not found
            }

            // Return the transaction with 200 status code
            return Ok(res);
        }

        /// <summary>
        /// Creates a new transaction.
        /// </summary>
        /// <param name="tran">The transaction to create.</param>
        /// <returns>The created transaction.</returns>
        [HttpPost]
        public async Task<ActionResult<Transaction>> CreateTransaction(Transaction tran)
        {
            // Validate the transaction object
            if (tran == null)
            {
                return BadRequest(); // Return 400 if the transaction object is null
            }

            // Call the service to create a new transaction asynchronously
            var result = await service.CreateTransactionAsync(tran);

            // Return the created transaction with 201 status code and location header
            return CreatedAtAction(nameof(GetTransaction), new { Id = result.Id }, result);
        }

        /// <summary>
        /// Deletes a transaction by ID.
        /// </summary>
        /// <param name="id">The transaction ID (GUID).</param>
        /// <returns>A status code indicating the result of the delete operation.</returns>
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<Transaction>> DeleteTransaction(Guid id)
        {
            // Call the service to delete the transaction asynchronously
            var result = await service.DeleteTransactionAsync(id);

            // Return 202 status code indicating the delete operation was accepted
            return Accepted(result);
        }

        /// <summary>
        /// Updates an existing transaction.
        /// </summary>
        /// <param name="id">The transaction ID (GUID).</param>
        /// <param name="tran">The updated transaction object.</param>
        /// <returns>A status code indicating the result of the update operation.</returns>
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<Transaction>> UpdateTransaction(Guid id, Transaction tran)
        {
            // Validate the transaction ID
            if (id != tran.Id)
            {
                return BadRequest(); // Return 400 if the transaction ID does not match
            }

            // Call the service to update the transaction asynchronously
            var result = await service.UpdateTransactionAsync(tran);

            // Return 202 status code indicating the update operation was accepted
            return Accepted(result);
        }
    }
}

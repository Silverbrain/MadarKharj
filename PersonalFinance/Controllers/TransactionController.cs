using PersonalFinance.Shared;
using Microsoft.AspNetCore.Mvc;
using PersonalFinance.Service;

namespace PersonalFinance;

[Route("/api/[controller]")]
public class TransactionController : ControllerBase
{
    private readonly ITransactionService service;

    public TransactionController(ITransactionService service)
    {
        this.service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Transaction>>> GetTransactions()
    {
        return Ok(await service.GetTransactionsAsync());
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Transaction>> GetTransaction(Guid id)
    {
        var res = await service.GetTransactionAsync(id);

        if (res == null)
        {
            return NotFound();
        }

        return Ok(res);
    }

    [HttpPost]
    public async Task<ActionResult<Transaction>> CreateTransaction(Transaction tran)
    {
        if (tran == null)
        {
            return BadRequest();
        }

        var result = await service.CreateTransactionAsync(tran);

        return CreatedAtAction(nameof(GetTransaction), new { Id = result.Id }, result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<Transaction>> DeleteTransaction(Guid id)
    {
        var result = await service.DeleteTransactionAsync(id);

        return Accepted(result);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<Transaction>> UpdateTransaction(Guid id, Transaction tran)
    {
        if(id != tran.Id)
        {
            return BadRequest();
        }

        var result = await service.UpdateTransactionAsync(tran);
        
        return Accepted(result);
    }
}

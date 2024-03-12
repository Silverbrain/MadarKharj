using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PersonalFinance.Components.Pages;
using PersonalFinance.Service;
using PersonalFinance.Shared;

namespace PersonalFinance;

[Route("/api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IAccountService accountService;

    public AccountController(IAccountService accountService)
    {
        this.accountService = accountService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Account>>> GetAccounts()
    {
        return Ok(await accountService.GetAccountsAsync());
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Account>> GetAccount(int id)
    {
        var acc = await accountService.GetAccountAsync(id);
        if (acc == null)
        {
            return NotFound();
        }

        return Ok(acc);
    }

    [HttpPost]
    public async Task<ActionResult<Account>> CreateAccount(Account account)
    {
        if (account == null)
        {
            return BadRequest("Account object can not be null.");
        }

        var res = await accountService.CreateAccountAsync(account);

        return CreatedAtAction(nameof(GetAccount), new { id = res.Id }, res);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> UpdateAccount(int id, Account account)
    {
        if (id != account.Id)
        {
            return BadRequest();
        }

        var updatedAcc = await accountService.UpdateAccountAsync(account);

        if (updatedAcc == null)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteAccount(int id)
    {
        if (id == null)
        {
            return BadRequest();
        }

        var res = await accountService.DeleteAccountAsync(id);

        if(res == null)
        {
            return NotFound();
        }

        return NoContent();
    }
}

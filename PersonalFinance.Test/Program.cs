// See https://aka.ms/new-console-template for more information
using System.Net.Http.Headers;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.IdentityModel.Tokens;
using PersonalFinance.Repository;
using PersonalFinance.Shared;

PFDbContext pFDb = new PFDbContext();
var dbAccess= new DataAccessTest(pFDb);

// Console.Write("Input the account name >");
// var newAccName = Console.ReadLine();

// if(!newAccName.IsNullOrEmpty())
// {
//     var res = dbAccess.CreateAccount(new Account{Name = newAccName});
//     Console.WriteLine(res);
// }

// foreach(var acc in dbAccess.GetAccounts())
// {
//     Console.WriteLine($"Id: {acc.Id}, Name: {acc.Name}");
// }

// Console.Write("Input the account Id to be deleted > ");
// int accId = int.Parse(Console.ReadLine());

// var res = dbAccess.DeleteAccount(accId);
// Console.WriteLine(res);

// foreach(var acc in dbAccess.GetAccounts())
// {
//     Console.WriteLine($"Id: {acc.Id}, Name: {acc.Name}");
// }

// Transaction tran = new Transaction{Id = Guid.NewGuid(), Date = DateTime.Now, Description = "New transaction for test"};
// var res = dbAccess.CreateTransaction(tran);
// Console.WriteLine(res);
//"442f76e0-a619-45c8-960c-4ff3f4b9f136"

// var tranId = new Guid("442f76e0-a619-45c8-960c-4ff3f4b9f136");

// Console.Write("Input the amount < 1 (Credit) for the entry >\n");
// var amount = float.Parse(Console.ReadLine());

// Console.Write("Input the account id for this entry > ");
// var accId = int.Parse(Console.ReadLine());

// Entry credEntry = new Entry{Id = Guid.NewGuid(), AccountId = accId, Amount = amount, TransactionId = tranId};

// Console.WriteLine(dbAccess.CreateEntry(credEntry));

// Console.Write("Input the amount > 1 (Debit) for the entry >\n");
// amount = float.Parse(Console.ReadLine());

// Console.Write("Input the account id for this entry > ");
// accId = int.Parse(Console.ReadLine());

// Entry debEntry = new Entry{Id = Guid.NewGuid(), AccountId = accId, Amount = amount, TransactionId = tranId};

// Console.WriteLine(dbAccess.CreateEntry(debEntry));

foreach(var tran in dbAccess.GetTransactions())
{
    var fromAcc = tran.Entries.First(e => e.Amount < 0).Account;
    var toAcc = tran.Entries.First(e => e.Amount > 0).Account;
    var debEntry = tran.Entries.First(e => e.Amount > 0);
    Console.WriteLine($@"Transaction Id: {tran.Id}
Date: {tran.Date}
From: {fromAcc.Name}
To: {toAcc.Name}
Amount: {debEntry.Amount}");
}
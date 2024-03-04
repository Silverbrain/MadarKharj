using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;

namespace PersonalFinance.Shared;

public class Transaction
{
    [Key]
    public Guid Id { get; set; }
    public DateTime Date { get; set; }
    public string? Description { get; set; }

    public IEnumerable<Entry>? Entries { get; set; }
}

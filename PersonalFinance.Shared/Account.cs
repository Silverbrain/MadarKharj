using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonalFinance.Shared;

public class Account
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string? Name { get; set; }

    public IEnumerable<Entry>? Entries { get; set; }
}

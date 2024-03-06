using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PersonalFinance.Shared;

public class Account
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string? Name { get; set; }

    public IEnumerable<Entry>? Entries { get; set; }

    public override string ToString()
    {
        var acc = new StringBuilder("{");

        acc.Append($"\"ID\" : \"{Id}\"");
        acc.Append($", \"Name\" : \"{Name}\"");
        acc.Append("}");
        
        return acc.ToString();
    }
}
﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonalFinance.Shared;

public class Entry
{
    [Key]
    public Guid Id { get; set; }
    public double Amount { get; set; }

    public int AccountId { get; set; }

    [ForeignKey("AccountId")]
    public Account? Account { get; set; }

    public Guid TransactionId { get; set; }

    [ForeignKey("TransactionId")]
    public Transaction? Transaction { get; set; }
}

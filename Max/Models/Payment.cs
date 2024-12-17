using System;
using System.Collections.Generic;

namespace Max.Models;

public partial class Payment
{
    public int Id { get; set; }

    public int? Reservation { get; set; }

    public decimal Amount { get; set; }

    public DateOnly PaymentDate { get; set; }

    public string? PaymentMethod { get; set; }
}

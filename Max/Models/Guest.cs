using System;
using System.Collections.Generic;

namespace Max.Models;

public partial class Guest
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Phone { get; set; }

    public string DocumentNumber { get; set; } = null!;

    public DateOnly CheckIn { get; set; }

    public DateOnly CheckOut { get; set; }
}

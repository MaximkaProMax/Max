using System;
using System.Collections.Generic;

namespace Max.Models;

public partial class Rent
{
    public int Id { get; set; }

    public string? Floor { get; set; }

    public int? RoomId { get; set; }

    public string? Category { get; set; }

    public int? GuestId { get; set; }

    public DateOnly? Checkin { get; set; }

    public DateOnly? Checkout { get; set; }
}

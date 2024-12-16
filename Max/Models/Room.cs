using System;
using System.Collections.Generic;

namespace Max.Models;

public partial class Room
{
    public int Id { get; set; }

    public string? Floor { get; set; }

    public int? Room1 { get; set; }

    public string? Category { get; set; }

    public string? Status { get; set; }
}

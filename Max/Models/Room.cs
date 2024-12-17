using System;
using System.Collections.Generic;

namespace Max.Models;

public partial class Room
{
    public int Id { get; set; }

    public string Floor { get; set; } = null!;

    public int Number { get; set; }

    public string Category { get; set; } = null!;

    public string? Status { get; set; }
}

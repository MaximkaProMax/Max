using System;
using System.Collections.Generic;

namespace Max.Models;

public partial class Service
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Price { get; set; }

    public string? Description { get; set; }
}

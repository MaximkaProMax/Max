using System;
using System.Collections.Generic;

namespace Max.Models;

public partial class Guest
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Surname { get; set; }

    public string? Patronym { get; set; }

    public DateOnly? Birthdate { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public string? Passport { get; set; }
}

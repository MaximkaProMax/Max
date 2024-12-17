using System;
using System.Collections.Generic;

namespace Max.Models;

public partial class RoomAccess
{
    public int Id { get; set; }

    public int? GuestId { get; set; }

    public int? RoomId { get; set; }

    public string AccessCardCode { get; set; } = null!;

    public string Status { get; set; } = null!;
}

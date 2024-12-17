using System;
using System.Collections.Generic;

namespace Max.Models;

public partial class Reservation
{
    public int Id { get; set; }

    public int? GuestId { get; set; }

    public int? RoomId { get; set; }

    public DateOnly CheckInDate { get; set; }

    public DateOnly CheckOutDate { get; set; }

    public string Status { get; set; } = null!;
}

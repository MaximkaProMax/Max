using System;
using System.Collections.Generic;

namespace Max.Models;

public partial class CleaningSchedule
{
    public int Id { get; set; }

    public int? RoomId { get; set; }

    public DateOnly CleaningDate { get; set; }

    public int? CleanerId { get; set; }

    public string Status { get; set; } = null!;
}

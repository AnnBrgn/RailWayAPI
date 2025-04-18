using System;
using System.Collections.Generic;

namespace RailWayAPI.Models;

public partial class Ticket
{
    public int? IdTrain { get; set; }

    public int? IdRoute { get; set; }

    public TimeOnly? Departure { get; set; }

    public string? Arrival { get; set; }

    public DateOnly? Date { get; set; }

    public int Id { get; set; }

    public int? IdUser { get; set; }

    public decimal? Cost { get; set; }

    public string? Type { get; set; }

    public virtual Route? IdRouteNavigation { get; set; }

    public virtual User? IdUserNavigation { get; set; }
}

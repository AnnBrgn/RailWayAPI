using System;
using System.Collections.Generic;

namespace RailWayAPI.Models;

public partial class Train
{
    public int Id { get; set; }

    public string? TypeTrain { get; set; }

    public int? CountPassengers { get; set; }

    public virtual ICollection<Table> Tables { get; set; } = new List<Table>();

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}

using System;
using System.Collections.Generic;

namespace RailWayAPI.Models;

public partial class Train
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public int? CountPassengers { get; set; }

    public virtual ICollection<Table> Tables { get; set; } = new List<Table>();
}

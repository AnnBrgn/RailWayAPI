using System;
using System.Collections.Generic;

namespace RailWayAPI.Models;

public partial class Station
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public virtual ICollection<Table> Tables { get; set; } = new List<Table>();
}

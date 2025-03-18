using System;
using System.Collections.Generic;

namespace RailWayAPI.Models;

public partial class CrossStationRoute
{
    public int? IdStation { get; set; }

    public int? IdRoute { get; set; }

    public TimeOnly? Time { get; set; }

    public virtual Route? IdRouteNavigation { get; set; }

    public virtual Station? IdStationNavigation { get; set; }
}

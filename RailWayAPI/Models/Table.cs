using System;
using System.Collections.Generic;

namespace RailWayAPI.Models;

public partial class Table
{
    public int Id { get; set; }

    public int? IdTrain { get; set; }

    public int? IdRoute { get; set; }

    public int? IdStation { get; set; }

    public TimeOnly? Time { get; set; }

    public virtual Route? IdRouteNavigation { get; set; }

    public virtual Station? IdStationNavigation { get; set; }

    public virtual Train? IdTrainNavigation { get; set; }
}

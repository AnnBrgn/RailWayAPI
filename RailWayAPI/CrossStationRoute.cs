using RailWayAPI.Models;
namespace RailWayAPI;

public partial class CrossStationRoute
{
    public int? IdStation { get; set; }

    public int? IdRoute { get; set; }

    public TimeOnly? Time { get; set; }

    public virtual Models.Route? IdRouteNavigation { get; set; }

    public virtual Station? IdStationNavigation { get; set; }
}

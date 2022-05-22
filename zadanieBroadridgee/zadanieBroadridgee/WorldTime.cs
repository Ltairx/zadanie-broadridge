namespace zadanieBroadridgee;

public class WorldTime
{
    public DateTimeOffset? datetime { get; set; }
    public string? timezones { get; set; }

    public WorldTime(DateTimeOffset? dt, string? tz)
    {
        datetime = dt;
        timezones = tz;
    }
}

namespace TheForgottenQuest.Events
{
    public class Event
    {
        public string Id { get; set; }
        public string Question { get; set; } = string.Empty;
        public Dictionary<string, Result> PositiveResults { get; set; } = new Dictionary<string, Result>();
        public Dictionary<string, Result> NegativeResults { get; set; } = new Dictionary<string, Result>();
        public int? NextEventId { get; set; }
        public string? Condition { get; set; }
    }
}

using System.Collections.Generic;

namespace TheForgottenQuest
{
    public class EventSet
    {
        public List<Event> MainQuest { get; set; } = new List<Event>();
        public List<Event> Level1To20 { get; set; } = new List<Event>();
        public List<Event> Level21To40 { get; set; } = new List<Event>();
    }
}

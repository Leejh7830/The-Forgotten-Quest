using System.Collections.Generic;
using The_Forgotten_Quest;
using TheForgottenQuest.User;

namespace TheForgottenQuest.Events
{
    public class EventSet
    {
        public List<Event> MainQuest { get; set; } = new List<Event>();
        public List<Event> Level1To20 { get; set; } = new List<Event>();
        public List<Event> Level21To40 { get; set; } = new List<Event>();

        

    }
}

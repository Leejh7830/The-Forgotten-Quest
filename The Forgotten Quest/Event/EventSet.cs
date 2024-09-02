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


        // 새로운 서브 이벤트 ID 리스트 추가
        public List<string> SubEventIds { get; set; } = new List<string> { "1", "10", "21" };
    }
}

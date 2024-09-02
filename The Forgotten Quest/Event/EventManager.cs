using TheForgottenQuest.User;

namespace TheForgottenQuest.Events
{
    public static class EventManager
    {
        private static Random random = new Random();
        private static EventSet events = new EventSet();

        public static void Initialize(string filePath)
        {
            events = EventLoader.LoadEvents(filePath);
        }

        public static void RunMainQuest(UserDTO player, string filePath)
        {
            Initialize(filePath);
            string EventID = player.CurrentMainQuestEventId;

            bool shouldExit = false;

            while (!string.IsNullOrEmpty(EventID) && !shouldExit)
            {
                Console.Clear();
                Utility.DisplayStats(player);
                var gameEvent = events.MainQuest.Find(e => e.Id.ToString() == EventID);

                while (gameEvent != null && !EventConditionChecker.CheckCondition(gameEvent, player, out string failedCondition))
                {
                    Console.Clear();
                    Utility.DisplayStats(player);
                    Console.WriteLine("메인 퀘스트 조건이 충족되지 않았습니다. 일반 이벤트를 실행합니다.");
                    Console.WriteLine($"조건: {failedCondition}\n");
                    Thread.Sleep(500);

                    RepeatSubEventsUntilConditionMet(gameEvent, player);

                    gameEvent = events.MainQuest.Find(e => e.Id.ToString() == EventID);
                }

                EventID = EventProcessor.ProcessEventSelection(gameEvent, player);
                if (EventID == null)
                {
                    shouldExit = true;
                }
            }

            if (!shouldExit)
            {
                RunRandomSubEvent(player);
            }
        }

        public static void RunRandomSubEvent(UserDTO player)
        {
            if (player.Level <= 20)
            {
                RunSubEvent(events.Level1To20, player);
            }
            else if (player.Level <= 40)
            {
                RunSubEvent(events.Level21To40, player);
            }
        }

        private static void RunSubEvent(List<Event> eventList, UserDTO player)
        {
            var randomizableEvents = eventList.Where(e => !e.IsSequential).ToList();
            if (randomizableEvents == null || randomizableEvents.Count == 0)
            {
                Utility.SlowType("진행 가능한 이벤트를 찾을 수 없습니다.");
                return;
            }

            int eventIndex = random.Next(randomizableEvents.Count);
            var gameEvent = randomizableEvents[eventIndex];

            string nextEventId = EventProcessor.ProcessEventSelection(gameEvent, player);

            if (!string.IsNullOrEmpty(nextEventId))
            {
                RunSubEvent(eventList, player);
            }
        }

        private static void RepeatSubEventsUntilConditionMet(Event gameEvent, UserDTO player)
        {
            do
            {
                RunRandomSubEvent(player);
            }
            while (!EventConditionChecker.CheckCondition(gameEvent, player, out _));
        }
    }
}

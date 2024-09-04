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

                    // 서브 이벤트 리스트를 EventSet에서 가져옴
                    RunSubEvent(events.SubEventIds, player);

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
                RunSubEvent(events.SubEventIds, player);
            }
        }

        private static void RunSubEvent(List<string> eventIdList, UserDTO player)
        {
            if (eventIdList == null || eventIdList.Count == 0)
            {
                Utility.SlowType("진행 가능한 이벤트를 찾을 수 없습니다.");
                return;
            }

            // 서브 이벤트 랜덤 선택
            string randomEventId = eventIdList[new Random().Next(eventIdList.Count)];
            var gameEvent = events.Level1To20.Find(e => e.Id.ToString() == randomEventId);

            if (gameEvent == null)
            {
                Utility.SlowType("올바른 이벤트를 찾을 수 없습니다.");
                Console.WriteLine("EventSet.cs에서 이벤트ID를 확인해 주세요.");
                Thread.Sleep(1000);
                return;
            }

            string nextEventId = EventProcessor.ProcessEventSelection(gameEvent, player);

            // 다음 이벤트가 전투 이벤트라면 전투 모드로 전환
            if (nextEventId?.StartsWith("combat_") == true)
            {
                EventProcessor.RunCombatEvent(gameEvent, player);
            }
            else if (!string.IsNullOrEmpty(nextEventId))
            {
                RunSubEvent(eventIdList, player); // 다음 이벤트 실행
            }
        }
    } 
}

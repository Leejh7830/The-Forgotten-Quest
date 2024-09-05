using TheForgottenQuest.Menu;
using TheForgottenQuest.User;

namespace TheForgottenQuest.Events
{
    public static class EventManager
    {
        private static Random random = new Random();
        private static EventSet events = new EventSet();
        private static bool shouldExit = false;

        public static void Initialize(string filePath)
        {
            events = EventLoader.LoadEvents(filePath);
        }

        public static void RunMainQuest(UserDTO player, string filePath)
        {
            Initialize(filePath);
            string EventID = player.CurrentMainQuestEventId;

            shouldExit = false; // 메인 메뉴로 나가기 전에 항상 플래그를 초기화

            while (!string.IsNullOrEmpty(EventID) && !shouldExit)
            {
                Console.Clear();
                Utility.DisplayStats(player);
                var gameEvent = events.MainQuest.Find(e => e.Id.ToString() == EventID);

                while (gameEvent != null && !EventConditionChecker.CheckCondition(gameEvent, player, out string failedCondition) && !shouldExit)
                {
                    Console.Clear();
                    Utility.DisplayStats(player);
                    Console.WriteLine("메인 퀘스트 조건이 충족되지 않았습니다. 일반 이벤트를 실행합니다.");
                    Console.WriteLine($"조건: {failedCondition}\n");
                    Thread.Sleep(500);

                    // 서브 이벤트 리스트를 EventSet에서 가져옴
                    RunSubEvent(events.SubEventIds, player);

                    if (shouldExit) return; // 서브 이벤트에서 메인 메뉴로 나가기로 결정되면 중단
                    gameEvent = events.MainQuest.Find(e => e.Id.ToString() == EventID);
                }

                string nextEventID = EventProcessor.ProcessEventSelection(gameEvent, player);

                if (nextEventID == null)
                {
                    shouldExit = true;
                }
                else
                {
                    EventID = nextEventID;
                }
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
                Console.WriteLine("메인 메뉴로 돌아갑니다...");
                Thread.Sleep(2000);
                
                shouldExit = true;
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

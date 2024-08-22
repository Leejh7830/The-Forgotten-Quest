using The_Forgotten_Quest;
using TheForgottenQuest.Events;
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
            while (!string.IsNullOrEmpty(EventID))
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
                    RunRandomSubEvent(player);

                    gameEvent = events.MainQuest.Find(e => e.Id.ToString() == EventID);
                }

                string nextEventID = RunMainEvent(events.MainQuest, EventID, player);
                EventID = nextEventID;
            }

            RunRandomSubEvent(player);
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

        private static string? RunMainEvent(List<Event> eventList, string currentEventId, UserDTO player)
        {
            var gameEvent = eventList.Find(e => e.Id.ToString() == currentEventId);

            if (gameEvent == null)
            {
                Utility.SlowType("올바른 이벤트를 찾을 수 없습니다.");
                return null;
            }
            Console.Clear();
            Utility.DisplayStats(player);
            string messageWithId = $"({gameEvent.Id}) {gameEvent.Question}"; // 이벤트앞에 ID 출력
            Utility.SlowType(messageWithId);
            Console.Write("어느 선택을 할까요? (1 또는 2 입력): ");
            string choice = Console.ReadLine();
            while (choice == null || (!gameEvent.PositiveResults.ContainsKey(choice) && !gameEvent.NegativeResults.ContainsKey(choice)))
            {
                Utility.SlowType("올바른 선택을 하지 않았습니다. 1 또는 2 중 하나를 선택하세요.");
                choice = Console.ReadLine();
            }

            Result result = Utility.DisplayRollResult(player, gameEvent, choice);

            // return gameEvent.NextEventId ?? -1; // 이벤트단위로 고정 진행
            return result.NextEventId ?? "defaultEventId"; // 결과에 따른 분기 진행
        }

        private static void RunSubEvent(List<Event> eventList, UserDTO player)
        {
            if (eventList == null || eventList.Count == 0)
            {
                Utility.SlowType("올바른 이벤트를 찾을 수 없습니다.");
                return;
            }

            // 랜덤이벤트 선택
            int eventIndex = random.Next(eventList.Count);
            var gameEvent = eventList[eventIndex];

            Utility.SlowType(gameEvent.Question);
            Utility.SlowType("어느 선택을 할까요? (1 또는 2 입력): ");
            string choice = Console.ReadLine();

            while (choice == null || (!gameEvent.PositiveResults.ContainsKey(choice) && !gameEvent.NegativeResults.ContainsKey(choice)))
            {
                Utility.SlowType("올바른 선택을 하지 않았습니다. 1 또는 2 중 하나를 선택하세요.");
                choice = Console.ReadLine();
            }

            Result result = Utility.DisplayRollResult(player, gameEvent, choice);

            // 세이브 이벤트 처리
            if (choice == "1" && result.SaveGame)
            {
                Utility.SavePlayer(player, JsonConstants.PlayerFilePath); // 저장
            }
        }
    }
}

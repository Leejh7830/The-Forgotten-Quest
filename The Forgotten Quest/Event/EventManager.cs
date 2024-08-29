using The_Forgotten_Quest;
using TheForgottenQuest.Events;
using TheForgottenQuest.Menu;
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

            bool shouldExit = false; // 메인 메뉴로 나가야 하는지 여부를 추적하는 플래그

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
                    RunRandomSubEvent(player);

                    gameEvent = events.MainQuest.Find(e => e.Id.ToString() == EventID);
                }

                string nextEventID = RunMainEvent(events.MainQuest, EventID, player);

                // 만약 nextEventID가 null이라면, 메인 메뉴로 나가고 이후 코드 실행하지 않음
                if (nextEventID == null)
                {
                    shouldExit = true; // 메인 메뉴로 나가야 함을 표시
                }
                else
                {
                    EventID = nextEventID;
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
            string messageWithId = $"({gameEvent.Id}) {gameEvent.Question}"; // 이벤트 앞에 ID 출력
            Utility.SlowType(messageWithId);
            Console.Write("어느 선택을 할까요? (1, 2 입력 또는 ESC를 눌러 저장): \n");

            ConsoleKeyInfo keyInfo;
            string choice = null;
            
            do
            {
                keyInfo = Console.ReadKey(true);

                if (keyInfo.Key == ConsoleKey.Escape)
                {
                    // ESC 키를 누르면 저장 기능 실행
                    Utility.SavePlayer(player, JsonConstants.PlayerFilePath); // 저장;
                    // MenuManager.ShowMainMenu(); // 메인메뉴로 이동
                    return null;
                }
                else if (keyInfo.Key == ConsoleKey.D1 || keyInfo.Key == ConsoleKey.NumPad1 ||
                        keyInfo.Key == ConsoleKey.D2 || keyInfo.Key == ConsoleKey.NumPad2)
                {
                    choice = keyInfo.KeyChar.ToString();
                }
                else
                {
                    Utility.SlowType("올바른 선택을 하지 않았습니다. 1, 2 또는 ESC 키를 누르세요.");
                }

            } while (choice == null);

            Result result = Utility.DisplayRollResult(player, gameEvent, choice);

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

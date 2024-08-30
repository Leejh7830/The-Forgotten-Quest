using The_Forgotten_Quest;
using The_Forgotten_Quest.Event;
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

                EventID = ProcessEventSelection(gameEvent, player);
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

            // 전투 이벤트인지 확인
            if (gameEvent.Id.StartsWith("combat_"))
            {
                return EventProcessor.RunCombatEvent(gameEvent, player); // 전투 이벤트로 바로 진입
            }

            // 전투 이벤트가 아닌 경우 선택지 제공
            Console.Write("어느 선택을 할까요? (1, 2 입력 또는 ESC를 눌러 저장): ");
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
            var randomizableEvents = eventList.Where(e => !e.IsSequential).ToList();
            if (randomizableEvents == null || randomizableEvents.Count == 0)
            {
                Utility.SlowType("진행 가능한 이벤트를 찾을 수 없습니다.");
                return;
            }

            int eventIndex = random.Next(randomizableEvents.Count);
            var gameEvent = randomizableEvents[eventIndex];

            string nextEventId = ProcessEventSelection(gameEvent, player);

            if (!string.IsNullOrEmpty(nextEventId))
            {
                RunSubEvent(eventList, player);
            }
        }

        private static string? ProcessEventSelection(Event gameEvent, UserDTO player)
        {
            Utility.SlowType(gameEvent.Question);
            Utility.SlowType("어느 선택을 할까요? (1, 2 입력 또는 ESC를 눌러 저장): ");

            ConsoleKeyInfo keyInfo;
            string choice = null;

            do
            {
                keyInfo = Console.ReadKey(true);

                if (keyInfo.Key == ConsoleKey.Escape)
                {
                    Utility.SavePlayer(player, JsonConstants.PlayerFilePath);
                    return null; // ESC 누르면 메인메뉴로 이동
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

            // 전투이벤트 진행 시
            if (result.NextEventId?.StartsWith("combat_") == true)
            {
                return EventProcessor.RunCombatEvent(gameEvent, player);
            }

            return result.NextEventId ?? "defaultEventId";
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

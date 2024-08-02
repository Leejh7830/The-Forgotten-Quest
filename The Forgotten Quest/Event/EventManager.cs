﻿  using TheForgottenQuest.Events;
using TheForgottenQuest.User;

namespace TheForgottenQuest.Events
{

    public static class EventManager
    {
        private static Random random = new Random();
        private static EventSet events = new EventSet();
        private static int currentMainQuestEventId = 1; // 메인 퀘스트 시작 ID

        public static void Initialize(string filePath)
        {
            events = EventLoader.LoadEvents(filePath);
        }

        public static void RunMainQuest(User.UserDTO player, string filePath)
        {
            Initialize(filePath);
            while (currentMainQuestEventId > 0)
            {
                var gameEvent = events.MainQuest.Find(e => e.Id == currentMainQuestEventId);

                while (gameEvent != null && !EventConditionChecker.CheckCondition(gameEvent, player, out string failedCondition))
                {
                    Utility.SlowType("메인 퀘스트 조건이 충족되지 않았습니다. 일반 이벤트를 실행합니다.");
                    Utility.SlowType($"조건: {failedCondition}\n");
                    Thread.Sleep(1000);
                    RunRandomSubEvent(player);

                    gameEvent = events.MainQuest.Find(e => e.Id == currentMainQuestEventId);
                }

                int nextEventId = RunMainEvent(events.MainQuest, currentMainQuestEventId, player);
                currentMainQuestEventId = nextEventId;
            }

            RunRandomSubEvent(player);
        }

        public static void RunRandomSubEvent(User.UserDTO player)
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

        private static int RunMainEvent(List<Event> eventList, int currentEventId, User.UserDTO player)
        {
            var gameEvent = eventList.Find(e => e.Id == currentEventId);

            if (gameEvent == null)
            {
                Utility.SlowType("올바른 이벤트를 찾을 수 없습니다.");
                return -1;
            }

            Utility.SlowType(gameEvent.Question);
            Console.Write("어느 길로 갈까요? (1 또는 2 입력): ");
            string choice = Console.ReadLine();
            while (choice == null || (!gameEvent.PositiveResults.ContainsKey(choice) && !gameEvent.NegativeResults.ContainsKey(choice)))
            {
                Utility.SlowType("올바른 선택을 하지 않았습니다. 1 또는 2 중 하나를 선택하세요.");
                choice = Console.ReadLine();
            }

            int roll = random.Next(1, 101);
            Result result;

            if (roll > 50)
            {
                result = gameEvent.PositiveResults[choice];
            }
            else
            {
                result = gameEvent.NegativeResults[choice];
            }

            Utility.SlowType($"Dice: {roll}\n{result.Message}\n");
            EventResultApplier.ApplyResult(player, result);

            return gameEvent.NextEventId ?? -1;
        }

        private static void RunSubEvent(List<Event> eventList, User.UserDTO player)
        {
            if (eventList == null || eventList.Count == 0)
            {
                Utility.SlowType("올바른 이벤트를 찾을 수 없습니다.");
                return;
            }

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

            int roll = random.Next(1, 101);
            Result result;

            if (roll > 50)
            {
                result = gameEvent.PositiveResults[choice];
            }
            else
            {
                result = gameEvent.NegativeResults[choice];
            }

            Utility.SlowType($"Dice: {roll}\n{result.Message}\n");
            EventResultApplier.ApplyResult(player, result);
        }

        public static void EndGame()
        {
            Utility.SlowType("모험을 종료합니다. 감사합니다!");
        }
    }
}

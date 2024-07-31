using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace TheForgottenQuest
{
    public class Event
    {
        public int Id { get; set; }
        public string Question { get; set; } = string.Empty;
        public Dictionary<string, Result> PositiveResults { get; set; } = new Dictionary<string, Result>();
        public Dictionary<string, Result> NegativeResults { get; set; } = new Dictionary<string, Result>();
        public int? NextEventId { get; set; }
        public string? Condition { get; set; }
    }

    public class Result
    {
        public string Message { get; set; } = string.Empty;
        public int LevelChange { get; set; } = 0;
        public int HPChange { get; set; } = 0;
        public int MPChange { get; set; } = 0;
        public int LUKChange { get; set; } = 0;
    }

    public class EventSet
    {
        public List<Event> MainQuest { get; set; } = new List<Event>();
        public List<Event> Level1To20 { get; set; } = new List<Event>();
        public List<Event> Level21To40 { get; set; } = new List<Event>();
    }

    public static class EventManager
    {
        private static Random random = new Random();
        private static EventSet events = new EventSet();
        private static int currentMainQuestEventId = 1; // 메인 퀘스트 시작 ID

        public static void Initialize(string filePath)
        {
            var json = File.ReadAllText(filePath);
            events = JsonConvert.DeserializeObject<EventSet>(json) ?? new EventSet();
        }

        public static void RunMainQuest(User player)
        {
            while (currentMainQuestEventId > 0)
            {
                var gameEvent = events.MainQuest.Find(e => e.Id == currentMainQuestEventId);

                if (gameEvent != null && !CheckCondition(gameEvent, player, out string failedCondition))
                {
                    Console.WriteLine("메인 퀘스트 조건이 충족되지 않았습니다. 일반 이벤트를 실행합니다.");
                    Console.WriteLine($"조건: {failedCondition}");
                    RunRandomEvent(player);
                    return;
                }

                int nextEventId = RunMainEvent(events.MainQuest, currentMainQuestEventId, player);
                currentMainQuestEventId = nextEventId;
            }

            RunRandomEvent(player);
        }

        public static void RunRandomEvent(User player)
        {
            if (player.Level <= 20)
            {
                RunSubEvent(events.Level1To20, player);
            }
            else if (player.Level <= 40)
            {
                RunSubEvent(events.Level21To40, player);
            }
            // 여기서 레벨 별 서브퀘스트 추가
        }

        private static int RunMainEvent(List<Event> eventList, int currentEventId, User player)
        {
            var gameEvent = eventList.Find(e => e.Id == currentEventId);

            if (gameEvent == null)
            {
                Console.WriteLine("올바른 이벤트를 찾을 수 없습니다.");
                return -1;
            }

            Console.WriteLine(gameEvent.Question);
            Console.Write("어느 길로 갈까요? (1 또는 2 입력): ");
            string choice = Console.ReadLine();
            while (choice == null || (!gameEvent.PositiveResults.ContainsKey(choice) && !gameEvent.NegativeResults.ContainsKey(choice)))
            {
                Console.WriteLine("올바른 선택을 하지 않았습니다. 1 또는 2 중 하나를 선택하세요.");
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

            Console.WriteLine($"Dice: {roll}\n{result.Message}");
            ApplyResult(player, result);

            return gameEvent.NextEventId ?? -1;
        }

        private static void RunSubEvent(List<Event> eventList, User player)
        {
            int eventIndex = random.Next(eventList.Count);
            var gameEvent = eventList[eventIndex];

            Console.WriteLine(gameEvent.Question);
            Console.Write("어느 길로 갈까요? (1 또는 2 입력): ");
            string choice = Console.ReadLine();
            while (choice == null || (!gameEvent.PositiveResults.ContainsKey(choice) && !gameEvent.NegativeResults.ContainsKey(choice)))
            {
                Console.WriteLine("올바른 선택을 하지 않았습니다. 1 또는 2 중 하나를 선택하세요.");
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

            Console.WriteLine($"Dice: {roll}\n{result.Message}");
            ApplyResult(player, result);
        }

        private static void ApplyResult(User player, Result result)
        {
            if (result.LevelChange != 0)
            {
                player.Level += result.LevelChange;
                Console.WriteLine($"레벨이 {result.LevelChange}만큼 변경되었습니다. 현재 레벨: {player.Level}");
            }

            if (result.HPChange != 0)
            {
                player.HP += result.HPChange;
                Console.WriteLine($"HP가 {result.HPChange}만큼 변경되었습니다. 현재 HP: {player.HP}");
            }

            if (result.MPChange != 0)
            {
                player.MP += result.MPChange;
                Console.WriteLine($"MP가 {result.MPChange}만큼 변경되었습니다. 현재 MP: {player.MP}");
            }

            if (result.LUKChange != 0)
            {
                player.LUK += result.LUKChange;
                Console.WriteLine($"운이 {result.LUKChange}만큼 변경되었습니다. 현재 운: {player.LUK}");
            }
        }

        private static bool CheckCondition(Event gameEvent, User player, out string failedCondition)
        {
            failedCondition = string.Empty;

            if (gameEvent.Condition != null)
            {
                var condition = GetCondition(gameEvent.Condition);
                if (!condition(player))
                {
                    failedCondition = gameEvent.Condition;
                    return false;
                }
            }

            return true;
        }

        private static Func<User, bool> GetCondition(string condition)
        {
            switch (condition)
            {
                case "LevelAbove3":
                    return player => player.Level >= 3;
                default:
                    return player => true;
            }
        }

        public static void EndGame()
        {
            Console.WriteLine("모험을 종료합니다. 감사합니다!");
        }
    }
}

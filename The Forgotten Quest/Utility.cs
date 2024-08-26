using System;
using System.Numerics;
using System.Reflection.Emit;
using System.Xml.Linq;
using TheForgottenQuest.Events;
using TheForgottenQuest.Menu;
using TheForgottenQuest.User;

namespace TheForgottenQuest
{
    public static class Utility
    {
        private static Random random = new Random();

        public static void ShowLoading(string message, int duration, ref bool loading)
        {
            Console.Write(message);
            Console.Write(" [");
            int total = 30; // 로딩 게이지의 총 길이
            int interval = duration / total;

            for (int i = 0; i < total; i++)
            {
                Thread.Sleep(interval);
                Console.Write("=");
            }

            SlowType("]");
            Thread.Sleep(200);
            if (loading)
            {
                DisplayCodeLikeText(100);
                loading = false;
            }
            Console.Clear();
        }

        private static void DisplayCodeLikeText(int lines)
        {
            string[] codeLines = new string[]
            {
                "ublic class MyClass",
                "    ivate int[] numbers = new int[100];",
                "    pte HttpClient _client = new HttpClient();",
                "     MyClass()",
                "        for (int i = 0; i < numbers.Length; i++)",
                "        {",
                "            numbers[i] = i;",
                "    pvoid PrintEvenNumbers()",
                "        foreach (int number in numbers)",
                "            if (number % 2 == 0)",
                "            {",
                "                Utility.SlowType(number);",
                "    pic int Finci(int n)",
                "        if (n <= 1) return n;",
                "        return Fibonacci(n - 1) + Fiboi(n - 2);",
                "    public void Run()",
                "        Task.Run(async () =>",
                "   await Task.Delay(1000);",
                "   intEvenNumbers();",
                "    ublic async Task<string> GetDataAsync(string url)",
                "    {",
                "        using (var client = new HttpClient())",
                "        {",
                "            var response = await client.GetAsync(url);",
                "            return await response.Content.ReadAsStringAsync();",
                "    lic async Task ProcessDataAsync()",
                "    {",
                "        string data = await GetDataAsync(\"https://example.com\");",
                "        Utility.SlowType(data);",
                "    ic async Task<List<string>> FetchMultipleDataAsync(List<string> urls)",
                "    {",
                "        var tasks = urls.Select(url => GetDataAsync(url));",
                "        var results = await Task.WhenAll(tasks);",
                "        return results.ToList();",
                "       public void ComplexOperation()",
                "        for (int i = 0; i < 10; i++)",
                "            Parallel.For(0, 100, (j) =>",
                "                Utility.SlowType($\"Operation {i}, iteration {j}\");",
                "    c async Task ComputeDataAsync()",
                "    {",
                "        var data = await GetDataAsync(\"https://api.example.com/data\");",
                "        var processed = data.Split('\\n').Select(line => line.Trim()).ToList();",
                "        processed.ForEach(line => Utility.SlowType(line));",
            };

            for (int i = 0; i < lines; i++)
            {
                Utility.SlowType(codeLines[random.Next(codeLines.Length)]);
                Thread.Sleep(3); // 각 줄 출력 후 약간의 지연 시간
            }
        }


        // 진행중인 Player 저장
        public static void SavePlayer(UserDTO currentPlayer, string filePath)
        {
            List<UserDTO> allPlayers = UserLoader.LoadUsers(filePath);

            // 현재 플레이어의 정보를 갱신
            for (int i = 0; i < allPlayers.Count; i++)
            {
                if (allPlayers[i].Id == currentPlayer.Id)
                {
                    allPlayers[i] = currentPlayer;
                    break;
                }
            }

            // 모든 플레이어 데이터를 다시 저장
            UserSaver.SaveUsers(allPlayers, filePath);
        }


        public static void SlowType(string message)
        {
            foreach (char c in message)
            {
                Console.Write(c);
                Thread.Sleep(20); // 각 글자 출력 후 지연
            }
            Console.WriteLine();// 줄바꿈
            Thread.Sleep(200); // 문장 완료 후 추가 지연
        }

        public static void DisplayStats(UserDTO player)
        {
            Console.WriteLine("=======================================================");
            Console.WriteLine($"    {"ID:",-5} {player.Id}");
            Console.WriteLine($"    {"Name:",-5} {player.Name}");
            Console.WriteLine($"    {"Job:",-5} {player.Job}");
            Console.WriteLine($"    {"Level:",-5} {player.Level}");
            Console.WriteLine($"    {"EXP:",-5} {player.EXP} / 100");
            Console.WriteLine($"    {"HP:",-5} {player.BuffDebuff.ModHP} / {player.BuffDebuff.ModMaxHP}");
            Console.WriteLine($"    {"MP:",-5} {player.BuffDebuff.ModMP} / {player.BuffDebuff.ModMaxMP}");
            Console.WriteLine($"    {"STR:",-5} {player.STR}");
            Console.WriteLine($"    {"DEX:",-5} {player.DEX}");
            Console.WriteLine($"    {"INT:",-5} {player.INT}");
            Console.WriteLine($"    {"LUK:",-5} {player.BuffDebuff.ModLUK}");
            Console.WriteLine("=======================================================");
        }


        public static Result DisplayRollResult(UserDTO player ,Event gameEvent, string choice)
        {
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
            SlowType($"Dice: {roll}\n{result.Message}");
            EventResultApplier.ApplyResult(player, result);
            Console.WriteLine("다음으로 이동...");
            Console.ReadLine();

            return result;
        }

        public static void GameOver()
        {
            // 게임 오버 로직 구현
            Console.WriteLine("게임 오버!");
            Console.WriteLine("메인 메뉴로 돌아갑니다...");
            Console.ReadKey(); // 사용자 입력 대기

            MenuManager.ShowMainMenu();
        }
    }
}

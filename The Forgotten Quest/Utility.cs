using System;
using System.Threading;

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

            Console.WriteLine("]");
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
                "public class MyClass",
                "    private int[] numbers = new int[100];",
                "    private HttpClient _client = new HttpClient();",
                "    public MyClass()",
                "        for (int i = 0; i < numbers.Length; i++)",
                "        {",
                "            numbers[i] = i;",
                "    public void PrintEvenNumbers()",
                "        foreach (int number in numbers)",
                "            if (number % 2 == 0)",
                "            {",
                "                Console.WriteLine(number);",
                "    public int Finci(int n)",
                "        if (n <= 1) return n;",
                "        return Fibonacci(n - 1) + Fiboi(n - 2);",
                "    public void Run()",
                "        Task.Run(async () =>",
                "   await Task.Delay(1000);",
                "   PrintEvenNumbers();",
                "    public async Task<string> GetDataAsync(string url)",
                "    {",
                "        using (var client = new HttpClient())",
                "        {",
                "            var response = await client.GetAsync(url);",
                "            return await response.Content.ReadAsStringAsync();",
                "    public async Task ProcessDataAsync()",
                "    {",
                "        string data = await GetDataAsync(\"https://example.com\");",
                "        Console.WriteLine(data);",
                "    public async Task<List<string>> FetchMultipleDataAsync(List<string> urls)",
                "    {",
                "        var tasks = urls.Select(url => GetDataAsync(url));",
                "        var results = await Task.WhenAll(tasks);",
                "        return results.ToList();",
                "    public void ComplexOperation()",
                "        for (int i = 0; i < 10; i++)",
                "            Parallel.For(0, 100, (j) =>",
                "                Console.WriteLine($\"Operation {i}, iteration {j}\");",
                "    public async Task ComputeDataAsync()",
                "    {",
                "        var data = await GetDataAsync(\"https://api.example.com/data\");",
                "        var processed = data.Split('\\n').Select(line => line.Trim()).ToList();",
                "        processed.ForEach(line => Console.WriteLine(line));",
            };

            for (int i = 0; i < lines; i++)
            {
                Console.WriteLine(codeLines[random.Next(codeLines.Length)]);
                Thread.Sleep(3); // 각 줄 출력 후 약간의 지연 시간
            }
        }
    }
}

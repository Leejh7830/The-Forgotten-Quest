using System;
using System.Threading;

namespace TheForgottenQuest
{
    public static class Utility
    {
        public static void ShowLoading(string message, int duration)
        {
            Console.Write(message);
            Console.Write(" [");
            int total = 50; // 로딩 게이지의 총 길이
            int interval = duration / total;

            for (int i = 0; i < total; i++)
            {
                Thread.Sleep(interval);
                Console.Write("=");
            }

            Console.WriteLine("] 완료!");
            Thread.Sleep(200);
            Console.Clear();
        }
    }
}

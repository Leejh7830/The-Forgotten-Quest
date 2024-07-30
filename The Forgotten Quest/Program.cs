using System;

namespace TheForgottenQuest
{
    class Program
    {
        static void Main(string[] args)
        {
            StartGame();
        }

        static void StartGame()
        {
            Console.WriteLine("===== The Forgotten Quest =====");
            Console.WriteLine("모험 게임에 오신 것을 환영합니다!");
            Console.WriteLine("당신의 이름을 입력하세요:");
            string playerName = Console.ReadLine();

            Console.WriteLine($"안녕하세요, {playerName}님! 모험을 시작합니다.");
            FirstScenario(playerName);
        }

        static void FirstScenario(string playerName)
        {
            Console.WriteLine("당신은 잊혀진 퀘스트를 시작하게 됩니다.");
            Console.WriteLine("어두운 숲에 들어섰습니다. 앞에는 두 갈래 길이 있습니다.");
            Console.WriteLine("1. 왼쪽 길");
            Console.WriteLine("2. 오른쪽 길");
            Console.Write("어느 길로 갈까요? (1 또는 2 입력): ");

            string choice = Console.ReadLine();

            if (choice == "1")
            {
                Console.WriteLine("왼쪽 길을 따라가다 보니, 신비한 마을에 도착했습니다!");
                // 추가 시나리오 작성
            }
            else if (choice == "2")
            {
                Console.WriteLine("오른쪽 길을 따라가다 보니, 깊고 어두운 동굴에 도착했습니다!");
                // 추가 시나리오 작성
            }
            else
            {
                Console.WriteLine("올바른 선택을 하지 않았습니다. 게임을 종료합니다.");
            }

            EndGame();
        }

        static void EndGame()
        {
            Console.WriteLine("모험을 종료합니다. 감사합니다!");
        }
    }
}

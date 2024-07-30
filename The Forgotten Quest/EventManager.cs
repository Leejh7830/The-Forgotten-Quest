using System;
using System.Collections.Generic;

namespace TheForgottenQuest
{
    public static class EventManager
    {
        private static Random random = new Random();
        private static List<string> scenarioQuestions = new List<string>
        {
            "어두운 숲에 들어섰습니다. 앞에는 두 갈래 길이 있습니다. 1.왼쪽 길 / 2.오른쪽 길",
            "신비한 동굴 앞에 서 있습니다. 1.들어간다 / 2.돌아간다"
            // 추가 질문은 여기에 추가
        };

        private static List<Action<string>> scenarioResults = new List<Action<string>>
        {
            FirstScenarioResult1,
            FirstScenarioResult2
            // 추가 결과 처리는 여기에 추가
        };

        public static void RunRandomScenario()
        {
            int questionIndex = random.Next(scenarioQuestions.Count);
            int resultIndex = random.Next(scenarioResults.Count);

            Console.WriteLine("당신은 잊혀진 퀘스트를 시작하게 됩니다.");
            Console.WriteLine(scenarioQuestions[questionIndex]);
            Console.Write("어느 길로 갈까요? (1 또는 2 입력): ");

            string choice = Console.ReadLine();
            while (choice != "1" && choice != "2")
            {
                Console.WriteLine("올바른 선택을 하지 않았습니다. 1 또는 2 중 하나를 선택하세요.");
                choice = Console.ReadLine();
            }

            scenarioResults[resultIndex](choice);
        }

        public static void FirstScenarioResult1(string choice)
        {
            if (choice == "1")
            {
                Console.WriteLine("보물 상자를 발견했습니다!");
            }
            else if (choice == "2")
            {
                Console.WriteLine("깊고 어두운 동굴에 도착했습니다!");
            }
        }

        public static void FirstScenarioResult2(string choice)
        {
            if (choice == "1")
            {
                Console.WriteLine("안전하게 집에 도착했습니다.");
            }
            else if (choice == "2")
            {
                Console.WriteLine("신비한 마을에 도착했습니다.");
            }
        }

        public static void EndGame()
        {
            Console.WriteLine("모험을 종료합니다. 감사합니다!");
        }
    }
}

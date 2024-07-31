using System;

namespace TheForgottenQuest
{
    class Program
    {
        static void Main(string[] args)
        {
            bool loading = true;
            EventManager.Initialize("events.json"); // JSON 파일 경로 설정
            StartGame(ref loading);
        }

        static void StartGame(ref bool loading)
        {
            Utility.ShowLoading("", 500, ref loading);

            Console.WriteLine("\n===== The Forgotten Quest =====\n");

            Console.WriteLine("모험 게임에 오신 것을 환영합니다!");

            string playerName = CharacterSetup.GetName();
            string playerJob = CharacterSetup.GetJob();

            bool confirmation = CharacterSetup.ConfirmSelection(playerName, playerJob);
            while (!confirmation)
            {
                playerName = CharacterSetup.GetName();
                playerJob = CharacterSetup.GetJob();
                confirmation = CharacterSetup.ConfirmSelection(playerName, playerJob);
            }

            User player = new User(playerName, playerJob);
            Utility.ShowLoading("", 100, ref loading);

            Console.WriteLine($"안녕하세요, {player.Name}님! 모험을 시작합니다.\n");
            player.DisplayStats();

            // 메인 퀘스트 시작
            EventManager.RunMainQuest(player);

            // 메인 퀘스트 완료 후 일반 이벤트 시작
            while (true)
            {
                EventManager.RunRandomEvent(player);
                Console.WriteLine("다음 이벤트를 진행하시겠습니까? (y/n)");
                string continueGame = Console.ReadLine();
                if (continueGame?.ToLower() != "y")
                {
                    break;
                }
            }

            EventManager.EndGame();
        }
    }
}

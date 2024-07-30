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
            Utility.ShowLoading("", 500);

            Console.WriteLine("===== The Forgotten Quest =====");

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
            Utility.ShowLoading("", 500);

            Console.WriteLine($"안녕하세요, {player.Name}님! 모험을 시작합니다.\n");
            player.DisplayStats();

            EventManager.RunRandomScenario();
        }
    }
}

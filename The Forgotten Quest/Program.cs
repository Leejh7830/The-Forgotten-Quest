using System;

namespace TheForgottenQuest
{
    class Program
    {
        static void Main(string[] args)
        {
            bool loading = true;
            StartGame(ref loading);
        }

        static void StartGame(ref bool loading)
        {
            Utility.ShowLoading("", 500, ref loading);

            /////////////////////////////////////////////STEP 0 : 캐릭터 생성 단계/////////////////////////////////////////////

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
            Utility.ShowLoading("", 60, ref loading);

            Console.WriteLine($"안녕하세요, {player.Name}님! 모험을 시작합니다.\n");

            /////////////////////////////////////////////STEP 1 : 캐릭터 생성 완료/////////////////////////////////////////////

            player.DisplayStats();

            EventManager.RunMainQuest(player);
        }
    }
}

using The_Forgotten_Quest;
using TheForgottenQuest.Events;
using TheForgottenQuest.User;

namespace TheForgottenQuest.Menu
{
    public static class MenuManager
    {
        public static void ShowMainMenu()
        {
            Console.Clear();
            Console.WriteLine("===== The Forgotten Quest =====");
            Console.WriteLine("1. 새로하기 (New)");
            Console.WriteLine("2. 이어하기 (Load)");
            Console.WriteLine("3. 종료 (Exit)");
            Console.Write("선택하세요: ");
        }

        public static void StartNewGame(ref bool loading)
        {
            Console.Clear();
            Console.WriteLine("\n===== The Forgotten Quest =====\n");

            Console.WriteLine("모험 게임에 오신 것을 환영합니다!");

            string saveFilePath = "players.json";
            List<User.User> AllPlayers = UserLoader.LoadUsers(saveFilePath) ?? new List<User.User>();

            string NewPlayerName = UserSetup.GetName();
            string NewPlayerJob = UserSetup.GetJob();

            bool confirmation = UserSetup.ConfirmSelection(NewPlayerName, NewPlayerJob);
            while (!confirmation)
            {
                NewPlayerName = UserSetup.GetName();
                NewPlayerJob = UserSetup.GetJob();
                confirmation = UserSetup.ConfirmSelection(NewPlayerName, NewPlayerJob);
            }
            User.User NewPlayer = new User.User(NewPlayerName, NewPlayerJob);
            AllPlayers.Add(NewPlayer);
            
            UserSaver.SaveUsers(AllPlayers, saveFilePath);
            Console.WriteLine("캐릭터 정보가 저장되었습니다.");

            

            Utility.ShowLoading("", 100, ref loading);

            Console.WriteLine($"안녕하세요, {NewPlayer.Name}님! 모험을 시작합니다.\n");
            NewPlayer.DisplayStats();

            string filePath = "events.json";
            EventManager.RunMainQuest(NewPlayer, filePath);
        }


        public static void LoadGame(ref bool loading)
        {
            Console.Clear();
            string saveFilePath = "players.json";

            try
            {
                List<User.User> loadedPlayers = UserLoader.LoadUsers(saveFilePath) ?? new List<User.User>();

                if (loadedPlayers.Count == 0)
                {
                    // 캐릭터가 하나도 없을 경우 메시지를 출력하고 종료
                    Console.WriteLine("저장된 캐릭터가 없습니다. 메인 메뉴로 돌아갑니다.");
                    Console.WriteLine("계속하려면 아무 키나 누르세요...");
                    Console.ReadKey(); // 사용자가 키를 입력할 때까지 대기
                    return;
                }

                Console.WriteLine("저장된 캐릭터들:");

                for (int i = 0; i < loadedPlayers.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {loadedPlayers[i].Name} (Level: {loadedPlayers[i].Level}, Job: {loadedPlayers[i].Job})\n");
                }

                Console.Write("플레이할 캐릭터를 선택하세요: ");
                int selectedCharacterIndex;

                while (!int.TryParse(Console.ReadLine(), out selectedCharacterIndex) || selectedCharacterIndex < 1 || selectedCharacterIndex > loadedPlayers.Count)
                {
                    Console.WriteLine("올바른 선택을 하지 않았습니다. 다시 시도해주세요.");
                    Console.Write("플레이할 캐릭터를 선택하세요: ");
                }

                User.User selectedPlayer = loadedPlayers[selectedCharacterIndex - 1];
                Console.WriteLine($"선택된 캐릭터: {selectedPlayer.Name}");
                selectedPlayer.DisplayStats();

                string filePath = "events.json";
                EventManager.RunMainQuest(selectedPlayer, filePath); // 선택한 캐릭터로 게임 시작
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("저장된 게임이 없습니다. 메인 메뉴로 돌아갑니다.");
                Console.WriteLine("계속하려면 아무 키나 누르세요...");
                Console.ReadKey(); // 사용자가 키를 입력할 때까지 대기
            }
            catch (Exception ex)
            {
                Console.WriteLine($"게임을 불러오는 중 오류가 발생했습니다: {ex.Message}");
                Console.WriteLine("계속하려면 아무 키나 누르세요...");
                Console.ReadKey(); // 사용자가 키를 입력할 때까지 대기
            }
        }

        public static void ExitGame()
        {
            Console.WriteLine("게임을 종료합니다. 감사합니다!");
        }
    }
}

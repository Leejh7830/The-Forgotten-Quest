using TheForgottenQuest.Menu;

namespace TheForgottenQuest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WindowWidth = 100;

            // bool loading = true;
            bool loading = false;
            // Utility.ShowLoading("", 100, ref loading);

            while (true)
            {
                MenuManager.ShowMainMenu();
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        MenuManager.StartNewGame(ref loading);
                        break;
                    case "2":
                        MenuManager.LoadGame(ref loading);
                        break;
                    case "3":
                        MenuManager.ExitGame();
                        return;
                    default:
                        Utility.SlowType("올바른 선택을 하지 않았습니다. 다시 시도해주세요.");
                        break;
                }
            }
        }
    }
}

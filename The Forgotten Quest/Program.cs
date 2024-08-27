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
                ConsoleKeyInfo keyInfo = Console.ReadKey(true); // 키 입력을 바로 받습니다.

                switch (keyInfo.Key)
                {
                    case ConsoleKey.D1: // 숫자 1
                    case ConsoleKey.NumPad1: // 숫자 패드 1
                        MenuManager.StartNewGame(ref loading);
                        break;
                    case ConsoleKey.D2: // 숫자 2
                    case ConsoleKey.NumPad2: // 숫자 패드 2
                        MenuManager.LoadGame(ref loading);
                        break;
                    case ConsoleKey.D3: // 숫자 3
                    case ConsoleKey.NumPad3: // 숫자 패드 3
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

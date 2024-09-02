using The_Forgotten_Quest;
using TheForgottenQuest.User;

namespace TheForgottenQuest.Events
{
    public static class EventProcessor
    {
        public static string RunCombatEvent(Event gameEvent, UserDTO player)
        {
            Console.WriteLine("전투 이벤트 시작!");

            while (true)
            {
                Console.WriteLine("1. 공격");
                Console.WriteLine("2. 스킬");
                Console.WriteLine("3. 도망치기");

                var keyInfo = Console.ReadKey(true);

                switch (keyInfo.Key)
                {
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        // 공격 로직
                        Console.WriteLine("공격을 선택했습니다.");
                        // 공격 결과 처리
                        break;

                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        // 스킬 로직
                        Console.WriteLine("스킬을 선택했습니다.");
                        // 스킬 결과 처리
                        break;

                    case ConsoleKey.D3:
                    case ConsoleKey.NumPad3:
                        // 도망치기 로직
                        Console.WriteLine("도망치기를 선택했습니다.");
                        return "next_event_id_after_escape"; // 도망 성공 시 다음 이벤트로 이동
                }

                // 전투 결과에 따른 다음 이벤트 ID 반환
                //if (/* 전투 종료 조건 */)
                {
                    return "next_event_id_after_combat";
                }
            }
        }


        public static string? ProcessEventSelection(Event gameEvent, UserDTO player)
        {
            Utility.SlowType(gameEvent.Question);
            Utility.SlowType("어느 선택을 할까요? (1, 2 입력 또는 ESC를 눌러 저장): ");

            string? choice = GetUserChoice(player);
            if (choice == null) return null; // ESC를 눌러 저장 시

            Result result = Utility.DisplayRollResult(player, gameEvent, choice);

            // 전투이벤트 진행 시
            if (result.NextEventId?.StartsWith("combat_") == true)
            {
                return RunCombatEvent(gameEvent, player);
            }

            return result.NextEventId ?? "defaultEventId";
        }

        private static string? GetUserChoice(UserDTO player)
        {
            ConsoleKeyInfo keyInfo;
            string? choice = null;

            do
            {
                keyInfo = Console.ReadKey(true);

                if (keyInfo.Key == ConsoleKey.Escape)
                {
                    Utility.SavePlayer(player, JsonConstants.PlayerFilePath);
                    return null; // ESC 누르면 메인메뉴로 이동
                }
                else if (keyInfo.Key == ConsoleKey.D1 || keyInfo.Key == ConsoleKey.NumPad1 ||
                        keyInfo.Key == ConsoleKey.D2 || keyInfo.Key == ConsoleKey.NumPad2)
                {
                    choice = keyInfo.KeyChar.ToString();
                }
                else
                {
                    Utility.SlowType("올바른 선택을 하지 않았습니다. 1, 2 또는 ESC 키를 누르세요.");
                }
            } while (choice == null);

            return choice;
        }
    }
}

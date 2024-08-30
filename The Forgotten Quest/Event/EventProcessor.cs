using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheForgottenQuest.Events;
using TheForgottenQuest.User;
using TheForgottenQuest;
using System.Numerics;

namespace The_Forgotten_Quest.Event
{
    public static class EventProcessor
    {
        public static string RunCombatEvent(Event gameEvent, UserDTO player)
        {
            // 전투 이벤트 처리 로직 추가
            Console.WriteLine("전투 이벤트 시작!");
            // 예: 플레이어의 행동 선택 (공격, 방어, 스킬 등)
            // 적의 행동 처리
            // 결과 계산 및 반환

            return "next_event_id"; // 전투 결과에 따른 다음 이벤트 ID 반환
        }

        public static string? ProcessEventSelection(Event gameEvent, UserDTO player)
        {
            Utility.SlowType(gameEvent.Question);
            Utility.SlowType("어느 선택을 할까요? (1, 2 입력 또는 ESC를 눌러 저장): ");

            string? choice = GetUserChoice();
            if (choice == null) return null; // ESC를 눌러 저장 시

            Result result = Utility.DisplayRollResult(player, gameEvent, choice);

            // 전투이벤트 진행 시
            if (result.NextEventId?.StartsWith("combat_") == true)
            {
                return RunCombatEvent(gameEvent, player);
            }

            return result.NextEventId ?? "defaultEventId";
        }

        private static string? GetUserChoice()
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

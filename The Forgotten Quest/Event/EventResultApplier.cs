using System;
using TheForgottenQuest.User;

namespace TheForgottenQuest.Events
{
    public static class EventResultApplier
    {
        public static void ApplyResult(User.User player, Result result)
        {
            if (result.LevelChange != 0)
            {
                player.Level += result.LevelChange;
                Console.WriteLine($"레벨이 {result.LevelChange}만큼 변경되었습니다. 현재 레벨: {player.Level}");
            }

            if (result.EXPChange != 0)
            {
                player.EXP += result.EXPChange;
                Console.WriteLine($"경험치가 {result.EXPChange}만큼 변경되었습니다. 현재 경험치: {player.EXP}");
            }

            if (result.HPChange != 0)
            {
                player.HP += result.HPChange;
                Console.WriteLine($"HP가 {result.HPChange}만큼 변경되었습니다. 현재 HP: {player.HP}");
            }

            if (result.MPChange != 0)
            {
                player.MP += result.MPChange;
                Console.WriteLine($"MP가 {result.MPChange}만큼 변경되었습니다. 현재 MP: {player.MP}");
            }

            if (result.LUKChange != 0)
            {
                player.LUK += result.LUKChange;
                Console.WriteLine($"운이 {result.LUKChange}만큼 변경되었습니다. 현재 운: {player.LUK}");
            }
        }
    }
}

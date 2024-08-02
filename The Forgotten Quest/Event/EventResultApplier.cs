using System;
using TheForgottenQuest.User;

namespace TheForgottenQuest.Events
{
    public static class EventResultApplier
    {
        public static void ApplyResult(UserDTO player, Result result)
        {
            if (result.LevelChange != 0)
            {
                player.Level += result.LevelChange;
                Utility.SlowType($"레벨이 {result.LevelChange}만큼 변경되었습니다. 현재 레벨: {player.Level}\n");
            }

            if (result.EXPChange != 0)
            {
                player.EXP += result.EXPChange;
                Utility.SlowType($"경험치가 {result.EXPChange}만큼 변경되었습니다. 현재 경험치: {player.EXP}\n");
            }

            if (result.HPChange != 0)
            {
                player.HP += result.HPChange;
                Utility.SlowType($"HP가 {result.HPChange}만큼 변경되었습니다. 현재 HP: {player.HP}\n");
            }

            if (result.MPChange != 0)
            {
                player.MP += result.MPChange;
                Utility.SlowType($"MP가 {result.MPChange}만큼 변경되었습니다. 현재 MP: {player.MP}\n");
            }

            if (result.LUKChange != 0)
            {
                player.LUK += result.LUKChange;
                Utility.SlowType($"운이 {result.LUKChange}만큼 변경되었습니다. 현재 운: {player.LUK}\n");
            }
        }
    }
}

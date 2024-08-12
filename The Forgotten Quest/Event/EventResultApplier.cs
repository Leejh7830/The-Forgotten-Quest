using TheForgottenQuest.User;

namespace TheForgottenQuest.Events
{
    public static class EventResultApplier
    {
        public static void ApplyResult(UserDTO player, Result result)
        {
            // 경험치 변화 적용
            if (result.EXPChange != 0)
            {
                player.AddExperience(result.EXPChange);
            }

            // HP, MP, LUK 변화 적용
            if (result.HPChange != 0)
            {
                player.ChangeHP(result.HPChange);
            }

            if (result.MPChange != 0)
            {
                player.ChangeMP(result.MPChange);
            }

            if (result.LUKChange != 0)
            {
                player.ChangeLUK(result.LUKChange);
            }

            // 레벨 변화에 따른 경험치 적용 및 레벨업
            if (result.LevelChange != 0)
            {
                for (int i = 0; i < result.LevelChange; i++)
                {
                    player.AddExperience(100); // 레벨업 시 경험치 100을 추가하여 레벨업 처리
                }
            }
        }

    }
}

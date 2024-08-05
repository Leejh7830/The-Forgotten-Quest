using TheForgottenQuest.User;

namespace TheForgottenQuest.Events
{
    public static class EventResultApplier
    {
        public static void ApplyResult(UserDTO player, Result result)
        {
            if (result.LevelChange != 0)
            {
                for (int i = 0; i < result.LevelChange; i++)
                {
                    player.AddExperience(100);
                }
            }

            if (result.EXPChange != 0)
            {
                player.AddExperience(result.EXPChange);
            }

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
        }
    }
}

﻿using TheForgottenQuest.User;

namespace TheForgottenQuest.Events
{
    public static class EventConditionChecker
    {
        public static bool CheckCondition(Event gameEvent, UserDTO player, out string failedCondition)
        {
            failedCondition = string.Empty;

            if (gameEvent.Condition != null)
            {
                var condition = GetCondition(gameEvent.Condition);
                if (!condition(player))
                {
                    failedCondition = gameEvent.Condition;
                    return false;
                }
            }

            return true;
        }

        private static Func<UserDTO, bool> GetCondition(string condition)
        {
            switch (condition)
            {
                case "LevelAbove3":
                    return player => player.Level >= 3;
                default:
                    return player => true;
            }
        }
    }
}

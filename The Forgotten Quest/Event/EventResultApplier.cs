using TheForgottenQuest.User;

namespace TheForgottenQuest.Events
{
    public static class EventResultApplier
    {
        public static void ApplyResult(UserDTO player, Result result)
        {
            // HP, MP, LUK 변화 적용
            if (result.HPChange != 0)
            {
                player.ChangeHP(result.HPChange);
            }

            if (result.MaxHPChange != 0)
            {
                player.ChangeMaxHP(result.MaxHPChange);
            }

            if (result.MPChange != 0)
            {
                player.ChangeMP(result.MPChange);
            }

            if(result.MaxMPChange != 0)
            {
                player.ChangeMaxMP(result.MaxMPChange);
            }

            if (result.LUKChange != 0)
            {
                player.ChangeLUK(result.LUKChange);
            }

            // 경험치 변화 적용 및 레벨업
            if (result.EXPChange != 0)
            {
                player.AddExperience(result.EXPChange);
            }

            // 경험치100%가 아닌 레벨을 바로 변화할 때
            if (result.LevelChange != 0)
            {
                for (int i = 0; i < result.LevelChange; i++)
                {
                    player.AddExperience(100); // 레벨업 시 경험치 100을 추가하여 레벨업 처리
                }
            }

            // 버프 적용 (항목 추가 시 Buff_Debuff.cs에 내용 추가해야함)
            if (!string.IsNullOrEmpty(result.Buff))
            {
                if (result.Debuff == "행운적용")
                {
                    player.BuffDebuff.ApplyBuff("행운");
                }
                else if (result.Debuff == "행운해제")
                {
                    player.BuffDebuff.RemoveBuff("행운");
                }
            }

            // 디버프 적용 (항목 추가 시 Buff_Debuff.cs에 내용 추가해야함)
            if (!string.IsNullOrEmpty(result.Debuff))
            {
                if (result.Debuff == "저주적용")
                {
                    player.BuffDebuff.ApplyDebuff("저주");
                }
                else if (result.Debuff == "저주해제")
                {
                    player.BuffDebuff.RemoveDebuff("저주");
                }
            }
        }
    }
}

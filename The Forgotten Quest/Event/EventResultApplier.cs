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
            if (result.Buffs != null && result.Buffs.Any())
            {
                foreach (var debuff in result.Buffs)
                {
                    var debuffName = debuff.Replace("적용", "").Replace("해제", "");
                    if (debuff.Contains("적용"))
                    {
                        player.BuffDebuff.ApplyDebuff(debuffName);
                    }
                    else if (debuff.Contains("해제"))
                    {
                        player.BuffDebuff.RemoveDebuff(debuffName);
                    }
                }
            }
            /*if (!string.IsNullOrEmpty(result.Buff))
            {
                var debuffName = result.Buff.Replace("적용", "").Replace("해제", "");
                if (result.Buff.Contains("적용"))
                {
                    player.BuffDebuff.ApplyDebuff(debuffName);
                }
                else if (result.Buff.Contains("해제"))
                {
                    player.BuffDebuff.RemoveDebuff(debuffName);
                }
            }*/

            // 디버프 적용 (항목 추가 시 Buff_Debuff.cs에 내용 추가해야함)
            if (result.Debuffs != null && result.Debuffs.Any())
            {
                foreach (var debuff in result.Debuffs)
                {
                    var debuffName = debuff.Replace("적용", "").Replace("해제", "");
                    if (debuff.Contains("적용"))
                    {
                        player.BuffDebuff.ApplyDebuff(debuffName);
                    }
                    else if (debuff.Contains("해제"))
                    {
                        player.BuffDebuff.RemoveDebuff(debuffName);
                    }
                }
            }
            /*if (!string.IsNullOrEmpty(result.Debuff))
            {
                var debuffName = result.Debuff.Replace("적용", "").Replace("해제", "");
                if (result.Debuff.Contains("적용"))
                {
                    player.BuffDebuff.ApplyDebuff(debuffName);
                }
                else if (result.Debuff.Contains("해제"))
                {
                    player.BuffDebuff.RemoveDebuff(debuffName);
                }
            }*/
        }
    }
}

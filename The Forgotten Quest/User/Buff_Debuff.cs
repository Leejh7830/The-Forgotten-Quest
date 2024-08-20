using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheForgottenQuest.User;

namespace The_Forgotten_Quest.User
{
    public class Buff_Debuff
    {
        private UserDTO player;

        // 생성자를 통해 UserDTO 객체를 전달받음
        public Buff_Debuff(UserDTO user)
        {
            player = user;
        }

        // 버프 및 디버프를 관리할 플래그
        public Dictionary<string, bool> StatusFlags { get; private set; } = new Dictionary<string, bool>();


        // 능력치에 따라 상태 플래그에 따른 수정
        public int ModifiedHP
        {
            get
            {
                int modifiedHp = player.HP;

                if (StatusFlags.ContainsKey("상처") && StatusFlags["상처"])
                {
                    modifiedHp -= 10; // 상처 디버프가 있을 경우 HP 감소
                }

                if (StatusFlags.ContainsKey("중독") && StatusFlags["중독"])
                {
                    modifiedHp -= 15; // 중독 디버프가 있을 경우 HP 추가 감소
                }

                if (StatusFlags.ContainsKey("행운") && StatusFlags["행운"])
                {
                    modifiedHp += 20; // 행운 버프가 있을 경우 HP 증가
                }

                return modifiedHp;
            }
        }

        public int ModifiedMaxHP
        {
            get
            {
                int modifiedMaxHp = player.MaxHP;
                if (StatusFlags.ContainsKey("저주") && StatusFlags["저주"])
                {
                    modifiedMaxHp -= 20;
                }

                return modifiedMaxHp;
            }
        }

        public int ModifiedMP
        {
            get
            {
                int modifiedMp = player.MP;

                if (StatusFlags.ContainsKey("명상") && StatusFlags["명상"])
                {
                    modifiedMp += 10;
                }

                return modifiedMp;
            }
        }

        public int ModifiedMaxMP
        {
            get
            {
                int ModifiedMaxMP = player.MaxMP;

                if (StatusFlags.ContainsKey("깊은명상") && StatusFlags["깊은명상"])
                {
                    ModifiedMaxMP += 10;
                }

                return ModifiedMaxMP;
            }
        }

        public int ModifiedLUK
        {
            get
            {
                int modifiedLuk = player.LUK;

                if (StatusFlags.ContainsKey("저주") && StatusFlags["저주"])
                {
                    modifiedLuk -= 5; // 저주 디버프가 있을 경우 운(LUK) 감소
                }

                if (StatusFlags.ContainsKey("행운") && StatusFlags["행운"])
                {
                    modifiedLuk += 5; // 행운 버프가 있을 경우 운(LUK) 증가
                }

                return modifiedLuk;
            }
        }



        // 버프 적용 메서드
        public void ApplyBuff(string buffName)
        {
            Console.WriteLine($"{buffName} 버프가 적용되었습니다.");

            if (StatusFlags.ContainsKey(buffName))
            {
                StatusFlags[buffName] = true; // 이미 있는 경우, 상태를 true로 설정
            }
            else
            {
                StatusFlags.Add(buffName, true); // 없는 경우, 새로 추가하면서 true로 설정
            }

        }

        // 버프 해제 메서드
        public void RemoveBuff(string buffName)
        {
            Console.WriteLine($"{buffName} 버프가 해제되었습니다.");

            if (StatusFlags.ContainsKey(buffName) && StatusFlags[buffName])
            {
                StatusFlags[buffName] = false; // 상태를 false로 설정하여 해제
            }
        }

        // 디버프 적용 메서드
        public void ApplyDebuff(string debuffName)
        {
            if (!StatusFlags.ContainsKey(debuffName) || !StatusFlags[debuffName])
            {
                Console.WriteLine($"{debuffName} 디버프가 적용되었습니다.");
                ////////////////////////////////아래에 디버프 추가////////////////////////////////////////

                /*if (debuffName == "저주")
                {
                    player.ChangeMaxHP(-20); // MaxHP를 즉시 -20 감소시킴
                }*/

                StatusFlags[debuffName] = true;
            }
            else
            {
                Console.WriteLine($"{debuffName} 디버프가 이미 적용되어 있습니다.");
            }
        }

        // 디버프 해제 메서드
        public void RemoveDebuff(string debuffName)
        {
            if (StatusFlags.ContainsKey(debuffName) && StatusFlags[debuffName])
            {
                Console.WriteLine($"{debuffName} 디버프가 해제되었습니다.");

                ////////////////////////////////아래에 디버프 추가////////////////////////////////////////
                /*if (debuffName == "저주")
                {
                    player.ChangeMaxHP(20); // MaxHP를 다시 20 증가시킴
                }*/

                StatusFlags[debuffName] = false;
            }
            else
            {
                Console.WriteLine($"{debuffName} 디버프가 적용되어 있지 않습니다.");
            }
        }

    }
}

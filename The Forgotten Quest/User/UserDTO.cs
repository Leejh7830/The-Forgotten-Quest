using Newtonsoft.Json;
using The_Forgotten_Quest.User;

namespace TheForgottenQuest.User
{
    public class UserDTO
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Job { get; private set; }
        public int Level { get; private set; }
        public int EXP { get; private set; }
        public int HP { get; private set; }
        public int MaxHP { get; private set; }
        public int MP { get; private set; }
        public int MaxMP { get; private set; }
        public int LUK { get; private set; }
        public bool IsAlive { get; private set; } = true; // 생존 여부, 현재는 불필요함
        public Buff_Debuff BuffDebuff { get; private set; }
        public string CurrentMainQuestEventId { get; set; } = "1"; // 기본값은 "1"로 설정


        // 새로 추가된 스탯들
        public int STR { get; private set; } // 힘
        public int DEX { get; private set; }  // 민첩
        public int INT { get; private set; }  // 지능
        public double AttackPower { get; private set; } // 최종 공격력 (STR/DEX/INT)


        // 새로운 플레이어 생성자 (고유ID 생성)
        public UserDTO(string name, string job)
        {
            Id = Guid.NewGuid(); // 새로운 ID 생성
            Name = name;
            Level = 1;
            EXP = 0;
            Job = job;
            SetStats(job);
            BuffDebuff = new Buff_Debuff(this);
            CurrentMainQuestEventId = "1";
        }


        [JsonConstructor]
        public UserDTO(Guid id, string name, string job, int level, int exp, int hp, int maxHp, int mp, int maxMp, int luk, bool isAlive, int str, int dex, int @int, string currentMainQuestEventId)
        {
            Id = id;
            Name = name;
            Job = job;
            Level = level;
            EXP = exp;
            HP = hp;
            MaxHP = maxHp;
            MP = mp;
            MaxMP = maxMp;
            LUK = luk;
            IsAlive = isAlive;
            STR = str;
            DEX = dex;
            INT = @int;
            BuffDebuff = new Buff_Debuff(this);
            CurrentMainQuestEventId = currentMainQuestEventId ?? "1"; // null인 경우 1할당
        }

        private void SetStats(string job)
        {
            switch (job.ToLower())
            {
                case "전사":
                    HP = 150;
                    MP = 10;
                    LUK = 0;
                    STR = 7;
                    DEX = 2;
                    INT = 1;
                    break;

                case "마법사":
                    HP = 100;
                    MP = 50;
                    LUK = 2;
                    STR = 2;
                    DEX = 2;
                    INT = 6;
                    break;

                case "도적":
                    HP = 120;
                    MP = 20;
                    LUK = 5;
                    STR = 3;
                    DEX = 5;
                    INT = 2;
                    break;

                default:
                    HP = 100;
                    MP = 0;
                    LUK = 0;
                    STR = 3;
                    DEX = 3;
                    INT = 3;
                    break;
            }
            MaxHP = HP;
            MaxMP = MP;
        }

        public void AddExperience(int exp)
        {
            EXP += exp;
            Utility.SlowType($"경험치를 {exp} 얻었습니다. 현재 경험치: {EXP}");
            while (EXP >= 100)
            {
                EXP -= 100;
                LevelUp();
            }
        }

        private void LevelUp()
        {
            Level++;
            Utility.SlowType($"{Name}의 레벨이 {Level}로 증가했습니다!");
            ChangeMaxHP(10);
            Utility.SlowType("MaxHP가 10 증가했습니다.");
            ChangeMaxMP(5);
            Utility.SlowType("MaxMP가 5 증가했습니다.");
            STR += 1;
            DEX += 1;
            INT += 1;
            Utility.SlowType("모든 스탯이 1씩 증가했습니다.");
            RestoreFullHP();
            RestoreFullMP();
            Utility.SlowType("HP / MP 회복되었습니다.");
        }

        private void CheckDeath()
        {
            if (HP <= 0)
            {
                IsAlive = false;
                Utility.SlowType($"{Name}이(가) 사망하였습니다. GAME OVER...");
                
                Utility.GameOver();
            }
        }
        

        public void ChangeHP(int amount)   
        {
            HP = Math.Clamp(BuffDebuff.ModHP + amount, 0, BuffDebuff.ModMaxHP); // ModifiedHP 사용
            Utility.SlowType($"HP가 {amount}만큼 변경되었습니다. 현재 HP: {BuffDebuff.ModHP}/{BuffDebuff.ModMaxHP}");
            CheckDeath();
        }

        public void ChangeMaxHP(int amount)
        {
            MaxHP = Math.Max(MaxHP + amount, 0); // MaxHP를 변경
            HP = Math.Min(HP, BuffDebuff.ModMaxHP); // HP가 변경된 MaxHP를 초과하지 않도록 조정
            Utility.SlowType($"MaxHP가 {amount}만큼 변경되었습니다. 현재 HP: {BuffDebuff.ModHP}/{BuffDebuff.ModMaxHP}");
            CheckDeath();
        }

        public void ChangeMP(int amount)
        {
            MP = Math.Clamp(BuffDebuff.ModMP + amount, 0, BuffDebuff.ModMaxMP); // ModifiedMP 사용
            Utility.SlowType($"MP가 {amount}만큼 변경되었습니다. 현재 MP: {BuffDebuff.ModMP}/{BuffDebuff.ModMaxMP}");
        }

        public void ChangeMaxMP(int amount)
        {
            MaxMP = Math.Max(MaxMP + amount, 0); // MaxMP를 변경
            MP = Math.Min(MP, BuffDebuff.ModMaxMP); // MP가 변경된 MaxMP를 초과하지 않도록 조정
            Utility.SlowType($"MaxMP가 {amount}만큼 변경되었습니다. 현재 MP: {BuffDebuff.ModMP}/{BuffDebuff.ModMaxMP}");
        }

        public void ChangeLUK(int amount)
        {
            LUK = BuffDebuff.ModLUK + amount; // ModifiedLUK 사용
            Utility.SlowType($"운이 {amount}만큼 변경되었습니다. 현재 운: {BuffDebuff.ModLUK}");
        }

        private void RestoreFullHP()
        {
            HP = BuffDebuff.ModMaxHP; // HP를 최대 HP로 회복
        }

        private void RestoreFullMP()
        {
            MP = BuffDebuff.ModMaxMP; // MP를 최대 MP로 회복
        }

        private void CalculateAttackPower()
        {
            double strWeight = 1.0;
            double dexWeight = 1.0;
            double intWeight = 1.0;

            switch (Job.ToLower())
            {
                case "전사":
                    strWeight = 1.2;
                    dexWeight = 1.0;
                    intWeight = 0.8;
                    break;

                case "마법사":
                    strWeight = 0.8;
                    dexWeight = 1.0;
                    intWeight = 1.2;
                    break;

                case "도적":
                    strWeight = 1.0;
                    dexWeight = 1.2;
                    intWeight = 1.0;
                    break;
            }

            AttackPower = (STR * strWeight) + (DEX * dexWeight) + (INT * intWeight);
        }
    }
}

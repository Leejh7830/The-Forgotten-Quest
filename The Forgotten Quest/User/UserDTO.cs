using Newtonsoft.Json;

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


        // 새로운 플레이어 생성자 (고유ID 생성)
        public UserDTO(string name, string job)
        {
            Id = Guid.NewGuid(); // 새로운 ID 생성
            Name = name;
            Level = 1;
            EXP = 0;
            Job = job;
            SetStats(job);
        }

        // JSON 역직렬화를 위한 생성자
        [JsonConstructor]
        public UserDTO(Guid id, string name, string job, int level, int exp, int hp, int maxHp, int mp, int maxMp, int luk, bool isAlive)
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
        }

        // 기존 플레이어 생성자
        public UserDTO(UserDTO existingUser)
        {
            Id = existingUser.Id;
            Name = existingUser.Name;
            Level = existingUser.Level;
            EXP = existingUser.EXP;
            Job = existingUser.Job;
            HP = existingUser.HP;
            MP = existingUser.MP;
            LUK = existingUser.LUK;
        }  
        
        private void SetStats(string job)
        {
            switch (job.ToLower())
            {
                case "전사":
                    HP = 150;
                    MP = 10;
                    LUK = 0;
                    break;

                case "마법사":
                    HP = 100;
                    MP = 50;
                    LUK = 2;
                    break;

                case "도적":
                    HP = 120;
                    MP = 20;
                    LUK = 5;
                    break;

                default:
                    HP = 100;
                    MP = 0;
                    LUK = 0;
                    break;
            }

            // MaxHP와 MaxMP도 초기화
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
            MaxHP += 10;
            Utility.SlowType("MaxHP가 10 증가했습니다.");
            MaxMP += 5;
            Utility.SlowType("MaxMP가 5 증가했습니다.");
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
            HP = Math.Clamp(HP + amount, 0, MaxHP);
            Utility.SlowType($"HP가 {amount}만큼 변경되었습니다. 현재 HP: {HP}/{MaxHP}");
            CheckDeath();
        }

        public void ChangeMP(int amount)
        {
            MP = Math.Clamp(MP + amount, 0, MaxMP);
            Utility.SlowType($"MP가 {amount}만큼 변경되었습니다. 현재 MP: {MP}/{MaxMP}");
        }

        public void ChangeLUK(int amount)
        {
            LUK += amount;
            Utility.SlowType($"운이 {amount}만큼 변경되었습니다. 현재 운: {LUK}");
        }

        private void RestoreFullHP()
        {
            HP = MaxHP;
        }

        private void RestoreFullMP()
        {
            MP = MaxMP;
        }

        public void DisplayStats()
        {
            Console.Clear();
            Console.WriteLine("=======================================================");
            Console.WriteLine($"    {"ID:",-5} {Id}");
            Console.WriteLine($"    {"Name:",-5} {Name}");
            Console.WriteLine($"    {"Level:",-5} {Level}");
            Console.WriteLine($"    {"EXP:",-5} {EXP}");
            Console.WriteLine($"    {"Job:",-5} {Job}");
            Console.WriteLine($"    {"HP:",-5} {HP}");
            Console.WriteLine($"    {"MP:",-5} {MP}");
            Console.WriteLine($"    {"LUK:",-5} {LUK}");
            Console.WriteLine("=======================================================");
        }

    }
}

namespace TheForgottenQuest.User
{
    public class UserDTO
    {
        public Guid Id { get; set; } // 고유 ID
        public string Name { get; set; }
        public int Level { get; set; }
        public int EXP { get; set; }
        public string Job { get; set; }
        public int HP { get; set; }
        public int MP { get; set; }
        public int LUK { get; set; }

        public UserDTO(string name, string job)
        {
            Id = Guid.NewGuid(); // 새로운 고유 ID 생성
            Name = name;
            Level = 1;
            EXP = 0;
            Job = job;
            SetStats(job);
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

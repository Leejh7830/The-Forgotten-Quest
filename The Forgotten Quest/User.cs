using System;

namespace TheForgottenQuest
{
    public class User
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public string Job { get; set; }
        public int HP { get; set; }
        public int MP { get; set; }
        public int LUK { get; set; }

        public User(string name, string job)
        {
            Name = name;
            Level = 1;
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
            Console.WriteLine("=====================================");
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"Level: {Level}");
            Console.WriteLine($"Job: {Job}");
            Console.WriteLine($"HP: {HP}");
            Console.WriteLine($"MP: {MP}");
            Console.WriteLine($"LUK: {LUK}");
            Console.WriteLine("=====================================\n");
        }
    }
}

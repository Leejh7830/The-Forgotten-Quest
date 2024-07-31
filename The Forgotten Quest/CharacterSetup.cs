using System;

namespace TheForgottenQuest
{
    public static class CharacterSetup
    {
        public static string GetName()
        {
            string playerName = "";
            while (string.IsNullOrWhiteSpace(playerName))
            {
                Console.WriteLine("당신의 이름을 입력하세요:");
                Console.WriteLine("- - - - - - - - - - - - - - -");
                playerName = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(playerName))
                {
                    Console.WriteLine("올바른 이름을 입력하세요. 이름은 비워둘 수 없습니다.");
                }
            }
            return playerName;
        }

        public static string GetJob()
        {
            Console.WriteLine("\n당신의 직업을 선택하세요:");
            Console.WriteLine("1. 전사");
            Console.WriteLine("2. 마법사");
            Console.WriteLine("3. 도적");
            Console.WriteLine("4. 선택안함");
            Console.WriteLine("- - - - - - - - - - - - - - -");

            string job = "";
            int jobChoice = 0;
            while (!int.TryParse(Console.ReadLine(), out jobChoice) || jobChoice < 1 || jobChoice > 4)
            {
                Console.WriteLine("올바른 번호를 선택하세요. (1, 2, 3, 4)");
            }

            switch (jobChoice)
            {
                case 1:
                    job = "전사";
                    break;
                case 2:
                    job = "마법사";
                    break;
                case 3:
                    job = "도적";
                    break;
                case 4:
                    Console.WriteLine("직업을 선택하지 않았습니다. 기본 직업으로 설정됩니다.");
                    job = "기본";
                    break;
            }

            return job;
        }

        public static bool ConfirmSelection(string name, string job)
        {
            Console.WriteLine($"\n당신의 이름은 {name}이고, 직업은 {job}입니다. 맞습니까?");
            Console.WriteLine("1. 예");
            Console.WriteLine("2. 아니오");
            int confirmation = 0;
            while (!int.TryParse(Console.ReadLine(), out confirmation) || (confirmation != 1 && confirmation != 2))
            {
                Console.WriteLine("올바른 번호를 선택하세요. (1 또는 2)");
            }
            if (confirmation == 2)
            {
                Console.Clear();
            }
            
            return confirmation == 1;
        }
    }
}

namespace TheForgottenQuest.User
{
    public static class UserSetup
    {
        public static string GetName()
        {
            string playerName = "";
            while (string.IsNullOrWhiteSpace(playerName))
            {
                Utility.SlowType("당신의 이름을 입력하세요:");
                Utility.SlowType("- - - - - - - - - - - - - - -");
                playerName = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(playerName))
                {
                    Utility.SlowType("올바른 이름을 입력하세요. 이름은 비워둘 수 없습니다.");
                }
            }
            return playerName;
        }

        public static string GetJob()
        {
            Utility.SlowType("\n당신의 직업을 선택하세요:");
            Utility.SlowType("1. 전사");
            Utility.SlowType("2. 마법사");
            Utility.SlowType("3. 도적");
            Utility.SlowType("- - - - - - - - - - - - - - -");

            string job = "";
            int jobChoice = 0;
            while (!int.TryParse(Console.ReadLine(), out jobChoice) || jobChoice < 1 || jobChoice > 3)
            {
                Utility.SlowType("올바른 번호를 선택하세요. (1, 2, 3)");
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
            }
            return job;
        }

        public static bool ConfirmSelection(string name, string job)
        {
            Utility.SlowType($"\n당신의 이름은 {name}이고, 직업은 {job}입니다. 맞습니까?");
            Utility.SlowType("1. 예");
            Utility.SlowType("2. 아니오");
            int confirmation = 0;
            while (!int.TryParse(Console.ReadLine(), out confirmation) || confirmation != 1 && confirmation != 2)
            {
                Utility.SlowType("올바른 번호를 선택하세요. (1 또는 2)");
            }
            if (confirmation == 2)
            {
                Console.Clear();
            }

            return confirmation == 1;
        }
    }
}

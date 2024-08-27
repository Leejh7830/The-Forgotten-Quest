using Newtonsoft.Json;
using System;

namespace TheForgottenQuest.User
{
    public static class UserSaver
    {
        // User Save기능은
        // 현재 새로캐릭터를 만들 때 / 게임 중 세이브포인트가 나올 때
        public static void SaveUsers(List<UserDTO> users, string filePath)
        {
            // JSON문자열로 변환, Formatting.Indented : 들여쓰기
            string json = JsonConvert.SerializeObject(users, Formatting.Indented);
            // 지정된 filePath에 저장, 기존 같은 이름파일 존재하면 덮어쓰기
            File.WriteAllText(filePath, json);

            Utility.SlowType("캐릭터 정보가 저장되었습니다.");
            Console.WriteLine("다음으로 이동...");
            Console.ReadLine();
        }
    }
}


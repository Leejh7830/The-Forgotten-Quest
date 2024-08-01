using Newtonsoft.Json;

namespace TheForgottenQuest.User
{
    public static class UserSaver
    {
        public static void SaveUsers(List<User> users, string filePath)
        {
            // JSON문자열로 변환, Formatting.Indented : 들여쓰기
            string json = JsonConvert.SerializeObject(users, Formatting.Indented);
            // 지정된 filePath에 저장, 기존 같은 이름파일 존재하면 덮어쓰기
            File.WriteAllText(filePath, json);
        }
    }
}


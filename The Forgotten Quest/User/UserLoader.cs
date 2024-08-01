using Newtonsoft.Json;

namespace TheForgottenQuest.User
{
    public static class UserLoader
    {
        public static List<User> LoadUsers(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("Save file not found", filePath);
            }

            string json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<List<User>>(json);
        }
    }
}

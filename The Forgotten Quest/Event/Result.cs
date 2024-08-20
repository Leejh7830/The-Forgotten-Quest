namespace TheForgottenQuest.Events
{
    public class Result
    {
        public string Message { get; set; } = string.Empty;
        public int LevelChange { get; set; } = 0;
        public int EXPChange { get; set; } = 0;
        public int HPChange { get; set; } = 0;
        public int MPChange { get; set; } = 0;
        public int LUKChange { get; set; } = 0;
        public bool SaveGame { get; set; } = false;
        public string? NextEventId { get; set; } // string? 는 string값 또는 null 값 가능
    }
}

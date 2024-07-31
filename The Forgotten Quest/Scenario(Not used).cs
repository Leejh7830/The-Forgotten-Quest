using System.Collections.Generic;

namespace TheForgottenQuest.Models
{
    public class Scenario
    {
        public string Question { get; set; } = string.Empty;
        public Dictionary<string, string> Results { get; set; } = new Dictionary<string, string>();
    }

    public class ScenarioSet
    {
        public List<Scenario> Level1To20 { get; set; } = new List<Scenario>();
        public List<Scenario> Level21To40 { get; set; } = new List<Scenario>();
    }
}

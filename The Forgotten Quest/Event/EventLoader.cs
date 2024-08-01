using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace TheForgottenQuest.Events
{
    public static class EventLoader
    {
        public static EventSet LoadEvents(string filePath)
        {
            var json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<EventSet>(json) ?? new EventSet();
        }
    }
}

using Starvaders_Seed_Analyzer.Content;
using System.Text.Json;

namespace Starvaders_Seed_Analyzer
{
    public static class JSONParser
    {
        public static ContentManager GetContent(string jsonString)
        {
            using (JsonDocument document = JsonDocument.Parse(jsonString))
            {
                JsonElement root = document.RootElement;
            }
            return new ContentManager();
        }
    }
}

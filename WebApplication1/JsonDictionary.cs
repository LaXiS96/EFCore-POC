using System.Text.Json;

namespace WebApplication1
{
    public class JsonDictionary : Dictionary<string, object?>
    {
        public JsonDictionary()
            : base(StringComparer.OrdinalIgnoreCase) { }

        public JsonDictionary(IDictionary<string, object?> dictionary)
            : base(dictionary, StringComparer.OrdinalIgnoreCase) { }

        public static JsonDictionary FromJson(string json)
        {
            var dict = JsonSerializer.Deserialize<JsonDictionary>(json)
                ?? throw new Exception($"Could not deserialize {nameof(JsonDictionary)}");
            return dict;
        }
    }
}

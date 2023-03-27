namespace WebApplication1
{
    public class JsonDictionary : Dictionary<string, object?>
    {
        public JsonDictionary()
            : base(StringComparer.OrdinalIgnoreCase) { }

        public JsonDictionary(IDictionary<string, object?> dictionary)
            : base(dictionary, StringComparer.OrdinalIgnoreCase) { }
    }
}

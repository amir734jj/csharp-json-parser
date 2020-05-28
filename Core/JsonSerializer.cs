using System.Collections;
using System.Linq;
using Core.Interfaces;
using Core.Tokens;
using Core.Tokens.Item;
using Core.Tokens.Typed;

namespace Core
{
    public class JsonSerializer : IJsonSerializer
    {
        public static IJsonSerializer New()
        {
            return new JsonSerializer();
        }

        private JsonSerializer()
        {
        }

        private static JToken ToToken<T>(T source)
        {
            switch (source)
            {
                case null:
                    return new NullToken();
                case bool value:
                    return new BooleanToken(value);
                case double value:
                    return new DoubleToken(value);
                case string value:
                    return new StringToken(value);
                case IList list:
                    var tokens = (from object item in list select ToToken(item)).ToList();
                    return new JArray(tokens);
                case { } value:
                    return new JObject(value.GetType().GetProperties()
                        .Select(x => new JProperty(x.Name, ToToken(x.GetValue(value)))));
            }
        }

        public string ToJson<T>(T source)
        {
            return ToToken(source).ToString();
        }
    }
}
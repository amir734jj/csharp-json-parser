using System.Collections.Generic;

namespace Core.Tokens.Item
{
    public class JObject : JToken<IEnumerable<JProperty>>
    {
        public JObject(IEnumerable<JProperty> properties)
        {
            Value = properties;
        }

        public override string ToString()
        {
            return $"{{ {string.Join(", ", Value)} }}";
        }
    }
}
using System.Collections.Generic;

namespace Core.Tokens.Item
{
    public class JArray : JToken<IEnumerable<JToken>>
    {
        public JArray(IEnumerable<JToken> tokens)
        {
            Value = tokens;
        }

        public override string ToString()
        {
            return $"[{string.Join(", ", Value)}]";
        }
    }
}
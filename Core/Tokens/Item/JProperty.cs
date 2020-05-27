using System.Collections.Generic;

namespace Core.Tokens.Item
{
    public class JProperty : JToken<KeyValuePair<string, JToken>>
    {
        public JProperty(string name, JToken value)
        {
            Value = new KeyValuePair<string, JToken>(name, value);
        }

        public override string ToString()
        {
            return @$"""{Value.Key}"": {Value.Value}";
        }
    }
}
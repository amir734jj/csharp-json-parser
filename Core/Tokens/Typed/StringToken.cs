namespace Core.Tokens.Typed
{
    public class StringToken : JToken<string>
    {
        public StringToken(string value)
        {
            Value = value;
        }

        public override string ToString()
        {
            return $@"""{Value}""";
        }
    }
}
namespace Core.Tokens.Typed
{
    public class BooleanToken : JToken<bool>
    {
        public BooleanToken(bool value)
        {
            Value = value;
        }

        public override string ToString()
        {
            return Value.ToString().ToLower();
        }
    }
}
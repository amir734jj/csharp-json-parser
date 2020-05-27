namespace Core.Tokens.Typed
{
    public class DoubleToken : JToken<double>
    {
        public DoubleToken(double value)
        {
            Value = value;
        }
    }
}
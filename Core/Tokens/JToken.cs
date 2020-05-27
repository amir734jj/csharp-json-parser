using Core.Tokens.Typed;

namespace Core.Tokens
{
    public class JToken<T> : JToken
    {
        public T Value { get; protected set; }
        
        public override string ToString()
        {
            return Value.ToString();
        }
    }
    
    public class JToken
    {
        public static implicit operator JToken(string value) => new StringToken(value);
        
        public static implicit operator JToken(bool value) => new BooleanToken(value);
        
        public static implicit operator JToken(double value) => new DoubleToken(value);
    }
}
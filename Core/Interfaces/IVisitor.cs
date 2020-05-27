using Core.Tokens.Item;
using Core.Tokens.Typed;

namespace Core.Interfaces
{
    public interface IVisitor<out T>
    {
        T Visit(JObject jObject);
        
        T Visit(JProperty jProperty);

        T Visit(JArray jArray);

        T Visit(BooleanToken token);
        
        T Visit(DoubleToken token);
        
        T Visit(StringToken token);
    }
}
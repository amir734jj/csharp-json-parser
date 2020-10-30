using Core.Tokens.Item;
using Core.Tokens.Typed;

namespace Core.Interfaces
{
    public interface IVisitor<out T>
    {
        T Visit(JObject @object);
        
        T Visit(JProperty property);

        T Visit(JArray array);

        T Visit(BooleanToken token);
        
        T Visit(DoubleToken token);
        
        T Visit(StringToken token);
        
        T Visit(NullToken token);
    }
}
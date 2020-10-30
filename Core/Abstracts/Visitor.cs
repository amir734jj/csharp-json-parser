using Core.Interfaces;
using Core.Tokens;
using Core.Tokens.Item;
using Core.Tokens.Typed;

namespace Core.Abstracts
{
    public abstract class Visitor<T> : IVisitor<T>
    {
        protected T Visit(JToken token)
        {
            return token switch
            {
                JObject jObject => Visit(jObject),
                JProperty jProperty => Visit(jProperty),
                JArray jArray => Visit(jArray),
                DoubleToken doubleToken => Visit(doubleToken),
                StringToken stringToken => Visit(stringToken),
                BooleanToken booleanToken => Visit(booleanToken),
                NullToken nullToken => Visit(nullToken),
                _ => default
            };
        }

        public abstract T Visit(JObject @object);

        public abstract T Visit(JProperty property);

        public abstract T Visit(JArray array);

        public abstract T Visit(BooleanToken token);

        public abstract T Visit(DoubleToken token);

        public abstract T Visit(StringToken token);

        public abstract T Visit(NullToken token);
    }
}
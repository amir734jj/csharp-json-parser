using Core.Interfaces;
using Core.Tokens;
using Core.Tokens.Item;
using Core.Tokens.Typed;
using FParsec.CSharp;
using FParsec;
using Microsoft.FSharp.Core;
using static FParsec.CSharp.CharParsersCS;
using static FParsec.CSharp.PrimitivesCS;

namespace Core
{
    public class JsonParser : IJsonParser
    {
        public FSharpFunc<CharStream<Unit>, Reply<JObject>> Parser { get; }

        public static IJsonParser New()
        {
            return new JsonParser();
        }
        
        private JsonParser()
        {
            FSharpFunc<CharStream<Unit>, Reply<JToken>> jValue = null;

            var jNull = StringCI("null", (JToken) new NullToken()).Lbl("null");

            var jNum = Float.Map(i => (JToken) i).Lbl("number");

            var jBool = StringCI("true").Or(StringCI("false"))
                .Map(b => (JToken) bool.Parse(b))
                .Lbl("bool");

            var quotedString = Between('"', ManyChars(NoneOf("\"")), '"');

            var jString = quotedString.Map(s => (JToken) s).Lbl("string");

            var arrItems = Many(Rec(() => jValue), CharP(',').And(WS));
            var jArray = Between(CharP('[').And(WS), arrItems, CharP(']'))
                .Map(elems => (JToken) new JArray(elems))
                .Lbl("array");

            var jIdentifier = quotedString.Lbl("identifier");
            var jProp = jIdentifier.And(WS).And(Skip(':')).And(WS).And(Rec(() => jValue))
                .Map((name, value) => new JProperty(name, value));
            var objProps = Many(jProp, CharP(',').And(WS));
            var jObject = Between(CharP('{').And(WS), objProps, CharP('}'))
                .Map(props => (JToken) new JObject(props))
                .Lbl("object");

            jValue = Choice(jNum, jBool, jNull, jString, jArray, jObject).And(WS);

            Parser = WS.And(jObject).And(WS).And(EOF).Map(o => o switch
            {
                JObject objectValue => objectValue,
                _ => null
            });
        }
    }
}
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
            FSharpFunc<CharStream<Unit>, Reply<JToken>> jvalue = null;

            var jnull = StringCI("null", (JToken) new NullToken()).Lbl("null");

            var jnum = Float.Map(i => (JToken) i).Lbl("number");

            var jbool = StringCI("true").Or(StringCI("false"))
                .Map(b => (JToken) bool.Parse(b))
                .Lbl("bool");

            var quotedString = Between('"', ManyChars(NoneOf("\"")), '"');

            var jstring = quotedString.Map(s => (JToken) s).Lbl("string");

            var arrItems = Many(Rec(() => jvalue), CharP(',').And(WS));
            var jarray = Between(CharP('[').And(WS), arrItems, CharP(']'))
                .Map(elems => (JToken) new JArray(elems))
                .Lbl("array");

            var jidentifier = quotedString.Lbl("identifier");
            var jprop = jidentifier.And(WS).And(Skip(':')).And(WS).And(Rec(() => jvalue))
                .Map((name, value) => new JProperty(name, value));
            var objProps = Many(jprop, CharP(',').And(WS));
            var jobject = Between(CharP('{').And(WS), objProps, CharP('}'))
                .Map(props => (JToken) new JObject(props))
                .Lbl("object");

            jvalue = Choice(jnum, jbool, jnull, jstring, jarray, jobject).And(WS);

            Parser = WS.And(jobject).And(WS).And(EOF).Map(o => o switch
            {
                JObject objectValue => objectValue,
                _ => null
            });
        }
    }
}
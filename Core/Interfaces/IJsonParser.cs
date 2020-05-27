using Core.Tokens.Item;
using FParsec;
using Microsoft.FSharp.Core;

namespace Core.Interfaces
{
    public interface IJsonParser
    {
        public FSharpFunc<CharStream<Unit>, Reply<JObject>> Parser { get; }
    }
}
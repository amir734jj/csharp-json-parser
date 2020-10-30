using System;
using AutoFixture;
using Core;
using Core.Traversal;
using FParsec.CSharp;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var parser = JsonParser.New();

            var payload = parser.Parser.ParseFile("example.json");

            Console.WriteLine(new JsonPrettyFormatter().Visit(payload.Result));

            var fixture = new Fixture();

            var model = fixture.Build<Model>().Without(x => x.RefNull).Create();

            Console.Write("\nShould be True to make sure serializer/deserializer works: ");
            Console.WriteLine(JsonSerializer.New().ToJson(JsonDeserializer.New().FromJson<Model>(JsonSerializer.New().ToJson(model))) == JsonSerializer.New().ToJson(model));
        }
    }
}
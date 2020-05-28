using System;
using System.Diagnostics;
using Core;
using Core.Traversal;
using FParsec.CSharp;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var p = JsonParser.New();

            var payload = p.Parser.ParseFile("example.json");

            Console.WriteLine(new JsonPrettyFormatter().Visit(payload.Result));

            var obj = new
            {
                name=  "Amir",
                flag = true
            };

            Console.WriteLine(JsonSerializer.New().ToJson(obj));
        }
    }
}
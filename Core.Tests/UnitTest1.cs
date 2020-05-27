using System;
using FParsec.CSharp;
using Xunit;

namespace Core.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var p = JsonParser.New();

            var result = p.Parser.ParseString(@"{ ""name"": ""amir"" }");
            
            Console.WriteLine(result);
        }
    }
}
using System.Linq;
using System.Text;
using Core.Abstracts;
using Core.Tokens.Item;
using Core.Tokens.Typed;

namespace Core.Traversal
{
    public class JsonPrettyFormatter : Visitor<string>
    {
        private int _indent;

        private string Indent()
        {
            return string.Empty.PadRight(_indent, ' ');
        }
        
        public override string Visit(JObject jObject)
        {
            var sb = new StringBuilder();

            sb.AppendLine("{");
            
            _indent++;
            var count = jObject.Value.Count();
            var index = 0;
            foreach (var jProperty in jObject.Value)
            {
                sb.Append(Indent());
                sb.Append(Visit(jProperty));
                if (index++ + 1 < count)
                {
                    sb.Append(",");
                }
                
                sb.AppendLine();
            }
            _indent--;
            
            sb.Append(Indent() + "}");

            return sb.ToString();
        }

        public override string Visit(JProperty jProperty)
        {
            return @$"""{jProperty.Value.Key}"": {Visit(jProperty.Value.Value)}";
        }
        
        public override string Visit(JArray jArray)
        {
            return @$"[{string.Join(", ", jArray.Value.Select(Visit))}]";
        }

        public override string Visit(BooleanToken token)
        {
            return token.ToString().ToLower();
        }

        public override string Visit(DoubleToken token)
        {
            return token.ToString();
        }

        public override string Visit(StringToken token)
        {
            return token.ToString();
        }

        public override string Visit(NullToken token)
        {
            return token.ToString();
        }
    }
}
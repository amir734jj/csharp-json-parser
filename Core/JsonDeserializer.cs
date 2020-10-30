using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Core.Interfaces;
using Core.Tokens;
using Core.Tokens.Item;
using Core.Tokens.Typed;
using FParsec;
using FParsec.CSharp;

namespace Core
{
    public class JsonDeserializer : IJsonDeserializer
    {
        public static IJsonDeserializer New()
        {
            return new JsonDeserializer();
        }

        private JsonDeserializer()
        {
        }

        private static object FromToken(JToken token, Type type)
        {
            switch (token)
            {
                case NullToken _:
                    return null;
                case BooleanToken booleanToken:
                    return booleanToken.Value;
                case DoubleToken doubleToken:
                    return doubleToken.Value;
                case StringToken stringToken:
                    return stringToken.Value;
                case JArray jArray:
                    var list =  (IList) Activator.CreateInstance(typeof(List<>).MakeGenericType(type.GetGenericArguments()));
                    foreach (var item in jArray.Value.Select(x => FromToken(x, type.GetGenericArguments().First())))
                    {
                        list?.Add(item);
                    }
                    return list;
                case JProperty jProperty:
                    return new KeyValuePair<string, object>(jProperty.Value.Key,
                        FromToken(jProperty.Value.Value, type));
                case JObject jObject:
                    var instance = Activator.CreateInstance(type);
                    foreach (var (propertyInfo, jProperty) in type.GetProperties().Join(jObject.Value,
                        info => info.Name, property => property.Value.Key,
                        (propertyInfo, property) => (propertyInfo, property)))
                    {
                        propertyInfo.SetValue(instance, FromToken(jProperty.Value.Value, propertyInfo.PropertyType));
                    }

                    return instance;
                default:
                    return null;
            }
        }

        public T FromJson<T>(string source)
        {
            var (status, result, _) = JsonParser.New().Parser.ParseString(source);

            return status == ReplyStatus.Ok ? (T) FromToken(result, typeof(T)) : default;
        }
    }
}
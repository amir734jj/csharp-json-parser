# csharp-json-parser

Simple JSON parser and pretty printer using `FParsec` library

Includes:
- JSON parser
- JSON pretty printer
- JSON serializer and deserializer with support for recurisve types

```json
{
 "glossary": {
  "title": "example glossary",
  "GlossDiv": {
   "title": "S",
   "GlossList": {
    "GlossEntry": {
     "ID": "SGML",
     "Id": 123,
     "Flag": true,
     "Foo": null,
     "SortAs": "SGML",
     "GlossTerm": "Standard Generalized Markup Language",
     "Acronym": "SGML",
     "Abbrev": "ISO 8879:1986",
     "GlossDef": {
      "para": "A meta-markup language, used to create markup languages such as DocBook.",
      "GlossSeeAlso": ["GML", "XML"]
     },
     "GlossSee": "markup"
    }
   }
  }
 }
}
```

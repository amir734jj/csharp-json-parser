namespace Core.Interfaces
{
    public interface IJsonDeserializer
    {
        T FromJson<T>(string source);
    }
}
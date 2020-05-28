namespace Core.Interfaces
{
    public interface IJsonSerializer
    {
        string ToJson<T>(T source);
    }
}
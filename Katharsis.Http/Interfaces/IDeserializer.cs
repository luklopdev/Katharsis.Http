namespace Katharsis.Http.Interfaces
{
    public interface IDeserializer
    {
        T Deserialize<T>(string content);
    }
}

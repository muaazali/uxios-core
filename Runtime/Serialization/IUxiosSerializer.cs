namespace Uxios.Core.Serialization
{
    public interface IUxiosSerializer
    {
        string Serialize<T>(T obj);
        T Deserialize<T>(string json);
    }
}
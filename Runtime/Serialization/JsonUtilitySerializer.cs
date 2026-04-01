using UnityEngine;

namespace Uxios.Core.Serialization
{
    public class JsonUtilitySerializer : IUxiosSerializer
    {
        public string Serialize<T>(T obj)
        {
            return JsonUtility.ToJson(obj);
        }

        public T Deserialize<T>(string json)
        {
            return JsonUtility.FromJson<T>(json);
        }
    }
}
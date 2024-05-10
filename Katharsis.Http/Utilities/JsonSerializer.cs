using Katharsis.Http.Interfaces;
using Newtonsoft.Json;

namespace Katharsis.Http.Utilities
{
    internal class JsonSerializer : ISerializer, IDeserializer
    {
        public T Deserialize<T>(string content)
        {
            if(string.IsNullOrWhiteSpace(content))
                throw new ArgumentNullException(nameof(content));

            return JsonConvert.DeserializeObject<T>(content);
        }

        public string Serialize(object body) 
        {
            if (body == null)
                throw new ArgumentNullException(nameof(body));

            return JsonConvert.SerializeObject(body);
        }
    }
}

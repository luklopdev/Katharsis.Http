using Katharsis.Http;
using Katharsis.Http.Interfaces;
using Newtonsoft.Json;

var defaultHeaders = new Dictionary<string, string>()
{
    ["key"] = "PR1V4T3K3Y"
};


const string BASE_URL = "https://reqres.in";
KatharsisClient client = new KatharsisClient(BASE_URL);

var jsonSerializer = new JsonSerializer();
client.Serializer = jsonSerializer;


KatharsisResponse response = await client.GetAsync("api/users?page=2");

Console.ReadKey();

internal class JsonSerializer : ISerializer
{
    public string Serialize(object body) => JsonConvert.SerializeObject(body);
}
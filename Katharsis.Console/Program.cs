using Katharsis.Http;
using Katharsis.Http.Interfaces;
using Newtonsoft.Json;

var defaultHeaders = new Dictionary<string, string>()
{
    ["key"] = "PR1V4T3K3Y"
};

const string BASE_URL = "https://reqres.in";
KatharsisClient client = new KatharsisClient(BASE_URL, defaultHeaders);

var picture = new
{
    Base64String = "po123u9asdjd1ej12j!JSSasdjJSA0sJd=="
};

var additionalHeaders = new Dictionary<string, string>
{
    ["Content-Type"] = "x-www-form-urlencoded"
};

KatharsisResponse response = client.Post("api/profile/uploadPicture?profileId=1", picture, additionalHeaders);

Console.ReadKey();

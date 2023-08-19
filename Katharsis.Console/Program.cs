using Katharsis.Http;

var token = File.ReadAllText("appsettings.txt");
const string BASE_URL = "http://api.weatherapi.com/v1";

var defaultHeaders = new Dictionary<string, string>()
{
    ["key"] = token
};

var client1 = new KatharsisClient();
var client = new KatharsisClient(BASE_URL);

var locations = new
{
    Locations = new List<object>
    { 
        new 
        {
            Q = "Warsaw"
        },
        new
        {
            Q = "London"
        }
    }
};

var result = client.Request(BASE_URL, Method.Get, defaultHeaders);


Console.ReadKey();
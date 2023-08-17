using Katharsis.Http;

var token = File.ReadAllText("appsettings.txt");
const string BASE_URL = "http://api.weatherapi.com/v1";

var defaultHeaders = new Dictionary<string, string>()
{
    ["key"] = token
};


var client = new KatharsisClient(BASE_URL, defaultHeaders);

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

var result = client.Request("current.json?q=Bulk", Method.Get);

Console.ReadKey();
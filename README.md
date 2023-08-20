# Katharsis.Http

Katharsis.Http is a library to help you write your HTTP requests code easier.

## Documentation

Please read the following documentation to understand how to use Katharsis.Http library.

### The Client
To make your first HTTP request you need to create HTTP Client. HTTP Client is the object that handles all HTTP requests. In Katharsis.Http class KatharsisClient instance of object handles all HTTP request methods.

To create The Client you should call KatharsisClient constructor with base url address.

```
using Katharsis.Http;

const string BASE_URL = "https://reqres.in";
KatharsisClient client = new KatharsisClient(BASE_URL);
```

The above code will create client instance of object with base url, so every request that will be send by this client will try to request `BASE_URL` address.

#### Default headers for The Client

You can also attach default headers that will be sent to every request using that specific client.

```
using Katharsis.Http;

var defaultHeaders = new Dictionary<string, string>()
{
    ["key"] = "PR1V4T3K3Y"
};

const string BASE_URL = "https://reqres.in";
KatharsisClient client = new KatharsisClient(BASE_URL, defaultHeaders);
```

#### Custom body serialization

Sometimes you need to serialize your HTTP body content in a specific way. Katharsis.Http allows you to do that. `KatharsisClient` object contains property called `Serializer` that implements `ISerializer` interface. You can change implementation of that property by creating you own class that will ipmlement such interface e.g.:

```
using Katharsis.Http.Interfaces;
using Newtonsoft.Json;

internal class JsonSerializer : ISerializer
{
    public string Serialize(object body) => JsonConvert.SerializeObject(body);
}
```

The above class is using `Newtonsoft.Json` library to serialize objects. `Serialize(object body)` method returns `string` that will be later on attached to request's HTTP content.
There are two ways of changing the Serializer property of your client: By `constructor` and by `setter`

##### Constructor

```
const string BASE_URL = "https://reqres.in";
var jsonSerializer = new JsonSerializer();
KatharsisClient client = new KatharsisClient(BASE_URL, jsonSerializer);
```

##### Setter

```
const string BASE_URL = "https://reqres.in";
KatharsisClient client = new KatharsisClient(BASE_URL);

var jsonSerializer = new JsonSerializer();
client.Serializer = jsonSerializer;
```

### The Request

To invoke your first request to `BASE_URL` using The Client, you can use several methods based on what type of request you would like to make. Each of method will always return `KatharsisResponse` instance of object containing:

* Content
* Exception (If any exception has been thrown internally in method, in other case it will be `null`)
* Request (Information about request that has been sent)
* Status

To invoke simple `GET` HTTP Request you can call either `Get()` or `GetAsync()` (for asynchronous operation) method with `resource parameter`.

```
KatharsisResponse response = client.Get("api/users?page=2");
```
or
```
KatharsisResponse response = await client.GetAsync("api/users?page=2");
```

Katharsis.Http handles 5 basic methods for `GET`, `POST`, `PUT`, `DELETE`, `PATCH` listed below:
* `Get()` or `GetAsync()`
* `Post()` or `PostAsync()`
* `Put()` or `PutAsync()`
* `Delete()` or `DeleteAsync()`
* `Patch()` or `PatchAsync()`

For all of these methods you can pass parameters, which `resource` is required.


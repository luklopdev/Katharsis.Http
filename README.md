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

### The Request

To invoke your first request to `BASE_URL` using The Client, you can use several methods based on what type of request you would like to make. Each of method will always return `KatharsisResponse` instance of object containing:

* Content
* Exception (If any exception has been thrown internally in method, in other case it will be `null`)
* Request (Information about request that has been sent)
* Status

To invoke simple `GET` HTTP Request you can call either `Get()` or `GetAsync()` (for asynchronous operation) method.

```
KatharsisResponse response = client.Get("api/users?page=2");
```
or
```
KatharsisResponse response = await client.GetAsync("api/users?page=2");
```

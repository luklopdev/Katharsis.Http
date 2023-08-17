using Katharsis.Http.Interfaces;
using Katharsis.Http.Utilities;

namespace Katharsis.Http
{
    public enum Method
    {
        Get,
        Post, 
        Put, 
        Delete,
        Patch,
        Head,
        Options,
        Trace
    }

    public class KatharsisClient
    {
        public ISerializer Serializer { get; set; }
        public string URL { get; set; }
        public Dictionary<string, string> Headers { get; set; }

        public KatharsisClient()
        {
            URL = string.Empty;
            Serializer = new JsonSerializer();
            Headers = new Dictionary<string, string>();
        }

        public KatharsisClient(string url) : this()
        {
            URL = url;
        }

        public KatharsisClient(string url, ISerializer serializer) : this(url)
        {
            Serializer = serializer;
        }

        public KatharsisClient(string url, Dictionary<string, string> headers) : this(url)
        {
            Headers = headers;
        }

        public KatharsisResponse Request(string resource)
            => RequestAsync(resource).Result;

        public KatharsisResponse Request(string resource, Dictionary<string, string> headers = null)
            => RequestAsync(resource, headers).Result;

        public KatharsisResponse Request(string resource, Method method)
            => RequestAsync(resource, method).Result;

        public KatharsisResponse Request(string resource, Method method, Dictionary<string, string> headers = null)
            => RequestAsync(resource, method, headers).Result;

        public KatharsisResponse Request(string resource, Method method, object body)
            => RequestAsync(resource, method, body).Result;

        public KatharsisResponse Request(string resource, Method method, object body, Dictionary<string, string> headers = null)
            => RequestAsync(resource, method, body, headers).Result;

        public async Task<KatharsisResponse> RequestAsync(string resource)
        {
            return await RequestAsync(resource, Method.Get);
        }

        public async Task<KatharsisResponse> RequestAsync(string resource, Dictionary<string, string> headers = null)
        {
            return await RequestAsync(resource, Method.Get, headers);
        }

        public async Task<KatharsisResponse> RequestAsync(string resource, Method method)
        {
            return await RequestAsync(resource, method, null);
        }

        public async Task<KatharsisResponse> RequestAsync(string resource, Method method, Dictionary<string, string> headers = null)
        {
            return await RequestAsync(resource, method, null, headers);
        }

        public async Task<KatharsisResponse> RequestAsync(string resource, Method method, object body, Dictionary<string, string> headers = null)
        {
            KatharsisResponse katharsisResponse = new KatharsisResponse();
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage();
            httpRequestMessage.Method = GetMethod(method);
            httpRequestMessage.RequestUri = new Uri(Path.Combine(URL, resource));

            if (body != null)
            {
                httpRequestMessage.Content = GetContent(body);
            }

            if (headers != null)
            {
                foreach (var header in headers)
                {
                    httpRequestMessage.Headers.Add(header.Key, header.Value);
                }
            }

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(URL);
                foreach (var header in Headers)
                {
                    client.DefaultRequestHeaders.Add(header.Key, header.Value);
                }

                HttpResponseMessage response = await client.SendAsync(httpRequestMessage);
                katharsisResponse.Content = await response.Content.ReadAsStringAsync();
            }

            return katharsisResponse;
        }

        public KatharsisResponse Get(string resource)
        {
            return RequestAsync(resource).Result;
        }

        private HttpMethod GetMethod(Method method) => method switch
        {
            Method.Get => HttpMethod.Get,
            Method.Post => HttpMethod.Post,
            Method.Put => HttpMethod.Put,
            Method.Delete => HttpMethod.Delete,
            Method.Patch => HttpMethod.Patch,
            Method.Head => HttpMethod.Head,
            Method.Options => HttpMethod.Options,
            Method.Trace => HttpMethod.Trace,
            _ => HttpMethod.Get,
        };

        private HttpContent GetContent(object body)
        {
            return new StringContent(Serializer.Serialize(body));
        }
    }
}

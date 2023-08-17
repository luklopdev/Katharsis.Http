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

        public KatharsisClient()
        {
            URL = string.Empty;
            Serializer = new JsonSerializer();
        }

        public KatharsisClient(string url) : this()
        {
            URL = url;
        }

        public KatharsisClient(string url, ISerializer serializer) : this(url)
        {
            Serializer = serializer;
        }

        public async Task<KatharsisResponse> RequestAsync(string resource)
        {
            return await RequestAsync(resource, Method.Get);
        }

        public async Task<KatharsisResponse> RequestAsync(string resource, Method method)
        {
            return await RequestAsync(resource, method, null);
        }

        public async Task<KatharsisResponse> RequestAsync(string resource, Method method, object body)
        {
            KatharsisResponse katharsisResponse = new KatharsisResponse();
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage();
            httpRequestMessage.Method = GetMethod(method);
            httpRequestMessage.RequestUri = new Uri(Path.Combine(URL, resource));

            if (body != null)
            {
                httpRequestMessage.Content = GetContent(body);
            }

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.SendAsync(httpRequestMessage);
                    response.EnsureSuccessStatusCode();
                    katharsisResponse.Content = await response.Content.ReadAsStringAsync();

                }
            }
            catch (HttpRequestException ex)
            {
                throw ex;
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

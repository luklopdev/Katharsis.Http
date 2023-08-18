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

    /// <summary>
    /// Class that handles HTTP Requests
    /// </summary>
    public class KatharsisClient
    {
        public ISerializer Serializer { get; set; }
        public string URL { get; set; }
        public Dictionary<string, string> Headers { get; set; }

        /// <summary>
        /// Initializes a new instance of <see cref="KatharsisClient"/> class that handles HTTP requests.
        /// </summary>
        /// <remarks>
        /// New instance is being created with empty 
        /// <see cref="URL">Web API's Base URL</see>,
        /// default JSON <see cref="Serializer">Serializer</see> that implements <see cref="ISerializer"/> interface,
        /// and empty <see cref="Headers">Headers</see> instance of <see cref="Dictionary{TKey, TValue}"/> class.
        /// </remarks>
        public KatharsisClient()
        {
            URL = string.Empty;
            Serializer = new JsonSerializer();
            Headers = new Dictionary<string, string>();
        }


        /// <inheritdoc cref="KatharsisClient()" path="/summary"/>
        /// <param name="url"><see cref="URL">Web API's Base URL</see></param>
        /// <remarks>
        /// New instance is being created with provided <paramref name="url"/> for 
        /// <see cref="URL">Web API's Base URL</see>,
        /// default JSON <see cref="Serializer">Serializer</see> that implements <see cref="ISerializer"/> interface,
        /// and empty <see cref="Headers">Headers</see> instance of <see cref="Dictionary{TKey, TValue}"/> class.
        /// </remarks>
        public KatharsisClient(string url) : this()
        {
            URL = url;
        }

        /// <inheritdoc cref="KatharsisClient()" path="/summary"/>
        /// <param name="url"><see cref="URL">Web API's Base URL</see></param>
        /// <remarks>
        /// New instance is being created with provided <paramref name="url"/> for 
        /// <see cref="URL">Web API's Base URL</see>,
        /// provided <paramref name="serializer"/> for <see cref="Serializer">Serializer</see> 
        /// that implements <see cref="ISerializer"/> interface,
        /// and empty <see cref="Headers">Headers</see> instance of <see cref="Dictionary{TKey, TValue}"/> class.
        /// <example>
        /// <para>This shows how to implement Serializer class for serializing body content for HTTP request:</para>
        /// <code>
        /// var serializer = new JsonSerializer();
        /// var client = new KatharsisClient(BASE_URL, serializer);
        ///
        /// internal class JsonSerializer : ISerializer
        /// {
        ///     public string Serialize(object body) => JsonConvert.SerializeObject(body);
        /// }
        /// </code>
        /// </example>
        /// </remarks>

        public KatharsisClient(string url, ISerializer serializer) : this(url)
        {
            Serializer = serializer;
        }

        /// <summary>
        /// A class that handles HTTP requests.
        /// </summary>
        /// <param name="url">Base URL Address.</param>
        /// <param name="headers">Default headers that will be attached to every request made by this object.</param>
        public KatharsisClient(string url, Dictionary<string, string> headers) : this(url)
        {
            Headers = headers;
        }

        /// <summary>
        /// Sends HTTP request for provided resource.
        /// </summary>
        /// <param name="resource">Web API resource.</param>
        /// <returns>HTTP response object.</returns>
        public KatharsisResponse Request(string resource)
            => RequestAsync(resource).Result;

        /// <summary>
        /// Sends HTTP request for provided resource.
        /// </summary>
        /// <param name="resource">Web API resource.</param>
        /// <param name="headers">Additional headers for this HTTP request.</param>
        /// <returns>HTTP response object.</returns>
        public KatharsisResponse Request(string resource, Dictionary<string, string> headers)
            => RequestAsync(resource, headers).Result;

        /// <summary>
        /// Sends HTTP request for provided resource.
        /// </summary>
        /// <param name="resource">Web API resource.</param>
        /// <param name="method">Additional headers for this HTTP request.</param>
        /// <returns>HTTP response object.</returns>
        public KatharsisResponse Request(string resource, Method method)
            => RequestAsync(resource, method).Result;

        public KatharsisResponse Request(string resource, Method method, Dictionary<string, string> headers)
            => RequestAsync(resource, method, headers).Result;

        public KatharsisResponse Request(string resource, Method method, object body)
            => RequestAsync(resource, method, body).Result;

        public KatharsisResponse Request(string resource, Method method, object body, Dictionary<string, string> headers)
            => RequestAsync(resource, method, body, headers).Result;

        public async Task<KatharsisResponse> RequestAsync(string resource)
        {
            return await RequestAsync(resource, Method.Get);
        }

        public async Task<KatharsisResponse> RequestAsync(string resource, Dictionary<string, string> headers)
        {
            return await RequestAsync(resource, Method.Get, headers);
        }

        public async Task<KatharsisResponse> RequestAsync(string resource, Method method)
        {
            return await RequestAsync(resource, method, null);
        }

        public async Task<KatharsisResponse> RequestAsync(string resource, Method method, object body)
        {
            return await RequestAsync(resource, method, body, null);
        }

        public async Task<KatharsisResponse> RequestAsync(string resource, Method method, Dictionary<string, string> headers)
        {
            return await RequestAsync(resource, method, null, headers);
        }

        public async Task<KatharsisResponse> RequestAsync(string resource, Method method, object body, Dictionary<string, string> headers)
        {
            KatharsisResponse katharsisResponse = new KatharsisResponse();
            try
            {
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
            }
            catch (Exception ex)
            {
                katharsisResponse.Exception = ex;
            }

            return katharsisResponse;
        }

        public KatharsisResponse Get(string resource) 
            => RequestAsync(resource, Method.Get).Result;

        public KatharsisResponse Get(string resource, Dictionary<string, string> headers) 
            => RequestAsync(resource, Method.Get, headers).Result;

        public KatharsisResponse Get(string resource, object body)
            => RequestAsync(resource, Method.Get, body).Result;

        public KatharsisResponse Get(string resource, object body, Dictionary<string, string> headers)
            => RequestAsync(resource, Method.Get, body, headers).Result;

        public async Task<KatharsisResponse> GetAsync(string resource)
            => await RequestAsync(resource, Method.Get);

        public async Task<KatharsisResponse> GetAsync(string resource, Dictionary<string, string> headers)
            => await RequestAsync(resource, Method.Get, headers);

        public async Task<KatharsisResponse> GetAsync(string resource, object body)
            => await RequestAsync(resource, Method.Get, body);

        public async Task<KatharsisResponse> GetAsync(string resource, object body, Dictionary<string, string> headers)
            => await RequestAsync(resource, Method.Get, body, headers);

        public KatharsisResponse Post(string resource) 
            => RequestAsync(resource, Method.Post).Result;

        public KatharsisResponse Post(string resource, Dictionary<string, string> headers)
            => RequestAsync(resource, Method.Post, headers).Result;

        public KatharsisResponse Post(string resource, object body)
            => RequestAsync(resource, Method.Post, body).Result;

        public KatharsisResponse Post(string resource, object body, Dictionary<string, string> headers)
            => RequestAsync(resource, Method.Post, body, headers).Result;

        public async Task<KatharsisResponse> PostAsync(string resource)
            => await RequestAsync(resource, Method.Post);

        public async Task<KatharsisResponse> PostAsync(string resource, Dictionary<string, string> headers)
            => await RequestAsync(resource, Method.Post, headers);

        public async Task<KatharsisResponse> PostAsync(string resource, object body)
            => await RequestAsync(resource, Method.Post, body);

        public async Task<KatharsisResponse> PostAsync(string resource, object body, Dictionary<string, string> headers)
            => await RequestAsync(resource, Method.Post, body, headers);

        public KatharsisResponse Put(string resource)
            => RequestAsync(resource, Method.Put).Result;

        public KatharsisResponse Put(string resource, Dictionary<string, string> headers)
            => RequestAsync(resource, Method.Put, headers).Result;

        public KatharsisResponse Put(string resource, object body)
            => RequestAsync(resource, Method.Put, body).Result;

        public KatharsisResponse Put(string resource, object body, Dictionary<string, string> headers)
            => RequestAsync(resource, Method.Put, body, headers).Result;

        public async Task<KatharsisResponse> PutAsync(string resource)
            => await RequestAsync(resource, Method.Put);

        public async Task<KatharsisResponse> PutAsync(string resource, Dictionary<string, string> headers)
            => await RequestAsync(resource, Method.Put, headers);

        public async Task<KatharsisResponse> PutAsync(string resource, object body)
            => await RequestAsync(resource, Method.Put, body);

        public async Task<KatharsisResponse> PutAsync(string resource, object body, Dictionary<string, string> headers)
            => await RequestAsync(resource, Method.Put, body, headers);

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

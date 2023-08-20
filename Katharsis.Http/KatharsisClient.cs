using Katharsis.Http.Interfaces;
using Katharsis.Http.Utilities;

namespace Katharsis.Http
{
    /// <summary>
    /// Enum for HTTP request methods.
    /// </summary>
    public enum Method
    {
        /// <summary>
        /// The <see cref="Get"/> method requests a representation of the specified resource. Requests using GET should only retrieve data.
        /// </summary>
        Get,
        /// <summary>
        /// The <see cref="Post"/> method submits an entity to the specified resource, often causing a change in state or side effects on the server.
        /// </summary>
        Post,
        /// <summary>
        /// The <see cref="Put"/> method replaces all current representations of the target resource with the request payload.
        /// </summary>
        Put,
        /// <summary>
        /// The <see cref="Delete"/> method deletes the specified resource.
        /// </summary>
        Delete,
        /// <summary>
        /// The <see cref="Patch"/> method applies partial modifications to a resource.
        /// </summary>
        Patch,
        /// <summary>
        /// The <see cref="Head"/> method asks for a response identical to a <see cref="Get"/> request, but without the response body.
        /// </summary>
        Head,
        /// <summary>
        /// The <see cref="Options"/> method describes the communication options for the target resource.
        /// </summary>
        Options,
        /// <summary>
        /// The <see cref="Trace"/> method performs a message loop-back test along the path to the target resource.
        /// </summary>
        Trace
    }

    /// <summary>
    /// Class that handles HTTP Requests
    /// </summary>
    public class KatharsisClient
    {
        ///<remarks>
        /// <example>
        /// <para>This shows how to implement Serializer class for serializing body content for HTTP request:</para>
        /// <code>
        /// var serializer = new JsonSerializer();
        /// var client = new KatharsisClient(BASE_URL);
        /// client.Serializer = serializer
        ///
        /// internal class JsonSerializer : ISerializer
        /// {
        ///     public string Serialize(object body) => JsonConvert.SerializeObject(body);
        /// }
        /// </code>
        /// </example>
        /// </remarks>
        /// <value>Instance of object that serializes body object to HTTP Content request.</value>
        public ISerializer Serializer { private get; set; }

        /// <summary>
        /// Web API's base address.
        /// </summary>
        public string URL { get; set; }

        /// <summary>
        /// Client's default headers for HTTP requests.
        /// </summary>
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

        /// <inheritdoc cref="KatharsisClient()" path="/summary"/>
        /// <param name="url"><see cref="URL">Web API's Base URL</see></param>
        /// <remarks>
        /// New instance is being created with provided <paramref name="url"/> for 
        /// <see cref="URL">Web API's Base URL</see>,
        /// default JSON <see cref="Serializer">Serializer</see> that implements <see cref="ISerializer"/> interface,
        /// and provided <paramref name="headers"/> for <see cref="Headers">Headers</see> instance of <see cref="Dictionary{TKey, TValue}"/> class.
        /// <example>
        /// <para>This shows how to add default headers that will be attached to every HTTP request when using the client:</para>
        /// <code>
        /// var defaultHeaders = new Dictionary&lt;string, string&gt;()
        /// {
        ///     ["Key"] = "PR!V$T3K3Y",
        ///     ["content-type"] = "application/json"
        /// };
        /// 
        /// var client = new KatharsisClient(BASE_URL, defaultHeaders);
        ///
        /// </code>
        /// </example>
        /// </remarks>
        public KatharsisClient(string url, Dictionary<string, string> headers) : this(url)
        {
            Headers = headers;
        }

        /// <inheritdoc cref="Request(string, Method, object, Dictionary{string, string})"/>
        public KatharsisResponse Request(string resource)
            => RequestAsync(resource).Result;

        /// <inheritdoc cref="Request(string, Method, object, Dictionary{string, string})"/>
        public KatharsisResponse Request(string resource, Dictionary<string, string> headers)
            => RequestAsync(resource, headers).Result;

        /// <inheritdoc cref="Request(string, Method, object, Dictionary{string, string})"/>
        public KatharsisResponse Request(string resource, Method method)
            => RequestAsync(resource, method).Result;

        /// <inheritdoc cref="Request(string, Method, object, Dictionary{string, string})"/>
        public KatharsisResponse Request(string resource, Method method, Dictionary<string, string> headers)
            => RequestAsync(resource, method, headers).Result;

        /// <inheritdoc cref="Request(string, Method, object, Dictionary{string, string})"/>
        public KatharsisResponse Request(string resource, Method method, object body)
            => RequestAsync(resource, method, body).Result;

        /// <summary>
        /// Sends HTTP request.
        /// </summary>
        /// <param name="resource">Web API's resource.</param>
        /// <param name="method">HTTP request <see cref="Method"/>.</param>
        /// <param name="body">Body object that will be serialized by <see cref="Serializer"/> object and attached to HTTP's request content.</param>
        /// <param name="headers">Additional headers for this HTTP request.</param>
        /// <returns>An HTTP response object.</returns>
        public KatharsisResponse Request(string resource, Method method, object body, Dictionary<string, string> headers)
            => RequestAsync(resource, method, body, headers).Result;

        /// <inheritdoc cref="RequestAsync(string, Method, object, Dictionary{string, string})"/>
        public async Task<KatharsisResponse> RequestAsync(string resource)
        {
            return await RequestAsync(resource, Method.Get);
        }

        /// <inheritdoc cref="RequestAsync(string, Method, object, Dictionary{string, string})"/>
        public async Task<KatharsisResponse> RequestAsync(string resource, Dictionary<string, string> headers)
        {
            return await RequestAsync(resource, Method.Get, headers);
        }

        /// <inheritdoc cref="RequestAsync(string, Method, object, Dictionary{string, string})"/>
        public async Task<KatharsisResponse> RequestAsync(string resource, Method method)
        {
            return await RequestAsync(resource, method, null);
        }

        /// <inheritdoc cref="RequestAsync(string, Method, object, Dictionary{string, string})"/>
        public async Task<KatharsisResponse> RequestAsync(string resource, Method method, object body)
        {
            return await RequestAsync(resource, method, body, null);
        }

        /// <inheritdoc cref="RequestAsync(string, Method, object, Dictionary{string, string})"/>
        public async Task<KatharsisResponse> RequestAsync(string resource, Method method, Dictionary<string, string> headers)
        {
            return await RequestAsync(resource, method, null, headers);
        }

        /// <summary>
        /// Sends an HTTP request as asynchronous operation.
        /// </summary>
        /// <param name="resource">Web API's resource.</param>
        /// <param name="method">HTTP request <see cref="Method"/>.</param>
        /// <param name="body">Body object that will be serialized by <see cref="Serializer"/> object and attached to HTTP's request content.</param>
        /// <param name="headers">Additional headers for this HTTP request.</param>
        /// <returns>The task object representing asynchronous operation.</returns>
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
                katharsisResponse.Exception = new KatharsisHttpException(@"
Exception has occured in RequestAsync method. Please contact the author of Katharsis.Http library", 
ex);
            }

            return katharsisResponse;
        }

        /// <inheritdoc cref="Get(string, object, Dictionary{string, string})"/>
        public KatharsisResponse Get(string resource) 
            => RequestAsync(resource, Method.Get).Result;

        /// <inheritdoc cref="Get(string, object, Dictionary{string, string})"/>
        public KatharsisResponse Get(string resource, Dictionary<string, string> headers) 
            => RequestAsync(resource, Method.Get, headers).Result;

        /// <inheritdoc cref="Get(string, object, Dictionary{string, string})"/>
        public KatharsisResponse Get(string resource, object body)
            => RequestAsync(resource, Method.Get, body).Result;

        /// <summary>
        /// Sends a GET HTTP request to specified client base's URL and resource.
        /// </summary>
        /// <param name="resource">Web API's resource.</param>
        /// <param name="body">Body object that will be serialized by <see cref="Serializer"/> object and attached to HTTP's request content.</param>
        /// <param name="headers">Additional headers for this HTTP request.</param>
        /// <returns>An HTTP response object.</returns>
        public KatharsisResponse Get(string resource, object body, Dictionary<string, string> headers)
            => RequestAsync(resource, Method.Get, body, headers).Result;

        /// <inheritdoc cref="GetAsync(string, object, Dictionary{string, string})"/>
        public async Task<KatharsisResponse> GetAsync(string resource)
            => await RequestAsync(resource, Method.Get);

        /// <inheritdoc cref="GetAsync(string, object, Dictionary{string, string})"/>
        public async Task<KatharsisResponse> GetAsync(string resource, Dictionary<string, string> headers)
            => await RequestAsync(resource, Method.Get, headers);

        /// <inheritdoc cref="GetAsync(string, object, Dictionary{string, string})"/>
        public async Task<KatharsisResponse> GetAsync(string resource, object body)
            => await RequestAsync(resource, Method.Get, body);

        /// <summary>
        /// Sends a GET HTTP request to specified client base's URL and resource as asynchronous operation.
        /// </summary>
        /// <param name="resource">Web API's resource.</param>
        /// <param name="body">Body object that will be serialized by <see cref="Serializer"/> object and attached to HTTP's request content.</param>
        /// <param name="headers">Additional headers for this HTTP request.</param>
        /// <returns>The task object representing asynchronous operation.</returns>
        public async Task<KatharsisResponse> GetAsync(string resource, object body, Dictionary<string, string> headers)
            => await RequestAsync(resource, Method.Get, body, headers);

        /// <inheritdoc cref="Post(string, object, Dictionary{string, string})"/>
        public KatharsisResponse Post(string resource) 
            => RequestAsync(resource, Method.Post).Result;

        /// <inheritdoc cref="Post(string, object, Dictionary{string, string})"/>
        public KatharsisResponse Post(string resource, Dictionary<string, string> headers)
            => RequestAsync(resource, Method.Post, headers).Result;

        /// <inheritdoc cref="Post(string, object, Dictionary{string, string})"/>
        public KatharsisResponse Post(string resource, object body)
            => RequestAsync(resource, Method.Post, body).Result;

        /// <summary>
        /// Sends a POST HTTP request to specified client base's URL and resource.
        /// </summary>
        /// <param name="resource">Web API's resource.</param>
        /// <param name="body">Body object that will be serialized by <see cref="Serializer"/> object and attached to HTTP's request content.</param>
        /// <param name="headers">Additional headers for this HTTP request.</param>
        /// <returns>An HTTP response object.</returns>
        public KatharsisResponse Post(string resource, object body, Dictionary<string, string> headers)
            => RequestAsync(resource, Method.Post, body, headers).Result;

        /// <inheritdoc cref="PostAsync(string, object, Dictionary{string, string})"/>
        public async Task<KatharsisResponse> PostAsync(string resource)
            => await RequestAsync(resource, Method.Post);

        /// <inheritdoc cref="PostAsync(string, object, Dictionary{string, string})"/>
        public async Task<KatharsisResponse> PostAsync(string resource, Dictionary<string, string> headers)
            => await RequestAsync(resource, Method.Post, headers);

        /// <inheritdoc cref="PostAsync(string, object, Dictionary{string, string})"/>
        public async Task<KatharsisResponse> PostAsync(string resource, object body)
            => await RequestAsync(resource, Method.Post, body);

        /// <summary>
        /// Sends a POST HTTP request to specified client base's URL and resource as asynchronous operation.
        /// </summary>
        /// <param name="resource">Web API's resource.</param>
        /// <param name="body">Body object that will be serialized by <see cref="Serializer"/> object and attached to HTTP's request content.</param>
        /// <param name="headers">Additional headers for this HTTP request.</param>
        /// <returns>The task object representing asynchronous operation.</returns>
        public async Task<KatharsisResponse> PostAsync(string resource, object body, Dictionary<string, string> headers)
            => await RequestAsync(resource, Method.Post, body, headers);

        /// <inheritdoc cref="Put(string, object, Dictionary{string, string})"/>
        public KatharsisResponse Put(string resource)
            => RequestAsync(resource, Method.Put).Result;

        /// <inheritdoc cref="Put(string, object, Dictionary{string, string})"/>
        public KatharsisResponse Put(string resource, Dictionary<string, string> headers)
            => RequestAsync(resource, Method.Put, headers).Result;

        /// <inheritdoc cref="Put(string, object, Dictionary{string, string})"/>
        public KatharsisResponse Put(string resource, object body)
            => RequestAsync(resource, Method.Put, body).Result;

        /// <summary>
        /// Sends a PUT HTTP request to specified client base's URL and resource.
        /// </summary>
        /// <param name="resource">Web API's resource.</param>
        /// <param name="body">Body object that will be serialized by <see cref="Serializer"/> object and attached to HTTP's request content.</param>
        /// <param name="headers">Additional headers for this HTTP request.</param>
        /// <returns>An HTTP response object.</returns>
        public KatharsisResponse Put(string resource, object body, Dictionary<string, string> headers)
            => RequestAsync(resource, Method.Put, body, headers).Result;

        /// <inheritdoc cref="PutAsync(string, object, Dictionary{string, string})"/>
        public async Task<KatharsisResponse> PutAsync(string resource)
            => await RequestAsync(resource, Method.Put);

        /// <inheritdoc cref="PutAsync(string, object, Dictionary{string, string})"/>
        public async Task<KatharsisResponse> PutAsync(string resource, Dictionary<string, string> headers)
            => await RequestAsync(resource, Method.Put, headers);

        /// <inheritdoc cref="PutAsync(string, object, Dictionary{string, string})"/>
        public async Task<KatharsisResponse> PutAsync(string resource, object body)
            => await RequestAsync(resource, Method.Put, body);

        /// <summary>
        /// Sends a PUT HTTP request to specified client base's URL and resource as asynchronous operation.
        /// </summary>
        /// <param name="resource">Web API's resource.</param>
        /// <param name="body">Body object that will be serialized by <see cref="Serializer"/> object and attached to HTTP's request content.</param>
        /// <param name="headers">Additional headers for this HTTP request.</param>
        /// <returns>The task object representing asynchronous operation.</returns>
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

using Katharsis.Http.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Katharsis.Http
{
    /// <summary>
    /// A class that holds request informations.
    /// </summary>
    public class KatharsisRequest
    {
        private string uri;
        /// <summary>
        /// The URL address that request has been sent to.
        /// </summary>
        public string Uri 
        { 
            get => uri;
            internal set 
            {
                uri = value;
                HttpRequestMessage.RequestUri = new Uri(value);
            } 
        }

        private object content;
        /// <summary>
        /// The body content that request contained.
        /// </summary>
        public object Content 
        { 
            get => content;
            internal set 
            {
                content = value;
                HttpRequestMessage.Content = GetContent(value);
            } 
        }

        private string contentType;
        internal string ContentType
        {
            get => contentType;
            set => contentType = value;
        }

        private Dictionary<string, string> headers;
        /// <summary>
        /// Headers attached to HTTP request.
        /// </summary>
        public Dictionary<string, string> Headers 
        { 
            get => headers;
            internal set 
            {
                headers = value;

                if (value == null)
                    return;

                foreach (var header in value)
                {
                    if(header.Key == "Content-Type")
                    {
                        ContentType = header.Value;
                        continue;
                    }

                    HttpRequestMessage.Headers.Add(header.Key, header.Value);
                }
            }
        }

        private Method method;
        /// <summary>
        /// The HTTP request method (eg. GET, POST, PUT). 
        /// </summary>
        public Method Method 
        { 
            get => method; 
            internal set 
            {
                method = value;
                HttpRequestMessage.Method = GetMethod(value);
            } 
        }

        internal readonly ISerializer Serializer;

        internal readonly HttpRequestMessage HttpRequestMessage;

        internal KatharsisRequest()
        {
            Headers = new Dictionary<string, string>();
        }

        internal KatharsisRequest(ISerializer serializer) : this()
        {
            HttpRequestMessage = new HttpRequestMessage();
            Serializer = serializer;
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

        private HttpContent GetContent(object content) => ContentType switch
        {
            Constants.ContentType.APPLICATION_X_WWW_FORM_URLENCODED => new FormUrlEncodedContent((Serializer as IDeserializer).Deserialize<Dictionary<string, string>>(Serializer.Serialize(content))),
            Constants.ContentType.APPLICATION_JSON => new StringContent(Serializer.Serialize(content)),
            _ => new StringContent(Serializer.Serialize(content))
        };
    }
}

using System.Net;
using System.Reflection;

namespace Katharsis.Http
{
    public class KatharsisResponse
    {
        /// <summary>
        /// The response content.
        /// </summary>
        public string Content { get; internal set; }

        /// <summary>
        /// Bytes content.
        /// </summary>
        public byte[] ContentBytes { get; set; }

        /// <summary>
        /// An exception that occured during method execution.
        /// </summary>
        public KatharsisHttpException Exception { get; internal set; }

        /// <summary>
        /// An object containing HTTP request informations after calling it.
        /// </summary>
        public KatharsisRequest Request { get; internal set; }

        /// <summary>
        /// HTTP response status.
        /// </summary>
        public Status Status { get; internal set; }
    }

    public class KatharsisResponse<T> : KatharsisResponse
    {
        public T GenericContent { get; set; }
        public KatharsisResponse(KatharsisResponse response)
        {
            CopyProperties(response, this);
        }

        private void CopyProperties(object source, object destination)
        {
            if (source == null || destination == null)
                return;

            Type sourceType = source.GetType();
            Type destinationType = destination.GetType();

            PropertyInfo[] sourceProperties = sourceType.GetProperties();
            foreach (var sourceProperty in sourceProperties)
            {
                if (sourceProperty.CanRead)
                {
                    PropertyInfo destinationProperty = destinationType.GetProperty(sourceProperty.Name);
                    if (destinationProperty != null && destinationProperty.CanWrite)
                    {
                        object value = sourceProperty.GetValue(source);
                        destinationProperty.SetValue(destination, value);
                    }
                }
            }
        }
    }

    public class Status
    {
        /// <summary>
        /// HTTP response status code represented as <see cref="int"/> value.
        /// </summary>
        public int Code { get; internal set; }

        /// <summary>
        /// HTTP response status code represented as <see cref="HttpStatusCode"/> value.
        /// </summary>
        public HttpStatusCode HttpStatus { get; internal set; }

        internal Status(HttpStatusCode httpStatusCode)
        {
            Code = (int)httpStatusCode;
            HttpStatus = httpStatusCode;
        }

        public override string ToString()
        {
            return Code + " " + HttpStatus;
        }
    }
}

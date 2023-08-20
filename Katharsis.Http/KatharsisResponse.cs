using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Katharsis.Http
{
    public class KatharsisResponse
    {
        /// <summary>
        /// The response content.
        /// </summary>
        public string Content { get; internal set; }

        /// <summary>
        /// An exception that occured during method execution.
        /// </summary>
        public KatharsisHttpException Exception { get; internal set; }

        /// <summary>
        /// HTTP response status.
        /// </summary>
        public Status Status { get; internal set; }
    }

    public class KatharsisResponse<T> : KatharsisResponse
    {
        public T GenericContent { get; set; }
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

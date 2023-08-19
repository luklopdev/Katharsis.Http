using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Katharsis.Http
{
    /// <summary>
    /// Exception handler for Katharsis HTTP errors. 
    /// </summary>
    public class KatharsisHttpException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="KatharsisHttpException"/> class.
        /// </summary>
        /// <param name="message"><inheritdoc cref="Exception(string)"/></param>
        public KatharsisHttpException(string message) : base(message)
        {
            
        }

        /// <inheritdoc cref="KatharsisHttpException(string)"/>
        /// <param name="message"><inheritdoc cref="Exception(string)"/></param>
        /// <param name="innerException"><inheritdoc cref="Exception(string, Exception)"/></param>
        public KatharsisHttpException(string message, Exception innerException) : base(message, innerException)
        {
            
        }
    }
}

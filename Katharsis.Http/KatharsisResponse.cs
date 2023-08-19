using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Katharsis.Http
{
    public class KatharsisResponse
    {
        public string Content { get; set; }
        public KatharsisHttpException Exception { get; internal set; }
    }

    public class KatharsisResponse<T> : KatharsisResponse
    {
        public T GenericContent { get; set; }
    }
}

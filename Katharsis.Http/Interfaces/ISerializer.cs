using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Katharsis.Http.Interfaces
{
    public interface ISerializer
    {
        public string Serialize(object body);
    }
}

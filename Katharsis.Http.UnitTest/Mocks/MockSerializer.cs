using Katharsis.Http.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Katharsis.Http.UnitTests.Mocks
{
    internal class MockSerializer : ISerializer
    {
        public string Serialize(object body)
        {
            return JsonConvert.SerializeObject(body);
        }
    }
}

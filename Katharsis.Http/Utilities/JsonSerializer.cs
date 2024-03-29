﻿using Katharsis.Http.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Katharsis.Http.Utilities
{
    internal class JsonSerializer : ISerializer
    {
        public string Serialize(object body) 
        {
            if (body == null)
                throw new ArgumentNullException(nameof(body));

            return JsonConvert.SerializeObject(body);
        }
    }
}

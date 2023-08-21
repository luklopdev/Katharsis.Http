using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Katharsis.Http.UnitTests
{
    [TestFixture]
    public class KatharsisClientTests
    {
        private KatharsisClient _client;

        [SetUp]
        public void Setup()
        {
            _client = new KatharsisClient();
        }

    }
}

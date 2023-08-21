using Katharsis.Http.UnitTests.Mocks;
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

        [Test]
        public void KatharsisClientConstructor_ShouldInitializeObject()
        {
            Assert.AreEqual(_client.URL, string.Empty);
            Assert.IsTrue(_client.Serializer != null);
            Assert.IsTrue(_client.Headers != null);
            Assert.IsTrue(_client.Headers.Count == 0);
        }

        [Test]
        public void KatharsisClientConstructor_ShouldInitializeObjectWithURL()
        {
            const string URL = "https://google.com";
            _client = new KatharsisClient(URL);

            Assert.AreEqual(URL, _client.URL);
            Assert.IsTrue(_client.Serializer != null);
            Assert.IsTrue(_client.Headers != null);
            Assert.IsTrue(_client.Headers.Count == 0);
        }

        [Test]
        public void KatharsisClientConstructor_ShouldInitializeObjectWithURLAndSerializer()
        {
            const string URL = "https://google.com";
            var serializer = new MockSerializer();

            _client = new KatharsisClient(URL, serializer);

            Assert.AreEqual(URL, _client.URL);
            Assert.IsTrue(_client.Serializer != null);
            Assert.IsTrue(_client.Serializer is MockSerializer);
            Assert.IsTrue(_client.Headers != null);
            Assert.IsTrue(_client.Headers.Count == 0);
        }

        [Test]
        public void KatharsisClientConstructor_ShouldInitializeObjectWithURLAndDefaultHeaders()
        {
            const string URL = "https://google.com";

            string header1Key = "key1";
            string header1Value = "value1";

            string header2Key = "key2";
            string header2Value = "value2";

            string header3Key = "key3";
            string header3Value = "value3";


            Dictionary<string, string> defaultHeaders = new Dictionary<string, string>()
            {
                [header1Key] = header1Value,         
                [header2Key] = header2Value,         
                [header3Key] = header3Value,         
            };

            _client = new KatharsisClient(URL, defaultHeaders);


            Assert.AreEqual(URL, _client.URL);
            Assert.IsTrue(_client.Serializer != null);
            Assert.IsTrue(_client.Headers != null);
            Assert.IsTrue(_client.Headers.Count == 3);
            Assert.IsTrue(_client.Headers.ContainsKey(header1Key));
            Assert.IsTrue(_client.Headers.ContainsKey(header2Key));
            Assert.IsTrue(_client.Headers.ContainsKey(header3Key));
            Assert.AreEqual(_client.Headers[header1Key], header1Value);
            Assert.AreEqual(_client.Headers[header2Key], header2Value);
            Assert.AreEqual(_client.Headers[header3Key], header3Value);
        }
    }
}

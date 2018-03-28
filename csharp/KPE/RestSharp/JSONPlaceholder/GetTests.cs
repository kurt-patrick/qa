using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace KPE.RestSharp.JSONPlaceholder
{
    [TestFixture(Description = "GET Requests")]
    public class GetTests
    {
        RestClient _restClient = null;

        [OneTimeSetUp()]
        public void OneTimeSetUp()
        {
            _restClient = new RestClient("http://jsonplaceholder.typicode.com");
        }

        [Test()]
        public void GetUser()
        {
            var request = new RestRequest("users/1");
            var response = _restClient.Execute<ResponseObjects.User>(request);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsNotNull(response.Data);
            Assert.AreEqual(1, response.Data.id);
            Assert.AreEqual("Leanne Graham", response.Data.name);

        }

        [Test()]
        public void GetUsers()
        {
            var request = new RestRequest("users");
            var response = _restClient.Execute<List<ResponseObjects.User>>(request);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsNotNull(response.Data);
            Assert.AreEqual(10, response.Data.Count);
            Assert.AreEqual(1, response.Data.First().id);
            Assert.AreEqual("Leanne Graham", response.Data.First().name);
            Assert.AreEqual(10, response.Data.Last().id);
            Assert.AreEqual("Clementina DuBuque", response.Data.Last().name);

        }

        [Test()]
        public void GetPost()
        {
            var request = new RestRequest("posts/1");
            var response = _restClient.Execute<ResponseObjects.Post>(request);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsNotNull(response.Data);

            var jsonObj = response.Data;
            Assert.AreEqual(1, jsonObj.userId);
            Assert.AreEqual(1, jsonObj.id);
            Assert.AreEqual("sunt aut facere repellat provident occaecati excepturi optio reprehenderit", jsonObj.title);
            Assert.AreEqual("quia et suscipit\nsuscipit recusandae consequuntur expedita et cum\nreprehenderit molestiae ut ut quas totam\nnostrum rerum est autem sunt rem eveniet architecto", jsonObj.body);

        }

    }
}

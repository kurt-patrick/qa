using NUnit.Framework;
using RestSharp;
using System.Net;

namespace KPE.RestSharp.JSONPlaceholder.Tests
{
    [TestFixture(Description = "POST Requests")]
    public class PostTests
    {
        RestClient _restClient = null;

        [OneTimeSetUp()]
        public void OneTimeSetUp()
        {
            _restClient = new RestClient("http://jsonplaceholder.typicode.com");
        }

        [Test()]
        public void ValidPost()
        {
            var request = new RestRequest("posts", Method.POST);
            var postBody = new ResponseObjects.Post() { body = "body", id = 101, title = "title", userId = 1 };
            request.AddJsonBody(postBody);

            var response = _restClient.Execute<ResponseObjects.Post>(request);

            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
            Assert.IsNotNull(response.Data);
            Assert.AreEqual(postBody.body, response.Data.body);
            Assert.AreEqual(postBody.id, response.Data.id);
            Assert.AreEqual(postBody.title, response.Data.title);
            Assert.AreEqual(postBody.userId, response.Data.userId);

        }

        [Test()]
        public void InvalidPost_ExistingId()
        {
            var request = new RestRequest("posts", Method.POST);
            request.AddJsonBody(new ResponseObjects.Post() { body = "body", id = 1, title = "title", userId = 1 });

            var response = _restClient.Execute<ResponseObjects.PostResponse>(request);

            Assert.AreEqual(HttpStatusCode.InternalServerError, response.StatusCode);
            Assert.IsNull(response.Data);

        }

    }
}

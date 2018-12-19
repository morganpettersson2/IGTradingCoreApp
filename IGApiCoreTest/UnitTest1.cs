using DalSoft.RestClient;
using IGApi;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace IGApiCoreTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestLogin()
        {
            HttpClient client = new HttpClient();
            var response = new HttpResponseMessage();
            string _identifier = "morganpettersson";
            string _password = "Woodo2017";
            string _apiKey = "439c48c0d6c72558b455cbaf8e6281ac1d94a82a";
            string version = "2";
            ConversationContext _conversationContext = new ConversationContext(null, null, _apiKey);
            var bodyInput = new AuthenticationRequest { identifier = _identifier, password = _password };
            StringContent scontent = new StringContent(JsonConvert.SerializeObject(bodyInput));
            scontent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            client.DefaultRequestHeaders.Add("X-IG-API-KEY", _conversationContext.apiKey);
            client.DefaultRequestHeaders.Add("VERSION", version);
            var postTask = client.PostAsync("https://demo-api.ig.com/gateway/deal/session", scontent);
            response = postTask.Result;


            Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);
        }

    }

}




using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace IGApi
{
    public class IgRestService
    {
        private HttpClient _client;

        public IgRestService()
        {
            _client = new HttpClient();
        }

        public async Task <JObject> Authenticate(ConversationContext conversationContext, string uri, string identifier, string password)
        {
            var response = new HttpResponseMessage();
            JObject oJson = new JObject();
            try
            {
                var bodyInput = new AuthenticationRequest { identifier = identifier, password = password };
                StringContent scontent = new StringContent(JsonConvert.SerializeObject(bodyInput));
                scontent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                _client.DefaultRequestHeaders.Add("X-IG-API-KEY", conversationContext.apiKey);
                _client.DefaultRequestHeaders.Add("VERSION", "2");
                var postTask = _client.PostAsync(uri, scontent);
                response = postTask.Result;

                if (response != null)
                {
                    ParseHeaders(conversationContext, response.Headers);
                    response.StatusCode = response.StatusCode;
                    string content = "";
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        content = await response.Content.ReadAsStringAsync();
                    }
                    if (content != "")
                    {
                        try
                        {
                            oJson = JObject.Parse(content);
                        }
                        catch (Exception ex)
                        {
                            //eventDispatcher.addEventMessage(ex.Message);
                        }
                        _client.Dispose();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
       
            return oJson;
        }

        private void ParseHeaders(ConversationContext conversationContext, HttpResponseHeaders headers)
        {
            if (conversationContext != null)
            {
                foreach (var header in headers)
                {
                    if (header.Key == "CST")
                    {
                        conversationContext.cst = header.Value.First();
                    }
                    if (header.Key.Equals("X-SECURITY-TOKEN"))
                    {
                        conversationContext.xSecurityToken = header.Value.First();
                    }
                }
            }
        }
    }
}
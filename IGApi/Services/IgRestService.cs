using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
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

        public async Task<IgResponse<T>> RestfulService<T>(string uri, HttpMethod method, string version, ConversationContext conversationContext, Object bodyInput = null)
        {
            StringContent scontent;

            var localVar = new IgResponse<T> { Response = default(T), StatusCode = HttpStatusCode.OK };

            if (bodyInput != null)
            {
                scontent = new StringContent(JsonConvert.SerializeObject(bodyInput));
                scontent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            }
            else
            {
                scontent = null;
            }
            var client = new HttpClient();
            SetDefaultRequestHeaders(client, conversationContext, version);

            var response = new HttpResponseMessage();

            string content = null;

            switch (method.Method)
            {
                case "POST":
                    var myPostTask = client.PostAsync(uri, scontent);
                    response = myPostTask.Result;
                    break;

                case "GET":
                    var myGetTask = client.GetAsync(uri);
                    response = myGetTask.Result;
                    break;

                case "PUT":
                    var myPutTask = client.PutAsync(uri, scontent);
                    response = myPutTask.Result;
                    break;

                case "DELETE":
                    Task<HttpResponseMessage> myDeleteTask;

                    if (scontent != null)
                    {
                        scontent.Headers.Add("_method", "DELETE");
                        myDeleteTask = client.PostAsync(uri, scontent);
                    }
                    else
                    {
                        myDeleteTask = client.DeleteAsync(uri);
                    }

                    response = myDeleteTask.Result;
                    break;
            }

            if (response != null)
            {
                ParseHeaders(conversationContext, response.Headers);
                localVar.StatusCode = response.StatusCode;
                //eventDispatcher.addEventMessage(method.Method + " request to " + _baseUrl + uri + " returned status " + localVar.StatusCode);
                if (localVar.StatusCode == HttpStatusCode.OK)
                {
                    content = await response.Content.ReadAsStringAsync();
                }
            }

            if (content != null)
            {
                var jss = new JsonSerializerSettings();
                jss.Converters.Add(new StringEnumConverter());
                jss.MissingMemberHandling = MissingMemberHandling.Ignore;
                jss.FloatFormatHandling = FloatFormatHandling.String;
                jss.NullValueHandling = NullValueHandling.Ignore;
                //jss.Error += DeserializationError;
                client.Dispose();

                try
                {
                    var json = JsonConvert.DeserializeObject<T>(content, jss);
                    localVar.Response = json;
                }
                catch (Exception ex)
                {
                   // eventDispatcher.addEventMessage(ex.Message);
                }
            }
            return localVar;
        }

        public void SetDefaultRequestHeaders(HttpClient client, ConversationContext conversationContext, string version)
        {
            if (conversationContext != null)
            {
                if (conversationContext.apiKey != null)
                {
                    client.DefaultRequestHeaders.Add("X-IG-API-KEY", conversationContext.apiKey);
                }
                if (conversationContext.cst != null)
                {
                    client.DefaultRequestHeaders.Add("CST", conversationContext.cst);
                }
                if (conversationContext.xSecurityToken != null)
                {
                    client.DefaultRequestHeaders.Add("X-SECURITY-TOKEN", conversationContext.xSecurityToken);
                }
                client.DefaultRequestHeaders.Add("VERSION", version);

            }
            //This only works for version 1 !!!           
            //client.DefaultRequestHeaders.TryAddWithoutValidation("version", version ?? "1");
        }
    }
}
using System;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace IGApi
{
    public class IgRestService
    {
        private static string _baseUrl = "https://demo-api.ig.com";
        private static readonly string _identifier = ConfigurationManager.AppSettings["identifier"];
        private static readonly string _password = ConfigurationManager.AppSettings["password"];
        private static readonly string env = ConfigurationManager.AppSettings["env"];
        private static string _apiKey;
        private HttpClient _client;
        
        public IgRestService()
        {
            _baseUrl = String.Format("https://{0}api.ig.com", env == "live" ? "" : env + "-");
            _client = new HttpClient();

            if (env == "demo")
            {
                _apiKey = ConfigurationManager.AppSettings["apiKeyDemo"];
            }
            else
            {
                _apiKey = ConfigurationManager.AppSettings["apiKeyProd"];
            }


        }

        public async Task<IgResponse<object>> LoginOAuth(string uri = "https://demo-api.ig.com/gateway/deal/session")
        {
            var localVar = new IgResponse<object> { Response = default(object), StatusCode = HttpStatusCode.OK };
            var response = new HttpResponseMessage();
            string version = "3";
            _apiKey = ConfigurationManager.AppSettings["apiKeyDemo"];
            var _identifier = "morganpettersson";
            var _password = "Woodo2017";
            ConversationContext conversationContext = new ConversationContext(null, null, "439c48c0d6c72558b455cbaf8e6281ac1d94a82a");
            var bodyInput = new AuthenticationRequest { identifier = _identifier, password = _password };
            StringContent scontent = new StringContent(JsonConvert.SerializeObject(bodyInput));
            scontent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            _client.DefaultRequestHeaders.Add("X-IG-API-KEY", conversationContext.apiKey);
            _client.DefaultRequestHeaders.Add("VERSION", version);
            var postTask = _client.PostAsync(uri, scontent);

            response=  postTask.Result;

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
                    var jss = new JsonSerializerSettings();
                    jss.Converters.Add(new StringEnumConverter());
                    jss.MissingMemberHandling = MissingMemberHandling.Ignore;
                    jss.FloatFormatHandling = FloatFormatHandling.String;
                    jss.NullValueHandling = NullValueHandling.Ignore;
                    //jss.Error += DeserializationError;
                    _client.Dispose();

                    try
                    {

                        var json = JsonConvert.DeserializeObject<object>(content, jss);
                        localVar.Response = json;
                    }
                    catch (Exception ex)
                    {
                        //eventDispatcher.addEventMessage(ex.Message);
                    }
                }
            }
            return localVar;
        }

        public void ParseHeaders(ConversationContext conversationContext, HttpResponseHeaders headers)
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
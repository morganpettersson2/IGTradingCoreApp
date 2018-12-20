using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using IGApi.Model.dto.endpoint.accountbalance;
using IGApi.Model.dto.endpoint.auth.session;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

namespace IGApi.Services
{
    public class IgRestApiClient
    {
        private static ConversationContext _conversationContext;
        private static readonly IgRestService IgRestService = new IgRestService();

        public ConversationContext GetConversationContext()
        {
            return _conversationContext;
        }

        public async Task<IgResponse<AuthenticationResponse>> Authenticate(ConversationContext conversationContext,string uri, string identifier, string password)
        {
            var localRespVar = new IgResponse<AuthenticationResponse> { Response = default(AuthenticationResponse), StatusCode = HttpStatusCode.OK };
            try
            {
                _conversationContext = conversationContext;
                JObject joj = await IgRestService.Authenticate(_conversationContext, uri, identifier, password);
                AuthenticationResponse json = JsonConvert(joj);
                localRespVar.Response = json;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return localRespVar;
        }
        public async Task<IgResponse<AccountDetailsResponse>> AccountBalance(string uri)
        {
            return await IgRestService.RestfulService<AccountDetailsResponse>(uri, HttpMethod.Get, "1", _conversationContext);
        }


        private static AuthenticationResponse JsonConvert(JObject joj)
        {
            AuthenticationResponse tempAu;
            try
            {
                var jss = new JsonSerializerSettings();
                jss.Converters.Add(new StringEnumConverter());
                jss.MissingMemberHandling = MissingMemberHandling.Ignore;
                jss.FloatFormatHandling = FloatFormatHandling.String;
                jss.NullValueHandling = NullValueHandling.Ignore;
                var json = Newtonsoft.Json.JsonConvert.DeserializeObject<AuthenticationResponse>(joj.ToString(), jss);
                tempAu = json;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return tempAu;
        }

        //public async Task<IgResponse<EncryptionKeyResponse>> fetchEncryptionKey()
        //{
        //    return await _igRestService.FetchEncryptionKey<EncryptionKeyResponse>("/gateway/deal/session/encryptionKey", HttpMethod.Get, "1", _conversationContext);
        //}
    }
}
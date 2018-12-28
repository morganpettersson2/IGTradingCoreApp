using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using IGApi.Model.dto.endpoint.accountbalance;
using IGApi.Model.dto.endpoint.auth.session;
using IGApi.Model.dto.endpoint.browse;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using IGApi.Model.dto.endpoint.prices.v2;

namespace IGApi.Services
{
    public class IgRestApiClient
    {
        private static ConversationContext _conversationContext;
        private static readonly IgRestService IgRestService = new IgRestService();
        //var identifier = ConfigurationManager.AppSettings["identifier"];
        //var password = ConfigurationManager.AppSettings["password"];
        //var apiKey = ConfigurationManager.AppSettings["apiKeyDemo"];
        //private static readonly string _uri = ConfigurationManager.AppSettings["uri"];
        private static readonly string _uri = "https://demo-api.ig.com/gateway/deal/";

        public ConversationContext GetConversationContext()
        {
            if (_conversationContext != null)
                return _conversationContext;


            return null;
        }

        public async Task<IgResponse<AuthenticationResponse>> Authenticate(ConversationContext conversationContext, string identifier, string password)
        {
            var localRespVar = new IgResponse<AuthenticationResponse> { Response = default(AuthenticationResponse), StatusCode = HttpStatusCode.OK };
            try
            {
                _conversationContext = conversationContext;
                JObject joj = await IgRestService.Authenticate(_conversationContext, _uri + "session", identifier, password);
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
        public async Task<IgResponse<AccountDetailsResponse>> AccountBalance()
        {
            return await IgRestService.RestfulService<AccountDetailsResponse>(_uri + "accounts", HttpMethod.Get, "1", _conversationContext);
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
        public async Task<IgResponse<AccountDetailsResponse>> LogOut()
        {
            return await IgRestService.RestfulService<AccountDetailsResponse>(_uri + "session", HttpMethod.Get, "1", _conversationContext);
        }

        public async Task<IgResponse<PriceList>> priceSearchByDate(string epic, string resolution, string startDate, string endDate, string pageNumber, string pageSize, string maxPricePoints)
        {
            return await IgRestService.RestfulService<PriceList>(_uri + "prices/" + epic + "?resolution=" + resolution + "&from=" + startDate + "&to=" + endDate + "&max=" + maxPricePoints + "&pageSize=" + pageSize + "&pageNumber=" + pageNumber, HttpMethod.Get, "3", _conversationContext);
        }
        public async Task<IgResponse<BrowseMarketsResponse>> browseRoot()
        {
            return await IgRestService.RestfulService<BrowseMarketsResponse>(_uri + "marketnavigation", HttpMethod.Get, "1", _conversationContext);
        }
        public async Task<List<IG.Domain.HierarchyNode>> GetOmx30Stocks()
        {
            List<IG.Domain.HierarchyNode> hierarchyNodeList = new List<IG.Domain.HierarchyNode>();
            var rootNodeList = await IgRestService.RestfulService<BrowseMarketsResponse>(_uri + "marketnavigation/169465", HttpMethod.Get, "1", _conversationContext);
            foreach (var rootNode in rootNodeList.Response.nodes)
            {
                var letterNode = await IgRestService.RestfulService<BrowseMarketsResponse>(_uri + "marketnavigation/" + rootNode.id, HttpMethod.Get, "1", _conversationContext);
                foreach (var stockNode in letterNode.Response.nodes)
                {
                    hierarchyNodeList.Add(new IG.Domain.HierarchyNode { Id = Convert.ToInt64(stockNode.id), Name = stockNode.name });
                }
            }
            return hierarchyNodeList;
        }
        public async Task<IgResponse<BrowseMarketsResponse>> GetHierarchyMarket(string instrumentId)
        {
            
            var node = await IgRestService.RestfulService<BrowseMarketsResponse>(_uri + "marketnavigation/" + instrumentId, HttpMethod.Get, "1", _conversationContext);
            
            return node;
        }

    }
}

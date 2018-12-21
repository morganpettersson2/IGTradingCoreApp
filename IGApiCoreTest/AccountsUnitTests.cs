using DalSoft.RestClient;
using IGApi;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using IGApi.Model.dto.endpoint.accountbalance;
using IGApi.Model.dto.endpoint.auth.session;
using IGApi.Services;

namespace IGApiCoreTest
{
    [TestClass]
    public class AccountsUnitTests
    {
       
  

        private static readonly IgRestApiClient IgRestService = new IgRestApiClient();
        
        private static readonly string _identifier = "morganpettersson";
        private static readonly string _password = "Woodo2017";
        private static readonly string _apiKey = "439c48c0d6c72558b455cbaf8e6281ac1d94a82a";
        private static ConversationContext _conversationContext = new ConversationContext("", "", _apiKey);

        [TestMethod]
        public async Task TestLoginExpectNoErrors()
        {
            var response = await IgRestService.Authenticate(_conversationContext, _identifier,_password);
            _conversationContext = IgRestService.GetConversationContext();
            Assert.IsTrue(response.StatusCode == HttpStatusCode.OK && _conversationContext.xSecurityToken.Length>0);
        }

        [TestMethod]
        public async Task GetAccountInfoExpectNuErrors()
        {
            if (IgRestService.GetConversationContext() == null )
                await IgRestService.Authenticate(_conversationContext,  _identifier, _password);

            var response = await IgRestService.AccountBalance();

            Assert.IsTrue(response.Response.accounts.Count >0); 
        }

        [TestMethod]
        public async Task LogInLogOutExpectNoErrors()
        {
            if (IgRestService.GetConversationContext() == null)
                await IgRestService.Authenticate(_conversationContext, _identifier, _password);

            var response = await IgRestService.LogOut();

            Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);
            
        }

    }



}



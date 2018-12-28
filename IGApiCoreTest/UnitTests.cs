
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using IGApi;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using IG.Domain;
using IGApi.Services;


namespace IGApiCoreTest
{
    [TestClass]
    public class UnitTests
    {



        private static readonly IgRestApiClient IgRestService = new IgRestApiClient();

        private static readonly string _identifier = "morganpettersson";
        private static readonly string _password = "Woodo2017";
        private static readonly string _apiKey = "439c48c0d6c72558b455cbaf8e6281ac1d94a82a";
        private static ConversationContext _conversationContext = new ConversationContext("", "", _apiKey);

        [TestMethod]
        public async Task TestLoginExpectNoErrors()
        {
            var response = await IgRestService.Authenticate(_conversationContext, _identifier, _password);
            _conversationContext = IgRestService.GetConversationContext();
            Assert.IsTrue(response.StatusCode == HttpStatusCode.OK && _conversationContext.xSecurityToken.Length > 0);
        }

        [TestMethod]
        public async Task GetAccountInfoExpectNuErrors()
        {
            if (IgRestService.GetConversationContext() == null)
                await IgRestService.Authenticate(_conversationContext, _identifier, _password);

            var response = await IgRestService.AccountBalance();

            Assert.IsTrue(response.Response.accounts.Count > 0);
        }

        [TestMethod]
        public async Task LogInLogOutExpectNoErrors()
        {
            if (IgRestService.GetConversationContext() == null)
                await IgRestService.Authenticate(_conversationContext, _identifier, _password);

            var response = await IgRestService.LogOut();

            Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);

        }

        [TestMethod]
        public async Task GetHistoricalDataForOMX30ExpectNoErrors()
        {
            string epic = "IX.D.OMX.IFM.IP";
            string resolution = "DAY";
            string startDate = "2018-01-01T00:00:00";
            string endDate = "2018-12-24T00:00:00";
            string maxPricePoints = "10";
            string pageSize = "20";
            string pageNr = "1";

            if (IgRestService.GetConversationContext() == null)
                await IgRestService.Authenticate(_conversationContext, _identifier, _password);

            var response = IgRestService.priceSearchByDate(epic, resolution, startDate, endDate, pageNr, pageSize,
                maxPricePoints);

            foreach (var price in response.Result.Response.prices)
            {
                //Spara ner till db
            }

            Assert.IsNotNull(response.Result.Response.prices);
        }


        /// <summary>
        /// Hämtar alla instrument som IG har
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task BrowswRoot()
        {
            if (IgRestService.GetConversationContext() == null)
                await IgRestService.Authenticate(_conversationContext, _identifier, _password);

            var response = await IgRestService.browseRoot();

            foreach (var nodes in response.Response.nodes)
            {
                //Spara ner till db
            }

            Assert.IsNotNull(response.Response.nodes);
        }

        /// <summary>
        /// Hämtar alla aktier under nod Omx30 som IG har
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task GetOmxStocksExpectNoErrors()
        {
            if (IgRestService.GetConversationContext() == null)
                await IgRestService.Authenticate(_conversationContext, _identifier, _password);

            var responseList = await IgRestService.GetOmx30Stocks();
            Assert.IsTrue(responseList.Count>0);
        }

        [TestMethod]
        public async Task GetOmxStocksFillHierarchyNodeTableExpectNoErrors()
        {
            if (IgRestService.GetConversationContext() == null)
                await IgRestService.Authenticate(_conversationContext, _identifier, _password);

            var responseList = IgRestService.GetOmx30Stocks();

            IG.Data.DataOperations dataOperations = new IG.Data.DataOperations();

            dataOperations.InsertHierarchyNodes(responseList.Result);
            Assert.IsNotNull(responseList);
        }

        [TestMethod]
        public void FillTimeFrameTableExpectNoErrors()
        {
            IG.Data.DataOperations dataOperations = new IG.Data.DataOperations();
            dataOperations.InsertTimeFrameData();
        }

        [TestMethod]
        public async Task GetOmxStocksFillHierarchyMarketTableExpectNoErrors()
        {
            if (IgRestService.GetConversationContext() == null)
                await IgRestService.Authenticate(_conversationContext, _identifier, _password);
            
            List<HierarchyMarket> marketList = new List<HierarchyMarket>();
            IG.Data.DataOperations dataOperations = new IG.Data.DataOperations();
            dataOperations.DeleteAllHierarchyNodes();
            List<HierarchyNode> nodeList = dataOperations.GetAllHierarchyNodes();
            foreach (var node in nodeList)
            {
                if (node.Id > 0)
                {
                    try
                    {
                        var instrumentNode = await IgRestService.GetHierarchyMarket(node.Id.ToString());
                        if (instrumentNode != null && instrumentNode.StatusCode == HttpStatusCode.OK)
                        {
                            System.Diagnostics.Debug.WriteLine(instrumentNode.Response.markets[0].instrumentName);
                            HierarchyMarket temp = new HierarchyMarket();
                            if (instrumentNode.Response.markets[0].bid != null)
                                temp.Bid = instrumentNode.Response.markets[0].bid;
                            if (instrumentNode.Response.markets[0].delayTime != null)
                                temp.DelayTime = instrumentNode.Response.markets[0].delayTime;
                            if (instrumentNode.Response.markets[0].epic != null)
                                temp.Epic = instrumentNode.Response.markets[0].epic;
                            if (instrumentNode.Response.markets[0].expiry != null)
                                temp.Expiry = instrumentNode.Response.markets[0].expiry;
                            if (instrumentNode.Response.markets[0].high != null)
                                temp.High = instrumentNode.Response.markets[0].high;
                            if (instrumentNode.Response.markets[0].instrumentName != null)
                                temp.InstrumentName = instrumentNode.Response.markets[0].instrumentName;
                            if (instrumentNode.Response.markets[0].instrumentType != null)
                                temp.InstrumentType = instrumentNode.Response.markets[0].instrumentType;
                            if (instrumentNode.Response.markets[0].lotSize != null)
                                temp.LotSize = instrumentNode.Response.markets[0].lotSize;
                            if (instrumentNode.Response.markets[0].low != null)
                                temp.Low = instrumentNode.Response.markets[0].low;
                            if (instrumentNode.Response.markets[0].marketStatus != null)
                                temp.MarketStatus = instrumentNode.Response.markets[0].marketStatus;
                            if (instrumentNode.Response.markets[0].netChange != null)
                                temp.NetChange = instrumentNode.Response.markets[0].netChange;
                            if (instrumentNode.Response.markets[0].offer != null)
                                temp.Offer = instrumentNode.Response.markets[0].offer;
                            if (instrumentNode.Response.markets[0].otcTradeable != null)
                                temp.OtcTradeable = instrumentNode.Response.markets[0].otcTradeable;
                            if (instrumentNode.Response.markets[0].percentageChange != null)
                                temp.PercentageChange = instrumentNode.Response.markets[0].percentageChange;
                            if (instrumentNode.Response.markets[0].scalingFactor != null)
                                temp.ScalingFactor = instrumentNode.Response.markets[0].scalingFactor;
                            if (instrumentNode.Response.markets[0].streamingPricesAvailable != null)
                                temp.StreamingPricesAvailable = instrumentNode.Response.markets[0].streamingPricesAvailable;
                            if (instrumentNode.Response.markets[0].updateTime != null)
                                temp.UpdateTime = instrumentNode.Response.markets[0].updateTime;
                            if (node.Id != null)
                                temp.HierarchyNodeId = node.Id;


                            dataOperations.InsertHierarchyMarket(temp);
                            Thread.Sleep(1000);
                        }
                        else
                        {
                            System.Diagnostics.Debug.WriteLine(node.Id.ToString() + "  " + instrumentNode.StatusCode);
                        }
                    }
                    catch (Exception e)
                    {
                        System.Diagnostics.Debug.WriteLine(e);
                        throw;
                    }
                  
                }
            }

            //System.Diagnostics.Debug.WriteLine("Count=" + marketList.Count);
            


        }

    }
}




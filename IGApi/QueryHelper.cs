using System;
using System.Net.Http;

namespace IGApi
{
    public class QueryHelper
    {
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


using System;
using System.Collections.Generic;
using System.Text;

namespace IGApi
{
    public class ConversationContext
    {
        public ConversationContext(string cst, string xSecurityToken, string apiKey)
        {
            this.cst = cst;
            this.xSecurityToken = xSecurityToken;
            this.apiKey = apiKey;
        }  

        public string cst { get; set; }
        public string xSecurityToken { get; set; }
        public string apiKey { get; set; }
    }


}

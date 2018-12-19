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

        public string cst { get;  }
        public string xSecurityToken { get; }
        public string apiKey { get; }
    }


}

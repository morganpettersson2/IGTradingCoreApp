using System.Collections.Generic;

namespace IGApi.Model.dto.endpoint.accountbalance
{
    public class AccountDetailsResponse
    {
        ///<Summary>
        ///List of client accounts
        ///</Summary>
        public List<AccountDetails> accounts { get; set; }
    }
}
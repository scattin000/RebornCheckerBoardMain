using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RebornCheckerBoardMain.Models.IssueToken
{
    /// <summary>
    /// The operation parameters for issuing tokens.
    /// </summary>
    public class IssueTokenModel
    {
        /// <summary>
        /// The type of produ t the token is being issued for.
        /// </summary>
        public TokenType TokenType { get; set; }

        /// <summary>
        /// The value of the token. May vary dependent on the token type.
        /// </summary>
        public ITokenValue TokenValue { get; set; }

        /// <summary>
        /// The title of the product being issued a token for.
        /// </summary>
        public string TokenContent { get; set; }

        /// <summary>
        /// The reason for the token issuance.
        /// </summary>
        public string Reason { get; set; }

        /// <summary>
        /// The email address of the customer the token is to be issued to.
        /// </summary>
        public string EmailAddress { get; set; }

        /// <summary>
        /// Comments about the token issuance from the agent.
        /// </summary>
        public string Comments { get; set; }
    }
}
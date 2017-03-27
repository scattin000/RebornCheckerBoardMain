using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RebornCheckerBoardMain.Models.DeactivateToken
{
    public class DeactivateTokenModel
    {
        /// <summary>
        /// The type of product the token was issued for. 
        /// </summary>
        public TokenType TokenType { get; set; }

        /// <summary>
        /// The value of the token that was issued
        /// </summary>
        public ITokenValue TokenValue { get; set; }

        /// <summary>
        /// The title of the product was issued for
        /// </summary>
        public string TokenContent { get; set; }

        /// <summary>
        /// The email address of the customer the token is to be issued to.
        /// </summary>
        public string EmailAddress { get; set; }

        /// <summary>
        /// Agent needs to enter a reason for deactivation
        /// </summary>
        public string Reason { get; set; }

        /// <summary>
        /// Comments about the token deactivation from the agent.
        /// </summary>
        public string Comments { get; set; }
    }
}
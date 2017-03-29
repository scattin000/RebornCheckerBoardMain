﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RebornCheckerBoardMain.Models
{
    /// <summary>
    /// Represents a token value for an issued token. Needs to call the Token Type to get 
    /// the details for that Type 
    /// </summary>
    public interface ITokenValue
    {
        /// <summary>
        /// Gets the token type for this token value.
        /// </summary>
        TokenType TokenType { get; }
    }

    /// <summary>
    /// The token value for games.
    /// </summary>
    public class GameTokenValue : ITokenValue
    {
        /// <summary>
        /// The token type.
        /// </summary>
        public TokenType TokenType { get { return TokenType.Game; } }

        /// <summary>
        /// The content for the game - hold the value of the token given
        /// </summary>
        public string DLCValue { get; set; }    
    }

    /// <summary>
    /// The token value for subscriptions.
    /// </summary>
    public class SubscriptionTokenValue : ITokenValue
    {
        /// <summary>
        /// The token type.
        /// </summary>
        public TokenType TokenType { get { return TokenType.Subscription; } }
        
        /// <summary>
        /// How many months should this subscription be issued.
        /// Hold the value of the token given
        /// </summary>
        
        public int SubscriptionMonths { get; set; }
    }
}
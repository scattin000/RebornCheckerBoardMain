using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace RebornCheckerBoardMain.Models.IssueToken
{
    /// <summary>
    /// The operation parameters for issuing tokens.
    /// </summary>
    public class IssueTokenModel
    {
        /// <summary>
        /// Unique to each portion this will be what is looked up in the deactivate
        /// </summary>
        [Key]
        public Guid TokenCode { get; set; }

        /// <summary>
        /// The type of produt the token is being issued for.
        /// </summary>
        public TokenType TokenType { get; set; }

        /// <summary>
        /// The value of the token. May vary dependent on the token type.
        /// </summary>
        public ITokenValue TokenValue { get; set; }

        /// <summary>
        /// The game title of the product being issued a token for.
        /// </summary>
        public string TokenContent { get; set; }

        /// <summary>
        /// The list of game titles of any game token.
        /// </summary>
        public SelectListItem[] GameTokenContent { get; set; }

        /// <summary>
        /// The list of subscription titles of any subscription token.
        /// </summary>
        public SelectListItem[] SubscriptionTokenContent { get; set; }
        /// <summary>
        /// List of game token values that can be given
        /// </summary>
        public SelectListItem[] GameTokenValue { get; set; }

        /// <summary>
        /// The reason for the token issuance.
        /// </summary>
        [Required(ErrorMessage ="Reason is Required")]
        public string Reason { get; set; }

        /// <summary>
        /// The list of valid reasons that an agent can fill in
        /// </summary>
        public SelectListItem[] ReasonChoices { get; set; }

        /// <summary>
        /// The email address of the customer the token is to be issued to.
        /// </summary>
        [Required(ErrorMessage ="Email is Required")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                            @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                            @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
                            ErrorMessage = "Invalid format, make sure the email follows this format: example@example.com")]
        public string EmailAddress { get; set; }

        /// <summary>
        /// Comments about the token issuance from the agent.
        /// </summary>
        [Required(ErrorMessage = "Comments are Required")]
        public string Comments { get; set; }
        /// <summary>
        /// This will be true whenever a token is issued 
        /// Deactivation == False This should be hidden... 
        /// </summary>
        public bool Status { get; set; }
    }

    // Handles fetching, storing, updating the TokenCategory instances of DB 
    public class IssueTokenModelDBContext : DbContext
    {
        //this was Tokens in the other one
        public DbSet<IssueTokenModel> IssuedTokens { get; set; }
    }

    /// <summary>
    /// The set of types of tokens that may be issued.
    /// </summary>
    public enum TokenType
    {
        /// <summary>
        /// A game or a DLC.
        /// </summary>
        Game,

        /// <summary>
        /// A subscription to a service.
        /// </summary>
        Subscription
    }
}
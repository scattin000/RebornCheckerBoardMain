using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using RebornCheckerBoardMain.Entities;

namespace RebornCheckerBoardMain.Models.DeactivateToken
{
    public class DeactivateTokenModel
    {
        // display the GUID associated 
        [Key]
        public Guid TokenCode { get; set; }
        // show the previous token type
        public TokenType TokenType { get; set; }
        // show the previous token Content
        public string TokenContent { get; set; }
        // show the previous token Value
        public string TokenValue { get; set; }

        // show the Account token was sent to 
        public string EmailAddress { get; set; }

        /// <summary>
        /// Things that will be new and saved!!!
        /// </summary>
        // Get a new reason for deactivation
        [Required(ErrorMessage = "Reason is Required")]
        public string Reason { get; set; }
        // Get new comment for action
        [Required(ErrorMessage = "Comments are Required")]
        public string Comments { get; set; }

        //Status will be updated in the 
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
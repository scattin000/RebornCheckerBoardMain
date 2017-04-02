using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RebornCheckerBoardMain.Entities
{
    /// <summary>
    /// The operation parameters for issuing tokens.
    /// </summary>
    // This is the schema for your database. If you need to make new changes here,
    // they will not automatically break your MVC models, which is great because you'll
    // have less code to update when you need to change this class. Also, you won't be
    // having to recreate the database a bazillion times.
    public class TokenEntity
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
        public string TokenValue { get; set; }

        /// <summary>
        /// The game title of the product being issued a token for.
        /// </summary>
        public string TokenContent { get; set; }

        public string IssuingReason { get; set; }

        public string EmailAddress { get; set; }

        public string IssuingComments { get; set; }

        public string DeactivatingReason { get; set; }

        public string DeactivationComments { get; set; }

        public bool Status { get; set; }
    }

    public class TokenDbContext : DbContext
    {
        //this was Tokens in the other one
        public DbSet<TokenEntity> IssuedTokens { get; set; }
    }
}
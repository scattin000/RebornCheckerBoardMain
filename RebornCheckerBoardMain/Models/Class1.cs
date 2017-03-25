
using System.Data.Entity;
using System.Linq;
using System.Web;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RebornCheckerBoardMain.Models
{
    /* Database for Token Category */ 
    public class TokenCategory
    {
        // this is the key - not displayed or really part of the token
        [Key]
        public int ID { get; set; }
        // token type 
        [Required]
        public string Type { get; set; }
        // Token Content
        [Required]
        public string Content { get; set; }
        // Token Values (can be numbers OR chars)
        [Required]
        public int Value { get; set; }
        //Token Value Label - IF go with vouchers as well 
        [Required]
        public string ValueLabel { get; set; }
        // Token Range for the specified type 
        [Required]
        public int ValueRange { get; set; }
        // Token Value Range Minimum
        [Required]
        public int ValueMin { get; set; }
    }
    // Handles fetching, storing, updating the TokenCategory instances of DB 
    public class TokenCategoryDBContext : DbContext
    {
        public DbSet<TokenCategory> Tokens { get; set; }
    }

    /* Second table for the Issued Tokens
    public class IssuedTokens
    { // token type
        public string Type { get; set; }
        // Token Content
        public string Content { get; set; }
        // Token Values (can be numbers OR chars)
        public int Value { get; set; }
        //Token Value Label - IF go with vouchers as well 
        public string ValueLabel { get; set; }
        // GUID
        [Key]
        public int Guid { get; set; }
        //CustomerEmail
        [Required]
        public string email { get; set; }
    }

    // Handles fetching, storing, updating the TokenCategory instances of DB 
    public class IssuedTokensDBContext : DbContext
    {
        public DbSet<IssuedTokens> Tokens { get; set; }

       // public System.Data.Entity.DbSet<RebornCheckerBoardMain.Models.TokenCategory> TokenCategories { get; set; }
    }
    */

}
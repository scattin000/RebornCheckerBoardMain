using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RebornCheckerBoardMain.Models;
using RebornCheckerBoardMain.Models.IssueToken;
using RebornCheckerBoardMain.Entities;
using RebornCheckerBoardMain.Models.DeactivateToken;

namespace RebornCheckerBoardMain.Controllers
{
    public class IssueTokenModelsController : Controller
    {
        private TokenDbContext tokens;
        
        public IssueTokenModelsController()
        {
            tokens = new TokenDbContext();
        }

        /// <summary>
        /// Get the tokens to display in the issueToken Page? ( link this to the
        /// the TokenCategory??? - How to GET data from one controller to another?)
        /// How to GET data from one Table in DB 
        /// </summary>
        // GET: IssueTokenModels
        public ActionResult Index()
        {
            // Filling the Token Content attributes to display on the page 
            var model = new IssueTokenModel
            { // FILL THE SET CONTENT VALUES!
                GameTokenContent = new SelectListItem[]
                {
                    new SelectListItem() { Value = "Battle Of The Worlds", Text = "Battle Of The Worlds" },
                    new SelectListItem() { Value = "Builder Blocks 2", Text = "Builder Blocks 2" },
                    new SelectListItem() { Value = "Builder Blocks 3", Text = "Builder Blocks 3" },
                    new SelectListItem() { Value = "Diver Craze", Text = "Diver Craze" },
                    new SelectListItem() { Value = "Diver Craze 2", Text = "Diver Craze 2" },
                    new SelectListItem() { Value = "Race To Mars", Text = "Race To Mars" },
                    new SelectListItem() { Value = "Rock 'Em Sock 'Em", Text = "Rock 'Em Sock 'Em" }
                },
                SubscriptionTokenContent = new SelectListItem[]
                {
                    new SelectListItem() { Value = "EducationOnline", Text = "EducationOnline" },
                    //Values WERE subscription1 & Subscription2 
                    new SelectListItem() { Value = "GamersPlus", Text = "GamersPlus" } // keep these values the same, feed them directly from the db
                }, // good practice to set to empty

                //FILL THE TOKEN VALUES (SUBSCRIPTION IS A RANGE SO NA)
                GameTokenValue = new SelectListItem[]
                {
                    new SelectListItem() { Value = "Standard Ed.", Text = "Standard Edition"},
                    new SelectListItem() { Value = "Speical Ed.", Text = "Special Edition" },
                    new SelectListItem() { Value = "DLC-100 game Currency", Text = "DLC-100 game Currency"},
                    new SelectListItem() { Value = "DLC-50 game Currency", Text = "DLC-50 game Currency"},
                    new SelectListItem() { Value = "DLC-Character", Text = "DLC-Character"},
                    new SelectListItem() { Value = "DLC-Bonus Lvl", Text = "DLC-Bonus Level"}
                },

                //Set contents for the Reason Drop down for issuing a token
                ReasonChoices = new SelectListItem[]
                {
                    new SelectListItem() { Value = "Make Good", Text = "Make Good" },
                    new SelectListItem() { Value = "Troubleshooting Issues - System", Text = "Troubleshooting Issues - System"},
                    new SelectListItem() { Value = "Technical Issues - System", Text = "Technical Issues - System" },
                    new SelectListItem() { Value = "Promotion Not Received", Text = "Promotional Token Not Received" },
                    new SelectListItem() { Value = "Damaged", Text = "Damaged Code" },
                    new SelectListItem() { Value = "Missing From Bundle", Text = "Missing Item From Bundle" },
                    new SelectListItem() { Value = "Inactive Status", Text = "Inactive Status" },
                    new SelectListItem() { Value = "Invalid Code", Text = "Invalid Code" },
                    new SelectListItem() { Value = "Incorrect Token Issued", Text = "Incorrect Token Type Issued" },
                },
                // good practice to set to empty
                TokenContent = string.Empty
                //TokenValue = string.Empty
            };
            return View(model);
        }
        /// <summary>
        /// Display the verification page if the token entry is valid.
        /// Passing in the model data from the issueToken view 
        /// </summary>
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Index(IssueTokenModel model)
        {
            var isValid = true;
            // if something in the verification doesn't work
            if (!ModelState.IsValid)
            { // is valid is false
                isValid = false;
            } // IF the token type is subscription run this check!
            if (model.TokenType == Models.IssueToken.TokenType.Subscription)
            { 
                int subscriptionMonths;
                // If not able to parse? then 
                if (!int.TryParse(model.TokenValue, out subscriptionMonths))
                { // try to parse the int, and if it's not an int then error message 
                    ModelState.AddModelError("Subscription Months", "Subscription months must be a number.");
                    isValid = false;
                }
                if (subscriptionMonths < 1 || subscriptionMonths > 24)
                { // if the int is out of range then display an error!!
                    ModelState.AddModelError("Subscription Months", "Subscriptions may only be issued for between 1 and 24 months.");
                    isValid = false;
                }
            }
            // if all the checks pass then proceed and it's valid 
            if (isValid)
            { // go to the verifyIssueToken partial view & display the model information
                return View("VerifyIssueToken", model);
            }
            return View(model);
        }

        /// <summary>
        /// I clicked issue token. Do the save.
        /// Take the model information from the verify token partial view & store to DB
        /// </summary>
        [HttpPost]
        public ActionResult HandleVerify(IssueTokenModel model)
        {        
            // Write all your code for sending to the database here; we know we have a valid token and the
            // user has confirmed they want to issue it, so it's time to do that now.
            // send data to the data base  (see the create function below??)
            if (ModelState.IsValid)
            {
                // We will go from a model to an entity because we're saving to the database.
                // We know that the model contains values that are safe to store because of the
                // ModelState.IsValid check above, so we know we won't write anything that'll screw
                // up the database.
                var token = new TokenEntity
                {
                    TokenCode = Guid.NewGuid(), // Our new identifier. The user doesn't get to pick one, so we do that here.
                    TokenType = ToEntityTokenType(model.TokenType),
                    TokenContent = model.TokenContent,
                    IssuingReason = model.Reason,
                    IssuingComments = model.Comments,
                    EmailAddress = model.EmailAddress,
                    TokenValue = model.TokenValue,
                    Status = true // All issued tokens are by default active - the user can't issue a deactivated token.
                };

                // Now we're adding the token to the database...
                tokens.IssuedTokens.Add(token);               
                // ... and saving it.
                tokens.SaveChanges();

                //return View();
                // Assuming we got here, the save worked fine. Go ahead and return control to the page;
                // our Javascript will pop up the alert saying that it's successful.
            return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            // Stays on the page if there is an error... (set to display system error message)
            return View(model);           
        }

        private Entities.TokenType ToEntityTokenType(Models.IssueToken.TokenType tokenType)
        {
            // This is the proper way to convert between one enum type to another. This guarantees
            // if we add a new Model.TokenType or an Entity.TokenType we will still be able to support
            // it.
            switch (tokenType)
            {
                case Models.IssueToken.TokenType.Game:
                    return Entities.TokenType.Game;

                case Models.IssueToken.TokenType.Subscription:
                    return Entities.TokenType.Subscription;

                default:
                    throw new NotSupportedException($"The token type '{tokenType}' is not supported.");
            }
        }
     
        /// <summary>
        /// Get the GUID's (Token Codes) for all the tokens to allow the user to select
        /// Which token they want to deactivate
        /// </summary>
        // GET: IssueTokenModels/Edit/5
        public ActionResult Deactivate()
        {
            // This will issue a query to the database to get all the token codes.
            // This will (behind the scenes) run a SQL query like
            //
            // SELECT TokenCode
            // FROM [dbo].[TokenEntity]
            // WHERE Status = 1
            //
            // and return the results.
            var tokenCodes = tokens.IssuedTokens.Where(token => token.Status == true).Select(token => token.TokenCode).ToList();

            // Now we will transform them into SelectListItems for our page (because we can't use
            // GUIDs directly in Html.DropDownListFor.
            var tokenListItems = new List<SelectListItem>();
            foreach (var tokenCode in tokenCodes)
            {
                var tokenListItem = new SelectListItem()
                {
                    Text = tokenCode.ToString(), // This is what the user will see.
                    Value = tokenCode.ToString() // This is what the controller and CSHTML will see.
                };
                tokenListItems.Add(tokenListItem);
            };

            // We need the first token code coming back from the database in order to be able to
            // populate the Javascript on our page as we render it. Remember, we call loadToken() at
            // the time we've finished loading our page. Leaving the selected token code empty would mean
            // that we are trying to load a token with a null ID. That's not good.
            //
            // Therefore, we'll look up the first entity in our database and get its code, and use
            // that as the primary selection.           
            Guid selectedTokenCode = tokenCodes.FirstOrDefault();
       
            // The first thing that the user will do is that they need to pick which token they
            // want to deactivate. That's a separate operation than actually deactivating a token,
            // which is why we have a specific model (SelectTokenToDeactivateModel) to do that - provide
            // a list of tokens and then have them pick one to deactivate.
            // 
            // We'll use Javascript to actually get the token content with another controller method
            // we will write (below this one).
            var model = new SelectTokenToDeactivateModel
            {
                TokensToChooseFrom = tokenListItems.ToArray(),
                SelectedTokenCode = selectedTokenCode == Guid.Empty ? "" : selectedTokenCode.ToString(),
                ReasonChoices = new SelectListItem[]
                {
                    new SelectListItem() { Value = "Created/ Inactive Status", Text = "Created/ Inactive Status "},
                    new SelectListItem() { Value = "Active Token Troubleshooting", Text = "Active Token Troubleshooting " },
                    new SelectListItem() { Value = "Technical Difficulties", Text = "Technical Difficulties " },
                    new SelectListItem() { Value = "Generated In Error", Text = "Generated Token In Error " },
                    new SelectListItem() { Value = "Issued Incorrect Token", Text = "Issued Incorrect Token Type" },
                    new SelectListItem() { Value = "Fraud/Abuse", Text = "Fraud/Abuse Reported" }
                },
            };

            return View(model);
        }
        /// <summary>
        /// Display contents on the page and deal with the actualy deactivation 
        /// </summary>
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult DisplayDeactiveTokenDetails(Guid id)
        {
            // Let's get the token.
            TokenEntity token = LookupEntityById(id);

            // We have our token. We will return it as JSON so the jQuery function we wrote
            // can interpret it and do its thing.
            return Json(token);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [Route("DeactivateToken/{id}")]
        public ActionResult DeactivateToken(Guid id, DeactivateTokenModel tokenModel)
        {
            // Here let's get the token by its id...
            TokenEntity token = LookupEntityById(id);
        
            // ... and save the deactivating reason and comments from our model.
            // We'll also deactivate it by setting the status to false.
            token.DeactivatingReason = tokenModel.Reason;
            token.DeactivationComments = tokenModel.Comments;
            token.Status = false;

            // And save it!
            tokens.SaveChanges();
            // And we're done. Return OK to the page.
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        private TokenEntity LookupEntityById(Guid id)
        {
            // We have an ID. Fetch the token data from the database.
            //
            // This will (behind the scenes) generate a query like:
            //
            // SELECT * 
            // FROM [dbo].[Tokens]
            // WHERE TokenCode = id
            //
            // ... and returns the first value from that result. If there are no results, it will return null,
            // which we need to account for.
            TokenEntity token = tokens.IssuedTokens.FirstOrDefault(entity => entity.TokenCode == id);

            // This is just in case someone breaks our database and takes out our token while we
            // were attempting to deactivate it. 
            if (token == null)
            {
                throw new KeyNotFoundException($"The ID '{id}' was not found in the database.");
            }

            return token;
        }

        /// <summary>
        /// Get information from the IssueTOken (Index Page) to verify the token
        /// On "Next >"
        /// </summary>
        // GET: IssueTokenModels/Details/5
        public ActionResult Details()//Guid? id)
        {
            /* if (id == null)
             {
                 return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
             }
             IssueTokenModel issueTokenModel = db.IssuedTokens.Find(id);
             if (issueTokenModel == null)
             {
                 return HttpNotFound();
             }
             return View(issueTokenModel);*/
            return View();
        }

    }
}

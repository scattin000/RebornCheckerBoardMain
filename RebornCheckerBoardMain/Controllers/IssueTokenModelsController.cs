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

namespace RebornCheckerBoardMain.Controllers
{
    public class IssueTokenModelsController : Controller
    {
        private IssueTokenModelDBContext db = new IssueTokenModelDBContext();
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
        [ValidateAntiForgeryToken]
        public ActionResult HandleVerify(IssueTokenModel model, string Command)
        {
            // IF the agent select "Cancel" Button... 
            if (Command == "Cancel" )
            {
                // This line will return them to the Issue Token page, and have a blank form to fill out.
                return RedirectToAction("Index");
            }/*
            else if(Command == "< Prev")
            {
                //return the user to the previous page and do NOT erase the values, they need to edit the model
                return RedirectToAction("Index",model);
            }*/
            else // otherwise if they select "Submit"
            {
                // Write all your code for sending to the database here; we know we have a valid token and the
                // user has confirmed they want to issue it, so it's time to do that now.
                // send data to the data base  (see the create function below??)
                if (ModelState.IsValid)
                {
                    // set Status to true when tokens are issued 
                    model.Status = true;
                    // create a GUID for the model
                    model.TokenCode = Guid.NewGuid();
                    //Add the new GUID & Status to the model
                    db.IssuedTokens.Add(model);
                    //Save the changes to DB - do I need to pass in the model?
                    db.SaveChanges();

                    //display confirmation alert IF successful 
                    // want to display alert with Okay button that would then allow the agent to select and be taken 
                    // to the home page?

                    // now go back to home after alert is shown? Or after they select " home"?
                   return RedirectToAction("Index");
                  // return View(model);
                }
                // what does this do? (returns the model to the view... but 
                return View(model);
            }
            
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

        /// <summary>
        /// GET token info from Verification(Details page) & create a new token
        /// On "Submit" in verification page 
        /// </summary>
        // GET: IssueTokenModels/Create
        public ActionResult Create()
        {

            return View();
        }
        /// <summary>
        /// POST: IssueTokenModels/Create
        /// SEND data from Issue page to the Issued Token DB 
        /// This creates the new token and then stores that data 
        /// </summary>
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TokenCode,TokenType,TokenContent,Reason,EmailAddress,Comments,Status")] IssueTokenModel issueTokenModel)
        {
            if (ModelState.IsValid)
            {
                issueTokenModel.TokenCode = Guid.NewGuid();
                db.IssuedTokens.Add(issueTokenModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(issueTokenModel);
        }
        /// <summary>
        /// This is when we actually want to deactivate the token
        /// they can do this here :) 
        /// because you cannot edit the transaction after it's been submitted 
        /// </summary>
        // GET: IssueTokenModels/Edit/5
        public ActionResult Deactivate(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IssueTokenModel issueTokenModel = db.IssuedTokens.Find(id);
            if (issueTokenModel == null)
            {
                return HttpNotFound();
            }
            return View(issueTokenModel);
        }
        /// <summary>
        /// Edit the transaction??? Not sure how to set this one up to link
        /// to the DB because at this point it's not actually sent to the DB yet...
        ///  THIS WILL BE THE DEACTIVATE!!!!!!!!!!!
        /// </summary>
        // POST: IssueTokenModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Deactivate([Bind(Include = "TokenCode,TokenType,TokenContent,Reason,EmailAddress,Comments,Status")] IssueTokenModel issueTokenModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(issueTokenModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(issueTokenModel);
        }

        // GET: IssueTokenModels/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IssueTokenModel issueTokenModel = db.IssuedTokens.Find(id);
            if (issueTokenModel == null)
            {
                return HttpNotFound();
            }
            return View(issueTokenModel);
        }

        // POST: IssueTokenModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            IssueTokenModel issueTokenModel = db.IssuedTokens.Find(id);
            db.IssuedTokens.Remove(issueTokenModel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

﻿using System;
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
                    new SelectListItem() { Value = "Game1", Text = "Game1" },
                    new SelectListItem() { Value = "Game2", Text = "Text2" }
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
                    new SelectListItem() { Value = "DLC-Character", Text = "DLC-Character"}
                },

                //Set contents for the Reason Drop down
                ReasonChoices = new SelectListItem[]
                {
                    new SelectListItem() { Value = "Reason1", Text = "Reason 1" },
                    new SelectListItem() { Value = "Reason2", Text = "Reason 2" }
                },

                // good practice to set to empty
                TokenContent = string.Empty
                //TokenValue = string.Empty

            };

            return View(model);
        }
        /// <summary>
        /// Display the verification page if the token entry is valid. 
        /// </summary>
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Index(IssueTokenModel model)
        {
            var isValid = true;
            if (!ModelState.IsValid)
            {
                isValid = false;
            }
            if (model.TokenType == Models.IssueToken.TokenType.Subscription)
            {
                int subscriptionMonths;
                if (!int.TryParse(model.TokenValue, out subscriptionMonths))
                {
                    ModelState.AddModelError("Subscription Months", "Subscription months must be a number.");
                    isValid = false;
                }
                if (subscriptionMonths < 1 || subscriptionMonths > 24)
                {
                    ModelState.AddModelError("Subscription Months", "Subscriptions may only be issued for between 1 and 24 months.");
                    isValid = false;
                }
            }

            if (isValid)
            {
                return View("VerifyIssueToken", model);
            }

            return View(model);
        }

        /// <summary>
        /// I clicked issue token. Do the save.
        /// </summary>
        public ActionResult HandleVerify(IssueTokenModel model)
        {
            // Write all your code for sending to the database here; we know we have a valid token and the
            // user has confirmed they want to issue it, so it's time to do that now.

            // send data to the data base 
            // set Status == True 

            // This line will return them to the Issue Token page, and have a blank form to fill out.
            return View("Index");
        }

        /// <summary>
        /// Get information from the IssueTOken (Index Page) to verify the token
        /// On "Next >"
        /// </summary>
        // GET: IssueTokenModels/Details/5
        public ActionResult Details(Guid? id)
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
        /// GET token info from Verification(Details page) & create a new token
        /// On "Submit" in verification page 
        /// </summary>
        // GET: IssueTokenModels/Create
        public ActionResult Create()
        {

            return View();
        }
        /// <summary>
        /// SEND data from Issue page to the Issued Token DB 
        /// This creates the new token and then stores that data 
        /// </summary>
        // POST: IssueTokenModels/Create
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
        public ActionResult Edit(Guid? id)
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
        public ActionResult Edit([Bind(Include = "TokenCode,TokenType,TokenContent,Reason,EmailAddress,Comments,Status")] IssueTokenModel issueTokenModel)
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

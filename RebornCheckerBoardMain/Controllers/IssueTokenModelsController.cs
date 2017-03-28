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
            return View();//db.IssuedTokens.ToList());
        }
        /// <summary>
        /// Get information from the IssueTOken (Index Page) to verify the token
        /// On "Next"
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

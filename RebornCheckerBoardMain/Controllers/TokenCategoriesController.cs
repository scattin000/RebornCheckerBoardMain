using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RebornCheckerBoardMain.Models;

namespace RebornCheckerBoardMain.Controllers
{
    public class TokenCategoriesController : Controller
    {
        private TokenCategoryDBContext db = new TokenCategoryDBContext();

        // GET: TokenCategories - Search the Token Types 

        public ActionResult Index(string SearchContent, string SearchType)
        {
           // display list of token content 
            var ContentLst = new List<string>();

            var ContentQry = from d in db.Tokens
                           orderby d.Content
                           select d.Content;
            //AddRange adds all distinct different values to the list --> need to specify for the selected token type??

            ContentLst.AddRange(ContentQry.Distinct());
            //Stores list of items in the SelectList 
            ViewBag.SearchContent = new SelectList(ContentLst);
            
            //display list of token Types  
            var TypeLst = new List<string>();

            var TypeQry = from d in db.Tokens
                          orderby d.Type
                          select d.Type;

            TypeLst.AddRange(TypeQry.Distinct());
            ViewBag.SearchType = new SelectList(TypeLst);

            // display list of token types 
            var tokens = from m in db.Tokens
                         select m;
            if (!string.IsNullOrEmpty(SearchType))
            {
                tokens = tokens.Where(s => s.Type == SearchType);
            }
            // display the contenct options
            if (!string.IsNullOrEmpty(SearchContent))
            {
                tokens = tokens.Where(x => x.Content == SearchContent);
            }
            return View(tokens);
        }

        // GET: TokenCategories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TokenCategory tokenCategory = db.Tokens.Find(id);
            if (tokenCategory == null)
            {
                return HttpNotFound();
            }
            return View(tokenCategory);
        }

        // GET: TokenCategories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TokenCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Type,Content,Value,ValueLabel,ValueRange,ValueMin")] TokenCategory tokenCategory)
        {
            if (ModelState.IsValid)
            {
                db.Tokens.Add(tokenCategory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tokenCategory);
        }

        // GET: TokenCategories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TokenCategory tokenCategory = db.Tokens.Find(id);
            if (tokenCategory == null)
            {
                return HttpNotFound();
            }
            return View(tokenCategory);
        }

        // POST: TokenCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Type,Content,Value,ValueLabel,ValueRange,ValueMin")] TokenCategory tokenCategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tokenCategory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tokenCategory);
        }

        // GET: TokenCategories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TokenCategory tokenCategory = db.Tokens.Find(id);
            if (tokenCategory == null)
            {
                return HttpNotFound();
            }
            return View(tokenCategory);
        }

        // POST: TokenCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TokenCategory tokenCategory = db.Tokens.Find(id);
            db.Tokens.Remove(tokenCategory);
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

    //How to add another page here??
}

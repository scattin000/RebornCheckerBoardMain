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
    public class HomeController : Controller
    {
        private TokenCategoryDBContext db = new TokenCategoryDBContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Issue()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }
        // CodeFor what the deactivate will generate
        public ActionResult Deactivate()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }

        public ActionResult IssueTokens(string SearchContent, string SearchType)
        {
            // display list of token content 
            var ContentLst = new List<string>();

            var ContentQry = from d in db.Tokens
                             orderby d.Content
                             select d.Content;

            ContentLst.AddRange(ContentQry.Distinct());
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
            if (!string.IsNullOrEmpty(SearchContent))
            {
                tokens = tokens.Where(x => x.Content == SearchContent);
            }
            return View(tokens);
        }

    }
}
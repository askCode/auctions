using Bids.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bids.Controllers
{
    public class MembersController : Controller
    {

        private AuctionContext db = new AuctionContext();
        //
        // GET: /Members/

        public ActionResult Index()
        {
            return View(db.Members.ToList());
        }

        [HttpGet]
        public ViewResult Create()
        {
            return View(new Bids.Models.Member { LoginName = "test" }); 
        }

        [HttpPost]
        public ActionResult Create(Bids.Models.Member member)
        {
            if (ModelState.IsValid)
            {
                db.Members.Add(member);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(member); 
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var member = db.Members.Find(id);
            return View(member);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Member member)
        {
            if (ModelState.IsValid)
            {
                db.Entry(member).State = System.Data.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(member); 

        }
    }
}

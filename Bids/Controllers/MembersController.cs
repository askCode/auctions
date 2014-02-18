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

        //private AuctionContext db = new AuctionContext();
        private IMemberRepository memberRepository;
        public MembersController()
        {
            this.memberRepository = new MemberRepository(new AuctionContext());
        }
        //
        // GET: /Members/

        public ActionResult Index()
        {
            //return View(db.Members.ToList());
            return View(memberRepository.GetMembers());
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
                //db.Members.Add(member);
                //db.SaveChanges();
                memberRepository.InsertMember(member);
                memberRepository.Save();
                return RedirectToAction("Index");
            }
            return View(member); 
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            //var member = db.Members.Find(id);
            var member = memberRepository.GetMemberById(id);
            return View(member);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Member member)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(member).State = System.Data.EntityState.Modified;
                //db.SaveChanges();
                memberRepository.UpdateMember(member);
                memberRepository.Save();
                return RedirectToAction("Index");
            }
            return View(member); 
        }

        protected override void Dispose(bool disposing)
        {
            memberRepository.Dispose();
            base.Dispose(disposing);
        }
    }
}

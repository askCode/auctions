using Bids.DAL;
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
        //private IMemberRepository memberRepository;
        private UnitOfWork unitOfWork = new UnitOfWork();
        
        //
        // GET: /Members/
        [HttpGet]
        public ActionResult Index()
        {
            //return View(db.Members.ToList());
            //return View(memberRepository.GetMembers());
            return View(unitOfWork.MemberRepository.Get());
        }
        [HttpGet]
        public ViewResult Details(int id)
        {
            return View(unitOfWork.MemberRepository.GetById(id));
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
                unitOfWork.MemberRepository.Insert(member);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View(member); 
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            //var member = db.Members.Find(id);
            //var member = memberRepository.GetMemberById(id);
            var member = unitOfWork.MemberRepository.GetById(id);
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
                //memberRepository.UpdateMember(member);
                //memberRepository.Save();
                unitOfWork.MemberRepository.Update(member);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View(member); 
        }

        protected override void Dispose(bool disposing)
        {
            //db.Dispose();
            //memberRepository.Dispose();
            //To make sure that database connections are properly closed and the resources they hold freed up
            unitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }
}

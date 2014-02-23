using Bids.WebUI.DAL;
using Bids.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bids.WebUI.Controllers
{
    [Authorize]
    public class ItemsController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        //
        // GET: /Items/        
        [HttpGet]
        public ActionResult Index()
        {
            return View(unitOfWork.ItemRepository.Get());
        }

        [HttpGet]
        public ViewResult Details(int id)
        {
            return View(unitOfWork.ItemRepository.GetById(id));
        }

        [HttpGet]
        public ViewResult Create()
        {
            UserProfile user;
            //using (UsersContext db = new UsersContext())
            //{
            //    user = db.UserProfiles.FirstOrDefault(u => u.UserName.ToLower() == User.Identity.Name.ToLower());

            //}
            user = unitOfWork.MemberRepository.Get(u => u.UserName.ToLower() == User.Identity.Name.ToLower()).First();

            return View(new Item { UserId = user.UserId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Item item)
        {
            //using (UsersContext db = new UsersContext())
            //{
            //    UserProfile user = db.UserProfiles.FirstOrDefault(u => u.UserName.ToLower() == User.Identity.Name.ToLower());
            //    item.Owner = user;
            //}
            if (ModelState.IsValid)
            {
                unitOfWork.ItemRepository.Insert(item);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View(item);
        }

        [HttpGet]
        public ViewResult Edit(int id)
        {
            return View(unitOfWork.ItemRepository.GetById(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Item item)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.ItemRepository.Update(item);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View(item);
        }

        [HttpPost]
        public ActionResult AddBid(int itemID, decimal bidAmount)
        {
            var item = unitOfWork.ItemRepository.GetById(itemID);
            //var member = unitOfWork.MemberRepository.Get().First();
            //item.Bids.Add(new Bid { BidAmount = bidAmount, DatePlaced = DateTime.Now, Member = member });
            unitOfWork.ItemRepository.Update(item);
            unitOfWork.Save();


            //return RedirectToAction("Index");
            return RedirectToAction("Details", new { id = itemID });
        }

    }
}

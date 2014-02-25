using Bids.WebUI.DAL;
using Bids.WebUI.Filters;
using Bids.WebUI.Helpers;
using Bids.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace Bids.WebUI.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    public class ItemsController : Controller
    {
        private IUnitOfWork unitOfWork;
        private ISecurityHelper securityHelper;

        public ItemsController()
        {
            unitOfWork = new UnitOfWork();
            securityHelper = new SecurityHelper();
        }

        public ItemsController(IUnitOfWork UoW, ISecurityHelper sh)
        {
            unitOfWork = UoW;
            securityHelper = sh;
        }

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
            return View(new Item { UserId = this.securityHelper.CurrentUserId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Item item)
        {            
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
            
            item.Bids.Add(new Bid { BidAmount = bidAmount, DatePlaced = DateTime.Now, UserID = this.securityHelper.CurrentUserId });
            unitOfWork.ItemRepository.Update(item);
            unitOfWork.Save();

            //return RedirectToAction("Index");
            return RedirectToAction("Details", new { id = itemID });
        }

    }
}

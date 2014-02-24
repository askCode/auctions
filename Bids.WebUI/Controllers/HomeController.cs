using Bids.WebUI.DAL;
using Bids.WebUI.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace Bids.WebUI.Controllers
{
    [InitializeSimpleMembership]
    public class HomeController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";
            ViewBag.MyItems = unitOfWork.ItemRepository.Get(x => x.UserId == WebSecurity.CurrentUserId && x.AuctionEndDate > DateTime.Now);
            ViewBag.MyBids = unitOfWork.BidRepository.Get(x => x.UserID == WebSecurity.CurrentUserId && x.Item.AuctionEndDate > DateTime.Now);
            
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}

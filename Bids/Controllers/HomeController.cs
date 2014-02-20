﻿using Bids.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bids.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        private UnitOfWork unitOfWork = new UnitOfWork();
        public ActionResult Index()
        {
            ViewBag.Members = unitOfWork.MemberRepository.Get();
            return View();
        }

    }
}

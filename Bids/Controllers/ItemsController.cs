﻿using Bids.DAL;
using Bids.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bids.Controllers
{
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
            return View(new Item());
        }

        [HttpPost]
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
    }
}
﻿using Model.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        [ChildActionOnly]
        public PartialViewResult ProductCategory()
        {
            var model = new ProductCategoryDao().ListAll();
            return PartialView(model);
        }

        //View category->danh sach san pham
        public ActionResult Category(long cateId)
        {
            var category = new CategoryDao().ViewDetail(cateId);
            return View(category);
        }

        //View chi tiet san pham
        public ActionResult Detail(long id)
        {
            var product = new ProductDao().ViewDetail(id);
            return View(product);
        }
    }
}
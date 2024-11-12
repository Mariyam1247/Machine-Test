using MachineTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AppContext = MachineTest.EF.AppContext;

namespace MachineTest.Controllers
{
    public class CategoryController : Controller
    {
        AppContext db = new AppContext();
        public ActionResult Index()
        {
            var data = db.Categories.ToList();
            return View(data);
        }
        public ActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Create(Category category)
        {
            db.Categories.Add(category);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Edit( int id)
        {
            var data = db.Categories.Single(x=>x.CategoryId==id);
            return View(data);
        }
        [HttpPost]
        public ActionResult Edit(int id, Category category)
        {
            Category data = db.Categories.Single(x => x.CategoryId == id);
            data.CategoryName=category.CategoryName;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var data = db.Categories.Single(x => x.CategoryId == id);
            return View(data);
        }
        [HttpPost]
        public ActionResult Delete(int id, Category category)
        {
            Category data = db.Categories.Single(x => x.CategoryId == id);
            db.Categories.Remove(data);
             
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            var data = db.Categories.Single(x => x.CategoryId == id);
            return View(data);
        }
    }
}
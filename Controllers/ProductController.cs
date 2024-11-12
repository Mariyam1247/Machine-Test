using MachineTest.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AppContext = MachineTest.EF.AppContext;

namespace MachineTest.Controllers
{
    public class ProductController : Controller
    {
        AppContext db = new AppContext();
        public ActionResult Index(int page = 1, int pageSize = 10)
        {
            //var data = db.Products.ToList();
            //return View(data);

            //var query = from p in db.Products.Include("Category")
            //            select p;

            //var totalRecords = query.Count();
            //var totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

            //var products = (from p in query
            //                orderby p.ProductId  
            //                select p)
            //                .Skip((page - 1) * pageSize)
            //                .Take(pageSize)
            //                .ToList();
            var query = db.Products.Include(p => p.Category);

            var totalRecords = query.Count();
            var totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

            var products = query
                .OrderBy(p => p.ProductId)  
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var model = new ProductListViewModel
            {
                Products = products,
                TotalPages = totalPages,
                CurrentPage = page,
                PageSize = pageSize
            };

            return View(model);
        }

        public ActionResult Create()
        {
            ViewBag.Categories = new SelectList(db.Categories, "CategoryId", "CategoryName");
            return View();
        }
        [HttpPost]
        public ActionResult Create(Product products)
        {
            //db.Products.Add(products);
            //db.SaveChanges();
            //return RedirectToAction("Index");
            if (ModelState.IsValid)
            {
                db.Products.Add(products);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Categories = new SelectList(db.Categories, "CategoryId", "CategoryName");
            return View(products);
        }
        public ActionResult Edit(int id)
        {
            //var data = db.Products.Single(x => x.ProductId == id);
            //return View(data);
            var product = (from p in db.Products
                           where p.ProductId == id
                           select p).FirstOrDefault();
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.Categories = new SelectList(db.Categories, "CategoryId", "CategoryName", product.CategoryId);
            return View(product);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Product products)
        {
            //Product data = db.Products.Single(x => x.CategoryId == id);
            //data.ProductName = products.ProductName;
            //db.SaveChanges();
            //return RedirectToAction("Index");
            if (ModelState.IsValid)
            {
                db.Entry(products).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Categories = new SelectList(db.Categories, "CategoryId", "CategoryName", products.CategoryId);
            return View(products);
        }

        public ActionResult Delete(int id)
        {
            //var data = db.Products.Single(x => x.ProductId == id);
            //return View(data);
            //var product = (from p in db.Products
            //               where p.ProductId == id
            //               select p).FirstOrDefault();
            //if (product == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(product);
            var product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //Product data = db.Products.Single(x => x.ProductId == id);
            //db.Products.Remove(data);

            //db.SaveChanges();

            var product = db.Products.Find(id);
            if (product != null)
            {
                db.Products.Remove(product);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            var data = db.Products.Single(x => x.ProductId == id);
            return View(data);
        }
    }
}
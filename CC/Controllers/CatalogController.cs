using System.Linq;
using CC.BL;
using CC.Data;
using CC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CC.Controllers
{
    public class CatalogController : Controller
    {
        private readonly ProductBl _productBl;
        private readonly StoreBl _storeBl;

        public CatalogController(CCContext ccContext)
        {
            _productBl = new ProductBl(ccContext);
            _storeBl = new StoreBl(ccContext);
        }

        public ActionResult ByCategory(Category category)
        {
            return View("Views/Catalog/index.cshtml", _productBl.GetProductsByCategory(category));
        }

        public ActionResult Search(Category category, double from, double to)
        {
            ViewBag.category = category.ToString();
            try
            {
                if (from <= 0 || to <= 0)
                {
                    return RedirectToAction("Index", "Error", new { error = "from or to cant be negative or zero" });
                }

                if (from > to)
                {
                    return RedirectToAction("Index", "Error", new { error = "from range cant be higer than to" });
                }

                var products = _productBl.GetProductsByCategory(category)
                    .Where(p => p.Price <= to && p.Price >= from);
                return View("Views/Catalog/index.cshtml", products);
            }
            catch
            {
                return RedirectToAction("Index", "Error");
            }
        }

        // GET: Catalog
        public ActionResult Index()
        {
            return View(_productBl.GetAllProducts());
        }

        // GET: Catalog/Details/
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Catalog/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Catalog/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Catalog/Edit/
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Catalog/Edit/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Catalog/Delete/
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Catalog/Delete/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
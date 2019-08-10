using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CC.Models;
using CC.BL;
using CC.Data;
using System;

namespace CC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ProductBl _productBl;

        public HomeController(CCContext ccContext)
        {
            _productBl = new ProductBl(ccContext);
        }

        public IActionResult Index()
        {
            ViewBag.Categories = Enum.GetValues(typeof(Category));
            return View(_productBl.GetAllProducts());
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Please Contact Us";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

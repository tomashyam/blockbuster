using System;
using CC.BL;
using CC.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CC.Controllers
{
    public class CommentController : Controller
    {
        private readonly CommentBl _commentBl;


        public CommentController(CCContext context)
        {
            _commentBl = new CommentBl(context);
        }

        [HttpPost]
        public ActionResult Add(IFormCollection form)
        {
            _commentBl.Add(form["ProductId"], form["Comment"], HttpContext.Session.GetString("ConnectedUserId"));

            return RedirectToAction("Details", "Product", new { id = form["ProductId"] });
        }
    }
}

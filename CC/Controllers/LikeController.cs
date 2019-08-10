using System;
using CC.BL;
using CC.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CC.Controllers
{
    public class LikeController : Controller
    {
        private readonly LikeBl _likeBl;


        public LikeController(CCContext context)
        {
            _likeBl = new LikeBl(context);
        }

        [HttpPost]
        public ActionResult Add([FromBody] string data)
        {
            _likeBl.Add(data.ToString(), HttpContext.Session.GetString("ConnectedUserId"));

            return RedirectToAction("Details", "Product", new { id = data });
        }
    }
}

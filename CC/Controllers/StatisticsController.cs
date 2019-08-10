using CC.Data;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CC.Controllers
{


    public class StatisticsController : Controller
    {
        private readonly CCContext _ccContext;

        public StatisticsController(CCContext ccContext)
        {
            _ccContext = ccContext;
        }

        public ActionResult GetStatistics()
        {

            var usersByGender = _ccContext.User
                .GroupBy(g => g.Gender)
                .Select(x => new { gender = x.Key.ToString(), count = x.Count() })
                .ToList();

            var prodcutsByCategory = _ccContext.Movie
                .GroupBy(p => p.Category)
                .Select(x => new { category = x.Key.ToString(), count = x.Count() })
                .ToList();

            var commentsByGender = _ccContext.Comment
                .GroupBy(c => c.Publisher.Gender)
                .Select(x => new { gender = x.Key.ToString(), count = x.Count() });

            ViewBag.prodcutsByCategory = JsonConvert.SerializeObject(prodcutsByCategory);
            ViewBag.usersByGender = JsonConvert.SerializeObject(usersByGender);
            ViewBag.commentsByGender = JsonConvert.SerializeObject(commentsByGender);

            return View("Views/Statistics/Statistics.cshtml");
        }
    }
}
﻿using CC.DAL;
using CC.Data;
using CC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Linq;

namespace CC.Controllers
{
    public class AdminController : Controller
    {
        private readonly AdminCredentials _settings;
        private readonly UserDataAccess _userDataAccess;
        private readonly AdminDataAccess _adminDataAccess;

        public AdminController(IOptions<AppSettings> settings, CCContext ccContext)
        {
            _settings = settings.Value.AdminCredentials;
            _adminDataAccess = new AdminDataAccess(ccContext);
            _userDataAccess = new UserDataAccess(ccContext);
        }

        public ActionResult Index()
        {

            if (HttpContext.Session.GetInt32("IsAdminConnected") == 1)
            {
                ViewBag.users = _userDataAccess.GetAllUsers();
                return View();
            }

            return View("Login");
        }

        public ActionResult Search(DateTime start, DateTime end, string userId)
        {
            try
            {
                var comments = _adminDataAccess.GetByUserIdInDateRange(start, end, userId);
                return View(comments);
            }
            catch
            {
                return RedirectToAction("Index", "Error");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(IFormCollection form)
        {
            if (ModelState.IsValid)
            {
                var username = form["Username"].ToString() ?? string.Empty;
                var password = form["Password"].ToString() ?? string.Empty;

                if (username == _settings.UserName && password == _settings.Password)
                {
                    return UpdateAdminState(true);
                }

            }

            ModelState.AddModelError("", "Invalid username or password!");
            return View();
        }

        [HttpPost]
        public ActionResult Logout()
        {
            return UpdateAdminState(false);
        }

        private ActionResult UpdateAdminState(bool isConnected)
        {
            HttpContext.Session.SetInt32("IsAdminConnected", isConnected ? 1 : 0);
            return RedirectToAction("Index", "Admin");
        }
    }
}

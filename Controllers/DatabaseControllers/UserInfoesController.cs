using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PracticalTraining.Models;
using PracticalTraining.Models.DatabaseMANKA;

namespace PracticalTraining.Controllers.DatabaseControllers
{
    public class UserInfoesController : Controller
    {
        private readonly MANKAContext _context;


        public UserInfoesController(MANKAContext context)
        {
            _context = context;
        }

        // GET: UserInfoes
        public IActionResult Index(string sortOrder, string searchString)
        {
            ViewBag.LoginSortParm = sortOrder == "login" ? "login desc" : "login";
            ViewBag.PasswordSortParm = sortOrder == "password" ? "password desc" : "password";
            ViewBag.AccessLevelSortParm = sortOrder == "accessLevel" ? "accessLevel desc" : "accessLevel";

            if (!String.IsNullOrEmpty(searchString))
            {
                UserInfoList.userInfoList = UserInfoList.userInfoList.Where(s => s.AccessLevel.ToUpper().Contains(searchString.ToUpper())
                                                                              || s.UserLogin.ToUpper().Contains(searchString.ToUpper())
                                                                              || s.UserPassword.ToUpper().Contains(searchString.ToUpper())).ToList();
            }
            else
            {
                UserInfoList.CreateList();
            }

            switch (sortOrder)
            {
                case "login":
                    UserInfoList.userInfoList = UserInfoList.userInfoList.OrderBy(s => s.UserLogin).ToList();
                    break;
                case "password":
                    UserInfoList.userInfoList = UserInfoList.userInfoList.OrderBy(s => s.UserPassword).ToList();
                    break;
                case "accessLevel":
                    UserInfoList.userInfoList = UserInfoList.userInfoList.OrderBy(s => s.AccessLevel).ToList();
                    break;
                case "login desc":
                    UserInfoList.userInfoList = UserInfoList.userInfoList.OrderByDescending(s => s.UserLogin).ToList();
                    break;
                case "password desc":
                    UserInfoList.userInfoList = UserInfoList.userInfoList.OrderByDescending(s => s.UserPassword).ToList();
                    break;
                case "accessLevel desc":
                    UserInfoList.userInfoList = UserInfoList.userInfoList.OrderByDescending(s => s.AccessLevel).ToList();
                    break;
                default:
                    UserInfoList.userInfoList = UserInfoList.userInfoList.OrderBy(s => s.AccessLevel).ToList();
                    break;
            }
            return View(UserInfoList.userInfoList);
        }

        // GET: UserInfoes/Edit/5
        public IActionResult Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userInfo = UserInfoList.userInfoList.FirstOrDefault(u => u.UserID == id);
            if (userInfo == null)
            {
                return NotFound();
            }
            return View(userInfo);
        }

        // POST: UserInfoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([Bind("UserLogin,AccessLevel,UserPassword")] UserInfo userInfo)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    userInfo.UpdateUser();
                    UserInfoList.CreateList();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(userInfo);
        }
    }
}

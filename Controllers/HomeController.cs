using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PracticalTraining.Models;
using PracticalTraining.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using PracticalTraining.Models.DatabaseMANKA;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PracticalTraining.Controllers
{
    public class HomeController : Controller
    {
        public static MainPageViewModel content;
        public static MANKAContext dbConnection;

        public HomeController()
        {
            content = new MainPageViewModel();
            dbConnection = new MANKAContext();
        }

        public IActionResult Index()
        {
            return View(content);
        }

        public RedirectToActionResult RegistrationWithReservation()
        {
            return RedirectToAction("Registrartion", "Home", new { reserve = true});
        }


        [HttpGet]
        public IActionResult Login(bool validLogin = true)
        {
            return View(validLogin);
        }

        [HttpPost]
        public RedirectToActionResult Login(string login, string password)
        {
            login = PhoneNumber.PhoneNumberDatabaseView(login);
            AuthorizedUserModel.AuthorizeUserModel(login, password);

            switch (AuthorizedUserModel.AccessLevel)
            {
                case 0:
                    return RedirectToAction("Index", "Guest");
                case 1:
                    return RedirectToAction("Index", "Admin");
                case 2:
                    return RedirectToAction("Index", "Owner");
                default:
                    return RedirectToAction("Login", "Home", new { validLogin = false });
            }
        }

        [HttpGet]
        public IActionResult Registration(bool validLogin = true, bool reserve = false)
        {
            return View(new bool[] { validLogin, reserve });
        }

        [HttpPost]
        public RedirectToActionResult Registration(string name, string famName, string phone, string mail, string socNet, string password, string cont)
        {
            phone = PhoneNumber.PhoneNumberDatabaseView(phone);
            bool continueReservation = bool.Parse(cont);

            try
            {
                dbConnection.GuestInfo.Add(new GuestInfo(name, famName, phone, mail, socNet, password));
                dbConnection.SaveChanges();
            }
            catch
            {
                return RedirectToAction("Registration", "Home", new { validLogin = false });
            }

            AuthorizedUserModel.AuthorizeUserModel(phone, password);
            if (continueReservation)
            {
                return RedirectToAction("Reservation", "Guest");
            }
            else
            {
                return RedirectToAction("Index", "Guest");
            }
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

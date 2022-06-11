using Microsoft.AspNetCore.Mvc;
using PracticalTraining.Models;
using PracticalTraining.Models.DatabaseMANKA;
using PracticalTraining.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PracticalTraining.Controllers
{
    public class AdminController: Controller
    {
        MANKAContext dbConnection;
        AdminModel adminModel;

        public AdminController()
        {
            dbConnection = new MANKAContext();
            adminModel = AuthorizedUserModel.AdminModel;
        }


        [HttpGet]
        public IActionResult Index()
        {
            return View(adminModel.AdminVM);
        }

        [HttpPost]
        public IActionResult Index(string id)
        {
            dbConnection = new MANKAContext();
            int reservationId = int.Parse(id);
            FinalReservations reservation = dbConnection.FinalReservations.FirstOrDefault(r => r.ReservationCode == reservationId);
            dbConnection.FinalReservations.Remove(reservation);
            dbConnection.SaveChanges();
            AuthorizedUserModel.AdminModel.UpdateAdminVM();
            adminModel = AuthorizedUserModel.AdminModel;
            return View(adminModel.AdminVM);
        }


        [HttpGet]
        public IActionResult AvailableWork()
        {
            return View(adminModel.AdminVM);
        }

        [HttpPost]
        public IActionResult AvailableWork(string id)
        {
            dbConnection = new MANKAContext();
            int reservationId = int.Parse(id);
            FinalReservations reservation = new FinalReservations();
            reservation.StaffPhone = adminModel.Admin.StaffPhone;
            reservation.ReservationCode = reservationId;
            dbConnection.FinalReservations.Add(reservation);
            dbConnection.SaveChanges();
            AuthorizedUserModel.AdminModel.UpdateAdminVM();
            adminModel = AuthorizedUserModel.AdminModel;
            return View(adminModel.AdminVM);
        }
    }
}

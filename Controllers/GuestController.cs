using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PracticalTraining.Models;
using PracticalTraining.Models.DatabaseMANKA;
using PracticalTraining.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace PracticalTraining.Controllers
{
    public class GuestController: Controller
    {
        MANKAContext dbConnection = new MANKAContext();
        GuestModel guestModel;


        public GuestController() 
        {
            guestModel = AuthorizedUserModel.GuestModel;
        }

        public IActionResult Index()
        {
            return View(guestModel.MainPageVM);
        }

        [HttpGet]
        public IActionResult Reservations()
        {
            return View(guestModel.GuestReservationsVM);
        }

        [HttpPost]
        public IActionResult Reservations(string id)
        {
            int reservationId = int.Parse(id);
            dbConnection.ReservationInfo.FirstOrDefault(r => r.ReservationCode == reservationId).CancelDate = DateTime.Now;
            dbConnection.SaveChanges();
            AuthorizedUserModel.GuestModel.UpdateGuestReservationsVM();
            guestModel = AuthorizedUserModel.GuestModel;
            return View(guestModel.GuestReservationsVM);
        }

        [HttpGet]
        public IActionResult Edit()
        {
            GuestInfo guest = guestModel.Guest;
            GuestEditViewModel info = new GuestEditViewModel(guest.GuestName, guest.GuestFamilyName, guest.GuestPhone, guest.GuestEmail, guest.GuestSocialNetwork);
            return View(info);
        }

        [HttpPost]
        public RedirectToActionResult Edit(string name, string famName, string mail, string socNet)
        {
            try
            {
                GuestInfo guest = guestModel.Guest;
                dbConnection.GuestInfo.Update(new GuestInfo(name, famName, guest.GuestPhone, mail, socNet, guest.RegistrationDate, guest.GuestPassword));
                dbConnection.SaveChanges();
                AuthorizedUserModel.UpdateGuest();
                guestModel = AuthorizedUserModel.GuestModel;
            }
            catch
            {
                return RedirectToAction("Edit", "Guest");
            }
            return RedirectToAction("Index", "Guest");
        }


        [HttpGet]
        public IActionResult Reservation()
        {
            return View(guestModel.ReservationVM);
        }

        [HttpPost]
        public IActionResult Reservation(string place, string date, string hourAmount, string time, string service, string payment, string peopleNumber, 
                                         string comment, string finalInput)
        {
            bool check = bool.Parse(finalInput);
            if (check)
            {
                ReservationInfo newReservation = new ReservationInfo(DateTime.Parse(date), new TimeSpan(int.Parse(time), 0, 0), 
                                                                     new TimeSpan(int.Parse(time) + int.Parse(hourAmount), 0, 0), comment, 
                                                                     short.Parse(place), short.Parse(service), short.Parse(payment), 
                                                                     guestModel.Guest.GuestPhone, short.Parse(peopleNumber));
                dbConnection.ReservationInfo.Add(newReservation);
                dbConnection.SaveChanges();
                AuthorizedUserModel.GuestModel.SetReservationVM();
                AuthorizedUserModel.GuestModel.UpdateGuestReservationsVM();
                guestModel = AuthorizedUserModel.GuestModel;
                return RedirectToAction("Index", "Guest");
            }
            else
            {
                guestModel.ReservationVM.UpdateReservationBaseInfo(place, date, hourAmount, time, service, payment, peopleNumber, comment);
                return RedirectToAction("Reservation", "Guest");
            }
        }
    }
}

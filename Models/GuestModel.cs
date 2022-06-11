using PracticalTraining.Models.DatabaseMANKA;
using PracticalTraining.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PracticalTraining.Models
{
    public class GuestModel
    {
        public string Name { get; set; }
        public MainPageViewModel MainPageVM { get; set; }
        public GuestEditViewModel GuestEditVM { get; set; }
        public GuestReservationsViewModel GuestReservationsVM { get; set; }
        public ReservationViewModel ReservationVM { get; set; }
        public GuestInfo Guest { get; set; }



        public GuestModel() { }


        public GuestModel(string login)
        {
            SetGuest(login);
            SetAttributes();
        }

        public void SetAttributes()
        {
            if (Guest != null)
            {
                UpdateName();
                UpdateMainPageVM();
                UpdateGuestEditVM();
                UpdateGuestReservationsVM();
                SetReservationVM();
            }
        }


        private void SetGuest(string login)
        {
            MANKAContext dbConnection = new MANKAContext();
            Guest = dbConnection.GuestInfo.FirstOrDefault(g => g.GuestPhone == login);
        }


        public void UpdateGuest()
        {
            MANKAContext dbConnection = new MANKAContext();
            Guest = dbConnection.GuestInfo.FirstOrDefault(g => g.GuestPhone == Guest.GuestPhone);
            UpdateName();
        }


        public void SetReservationVM()
        {
            ReservationVM = new ReservationViewModel(Name);
        }


        public void UpdateName()
        {
            Name = Guest.GuestName + " " + Guest.GuestFamilyName;
        }


        public void UpdateMainPageVM()
        {
            MainPageVM = new MainPageViewModel(Name);
        }


        public void UpdateGuestEditVM()
        {
            GuestEditVM = new GuestEditViewModel(Guest.GuestName, Guest.GuestFamilyName, Guest.GuestPhone, Guest.GuestEmail, Guest.GuestSocialNetwork);
        }


        public void UpdateGuestReservationsVM()
        {
            MANKAContext dbConnection = new MANKAContext();

            List<ReservationInfo> reservations = dbConnection.ReservationInfo.Where(r => r.GuestPhone == Guest.GuestPhone)
                                                                              .OrderByDescending(r => r.ReservationDate)
                                                                              .ToList();
            List<ReservationView> reservationsViewCurrent = reservations.Where(r => (r.CancelDate == null && r.ReservationDate > DateTime.Now))
                                                                        .Select(r => new ReservationView(r))
                                                                        .ToList();
            List<ReservationView> reservationsViewPast = reservations.Where(r => !(r.CancelDate == null && r.ReservationDate > DateTime.Now))
                                                                        .Select(r => new ReservationView(r))
                                                                        .ToList();
            GuestReservationsVM = new GuestReservationsViewModel(reservationsViewCurrent, reservationsViewPast, Name);
        }
    }
}

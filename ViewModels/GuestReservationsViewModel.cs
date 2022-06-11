using PracticalTraining.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PracticalTraining.ViewModels
{
    public class GuestReservationsViewModel
    {
        public List<ReservationView> ReservationsViewCurrent { get; set; }
        public List<ReservationView> ReservationsViewPast { get; set; }
        public string GuestName { get; set; }


        public GuestReservationsViewModel(List<ReservationView> reservationsViewCurrent, List<ReservationView> reservationsViewPast, string guestName)
        {
            ReservationsViewCurrent = reservationsViewCurrent;
            ReservationsViewPast = reservationsViewPast;
            GuestName = guestName;
        }
    }
}

using PracticalTraining.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PracticalTraining.ViewModels
{
    public class AdminViewModel
    {

        public string AdminPhone { get; set; }
        public string AdminName { get; set; }
        public List<ReservationView> ChosenReservations { get; set; }
        public List<ReservationView> AvailableReservations { get; set; }


        public AdminViewModel() { }

        public AdminViewModel(string adminPhone, string adminName)
        {
            AdminPhone = adminPhone;
            AdminName = adminName;
        }

        public AdminViewModel(string adminPhone, string adminName, List<ReservationView> chosenReservations)
        {
            AdminPhone = adminPhone;
            AdminName = adminName;
            ChosenReservations = chosenReservations;
        }

        public AdminViewModel(string adminPhone, string adminName, List<ReservationView> chosenReservations, List<ReservationView> availableReservations)
        {
            AdminPhone = adminPhone;
            AdminName = adminName;
            ChosenReservations = chosenReservations;
            AvailableReservations = availableReservations;
        }
    }
}

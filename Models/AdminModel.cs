using PracticalTraining.Models.DatabaseMANKA;
using PracticalTraining.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PracticalTraining.Models
{
    public class AdminModel
    {
        public string Name { get; set; }
        public StaffInfo Admin { get; set; }
        public AdminViewModel AdminVM { get; set; }

        public AdminModel() { }

        internal void SetAttributes()
        {
            UpdateName();
            UpdateAdminVM();
        }


        public AdminModel(string login)
        {
            UpdateAdmin(login);
            SetAttributes();
        }

        public void UpdateName()
        {
            Name = Admin.StaffFamilyName + " " + Admin.StaffName + " " + Admin.StaffSurname;
        }

        public void UpdateAdmin(string login)
        {
            MANKAContext dbConnection = new MANKAContext();
            Admin = dbConnection.StaffInfo.FirstOrDefault(s => s.StaffPhone == login);
        }

        public void UpdateAdminVM()
        {
            AdminVM = new AdminViewModel(Admin.StaffPhone, Name, GetAdminReservations(Admin), GetAvailableReservations(Admin));
        }


        private static List<ReservationView> GetAdminReservations(StaffInfo admin)
        {
            MANKAContext dbConnection = new MANKAContext();
            List<int> finalAdmin = dbConnection.FinalReservations.Where(r => r.StaffPhone == admin.StaffPhone).Select(r => r.ReservationCode).ToList();
            return dbConnection.ReservationInfo.Where(r => finalAdmin.Contains(r.ReservationCode) && r.CancelDate == null && r.ReservationDate >= DateTime.Now)
                                                                     .Select(r => new ReservationView(r))
                                                                     .ToList();
        }

        private static List<ReservationView> GetAvailableReservations(StaffInfo admin)
        {
            MANKAContext dbConnection = new MANKAContext();
            List<int> finalAll = dbConnection.FinalReservations.Select(r => r.ReservationCode).ToList();
            return dbConnection.ReservationInfo.Where(r => r.ReservationDate >= DateTime.Now && !finalAll.Contains(r.ReservationCode) && r.CancelDate == null)
                                                   .Select(r => new ReservationView(r))
                                                   .ToList();
        }
    }
}

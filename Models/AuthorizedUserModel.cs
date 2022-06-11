using PracticalTraining.Models.DatabaseMANKA;
using PracticalTraining.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PracticalTraining.Models
{
    public class AuthorizedUserModel
    {

        public static GuestModel GuestModel { get; set; }
        public static AdminModel AdminModel { get; set; }
        public static int AccessLevel { get; set; }


        public static void AuthorizeUserModel(string login, string password)
        {
            MANKAContext dbConnection = new MANKAContext();

            GuestModel = null;
            AdminModel = null;
            AccessLevel = -1;

            GuestInfo g = dbConnection.GuestInfo.FirstOrDefault(g => (g.GuestPhone == login) && (g.GuestPassword == password));
            if (g != null)
            {
                AccessLevel = 0;
                GuestModel = new GuestModel();
                GuestModel.Guest = g;
                GuestModel.SetAttributes();
                return;
            }

            if (dbConnection.BaseInfo.FirstOrDefault(s => (s.OwnerLogin == login) && (s.OwnerPassword == password)) != null)
            {
                AccessLevel = 2;
                return;
            }

            StaffInfo a = dbConnection.StaffInfo.FirstOrDefault(s => (s.StaffPhone == login) && (s.StaffPassword == password));
            if (a != null)
            {
                AccessLevel = 1;
                AdminModel = new AdminModel();
                AdminModel.Admin = a;
                AdminModel.SetAttributes();
                return;
            }
        }

        public static void UpdateGuest()
        {
            GuestModel = new GuestModel(GuestModel.Guest.GuestPhone);
        }

        public static void UpdateAdmin()
        {
            AdminModel = new AdminModel(AdminModel.Admin.StaffPhone);
        }
    }
}

using PracticalTraining.Models.DatabaseMANKA;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace PracticalTraining.Models
{
    public class UserInfoList
    {
        public static List<UserInfo> userInfoList;

        public static void CreateList()
        {
            MANKAContext dbConnection = new MANKAContext();
            userInfoList = dbConnection.GuestInfo.Select(g => new UserInfo(g.GuestPhone, g.GuestPassword, "Гость")).ToList();
            userInfoList = userInfoList.Concat(dbConnection.StaffInfo.Select(s => new UserInfo(s.StaffPhone, s.StaffPassword, "Администратор")).ToList()).ToList();
            userInfoList = userInfoList.Concat(dbConnection.BaseInfo.Select(o => new UserInfo(o.OwnerLogin, o.OwnerPassword, "Владелец фотостудии"))).ToList();
        }
    }

    public class UserInfo
    {        
        public string UserID { get; set; }

        [Required]
        public string UserLogin { get; set; }

        [Required (ErrorMessage = "Введите пароль")]
        public string UserPassword { get; set; }

        [Required]
        public string AccessLevel { get; set; }

        private static int _id = 0;

        public UserInfo() { }


        public UserInfo(string login, string password, string accessLevel)
        {
            UserID = _id.ToString();
            UserLogin = PhoneNumber.PhoneNumberNormalView(login);
            UserPassword = password;
            AccessLevel = accessLevel;
            _id++;
        }

        public void UpdateUser()
        {
            MANKAContext dbConnection = new MANKAContext();
            this.UserLogin = PhoneNumber.PhoneNumberDatabaseView(UserLogin);
            switch(this.AccessLevel)
            {
                case "Гость":
                    GuestInfo guest = dbConnection.GuestInfo.FirstOrDefault(g => g.GuestPhone == this.UserLogin);
                    if (guest != null)
                    {
                        guest.GuestPassword = this.UserPassword;
                        dbConnection.GuestInfo.Update(guest);
                    }
                    break;
                case "Администратор":
                    StaffInfo admin = dbConnection.StaffInfo.FirstOrDefault(a => a.StaffPhone == this.UserLogin);
                    if (admin != null)
                    {
                        admin.StaffPassword = this.UserPassword;
                        dbConnection.StaffInfo.Update(admin);
                    }
                    break;
                case "Владелец фотостудии":
                    BaseInfo owner = dbConnection.BaseInfo.FirstOrDefault(o => o.OwnerLogin == this.UserLogin);
                    if (owner != null)
                    {
                        owner.OwnerPassword = this.UserPassword;
                        dbConnection.BaseInfo.Update(owner);
                    }
                    break;
            }
            dbConnection.SaveChanges();
        }

    }
}

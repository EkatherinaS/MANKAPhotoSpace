using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PracticalTraining.Models.DatabaseMANKA
{
    public partial class GuestInfo
    {
        public GuestInfo()
        {
            ReservationInfo = new HashSet<ReservationInfo>();
        }

        [Required(ErrorMessage = "Введите номер телефона")]
        public string GuestPhone { get; set; }

        [Required(ErrorMessage = "Введите имя")]
        public string GuestName { get; set; }

        [Required(ErrorMessage = "Введите фамилию")]
        public string GuestFamilyName { get; set; }

        public string GuestEmail { get; set; }

        public string GuestSocialNetwork { get; set; }

        public DateTime RegistrationDate { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        public string GuestPassword { get; set; }

        public virtual ICollection<ReservationInfo> ReservationInfo { get; set; }


        public GuestInfo(string name, string famName, string phone, string mail, string socNet, string password)
        {
            GuestPhone = phone;
            GuestName = name;
            GuestFamilyName = famName;
            GuestEmail = mail;
            GuestSocialNetwork = socNet;
            RegistrationDate = DateTime.Now;
            GuestPassword = password;
        }


        public GuestInfo(string name, string famName, string phone, string mail, string socNet, DateTime regDate, string password)
        {
            GuestPhone = phone;
            GuestName = name;
            GuestFamilyName = famName;
            GuestEmail = mail;
            GuestSocialNetwork = socNet;
            RegistrationDate = regDate;
            GuestPassword = password;
        }
    }
}

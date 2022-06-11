using System;
using System.ComponentModel.DataAnnotations;

namespace PracticalTraining.ViewModels
{
    public class GuestEditViewModel
    {

        [Required(ErrorMessage = "Введите номер телефона")]
        public string GuestPhone { get; set; }

        [Required(ErrorMessage = "Введите имя")]
        public string GuestName { get; set; }

        [Required(ErrorMessage = "Введите фамилию")]
        public string GuestFamilyName { get; set; }
        public string GuestEmail { get; set; }
        public string GuestSocialNetwork { get; set; }

        public GuestEditViewModel() { }

        public GuestEditViewModel(string name, string famName, string phone, string mail, string socNet)
        {
            GuestPhone = phone;
            GuestName = name;
            GuestFamilyName = famName;
            GuestEmail = mail;
            GuestSocialNetwork = socNet;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PracticalTraining.Models.DatabaseMANKA
{
    public partial class StaffInfo
    {
        public StaffInfo()
        {
            FinalReservations = new HashSet<FinalReservations>();
        }
        public string StaffPhoneView;


        [Required(ErrorMessage = "Введите номер телефона администратора")]
        public string StaffPhone { get; set; }

        [Required(ErrorMessage = "Введите серию паспорта")]
        [StringLength(4)]
        public string PassportSeries { get; set; }

        [Required(ErrorMessage = "Введите номер паспорта")]
        [StringLength(6)]
        public string PassportNumber { get; set; }

        [Required(ErrorMessage = "Введите СНИЛС")]
        [StringLength(11)]
        public string Snils { get; set; }

        [Required(ErrorMessage = "Введите почасовую оплату")]
        [RegularExpression("^[0-9]*[.,]?[0-9]+$", ErrorMessage = "Почасовая оплата должна быть числом")]
        public decimal PaymentPerHour { get; set; }

        [Required(ErrorMessage = "Введите фамилию")]
        public string StaffFamilyName { get; set; }

        [Required(ErrorMessage = "Введите имя")]
        public string StaffName { get; set; }
        public string StaffSurname { get; set; }
        public DateTime? ResignationDate { get; set; }
            
        [Required(ErrorMessage = "Введите пароль")]
        public string StaffPassword { get; set; }

        public virtual ICollection<FinalReservations> FinalReservations { get; set; }
    }
}

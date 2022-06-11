using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PracticalTraining.Models.DatabaseMANKA
{
    public partial class ReservationInfo
    {
        public int ReservationCode { get; set; }

        [Required(ErrorMessage = "Выберите дату")]
        public DateTime ReservationDate { get; set; }

        [Required(ErrorMessage = "Выберите время")]
        public TimeSpan StartTime { get; set; }

        [Required(ErrorMessage = "Выберите время")]
        public TimeSpan FinishTime { get; set; }
        public string ReservationComment { get; set; }

        [Required(ErrorMessage = "Выберите поемещение")]
        public short PlaceCode { get; set; }

        [Required(ErrorMessage = "Выберите услугу")]
        public short ServiceCode { get; set; }

        [Required(ErrorMessage = "Выберите способ оплаты")]
        public short PaymentCode { get; set; }
        public DateTime? CancelDate { get; set; }

        [Required(ErrorMessage = "Введите номер телефона")]
        public string GuestPhone { get; set; }

        [Required(ErrorMessage = "Введите количество человек")]
        public short PeopleNumber { get; set; }

        public virtual GuestInfo GuestPhoneNavigation { get; set; }
        public virtual PaymentInfo PaymentCodeNavigation { get; set; }
        public virtual PlaceInfo PlaceCodeNavigation { get; set; }
        public virtual ServiceInfo ServiceCodeNavigation { get; set; }
        public virtual FinalReservations FinalReservations { get; set; }


        public ReservationInfo(DateTime reservationDate, TimeSpan startTime, TimeSpan finishTime, string reservationComment,
                               short placeCode, short serviceCode, short paymentCode, string guestPhone, short peopleNumber)
        {
            ReservationDate = reservationDate;
            StartTime = startTime;
            FinishTime = finishTime;
            ReservationComment = reservationComment;
            PlaceCode = placeCode;
            ServiceCode = serviceCode;
            PaymentCode = paymentCode;
            GuestPhone = guestPhone;
            PeopleNumber = peopleNumber;
        }
    }
}

using System;
using System.Collections.Generic;

namespace PracticalTraining.Models.DatabaseMANKA
{
    public partial class FinalReservations
    {
        public int ReservationCode { get; set; }

        public string StaffPhone { get; set; }

        public virtual ReservationInfo ReservationCodeNavigation { get; set; }
        public virtual StaffInfo StaffPhoneNavigation { get; set; }
    }
}

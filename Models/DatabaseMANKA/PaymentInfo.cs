using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PracticalTraining.Models.DatabaseMANKA
{
    public partial class PaymentInfo
    {
        public PaymentInfo()
        {
            ReservationInfo = new HashSet<ReservationInfo>();
        }

        public short PaymentCode { get; set; }

        [Required(ErrorMessage = "Введите название способа оплаты")]
        public string PaymentName { get; set; }
        public DateTime? DateExploration { get; set; }

        public virtual ICollection<ReservationInfo> ReservationInfo { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PracticalTraining.Models.DatabaseMANKA
{
    public partial class ServiceInfo
    {
        public ServiceInfo()
        {
            ReservationInfo = new HashSet<ReservationInfo>();
        }

        public short ServiceCode { get; set; }

        [Required(ErrorMessage = "Введите название услуги")]
        public string ServiceName { get; set; }

        [Required(ErrorMessage = "Введите цену")]
        public decimal ServicePrice { get; set; }
        public DateTime? DateExploration { get; set; }

        public virtual ICollection<ReservationInfo> ReservationInfo { get; set; }
    }
}

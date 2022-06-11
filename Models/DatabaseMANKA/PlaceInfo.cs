using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace PracticalTraining.Models.DatabaseMANKA
{
    public partial class PlaceInfo
    {
        public PlaceInfo()
        {
            ReservationInfo = new HashSet<ReservationInfo>();
        }

        public short PlaceCode { get; set; }

        [Required(ErrorMessage = "Введите название помещения")]
        public string PlaceName { get; set; }

        [Required(ErrorMessage = "Введите максимальное количество человек")]
        public short MaxPeopleNumber { get; set; }
        public DateTime? PlaceCloseDate { get; set; }

        [Required(ErrorMessage = "Введите адрес")]
        public short AddressCode { get; set; }

        public virtual BuildingInfo AddressCodeNavigation { get; set; }
        public virtual ICollection<ReservationInfo> ReservationInfo { get; set; }

    }
}

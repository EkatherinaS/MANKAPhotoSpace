using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PracticalTraining.Models.DatabaseMANKA
{
    public partial class BuildingInfo
    {
        public BuildingInfo()
        {
            PlaceInfo = new HashSet<PlaceInfo>();
        }

        public short AddressCode { get; set; }

        [Required(ErrorMessage = "Введите номер дома")]
        public string BuildingNumber { get; set; }
            
        [Required(ErrorMessage = "Введите улицу")]
        public short StreetCode { get; set; }

        public virtual StreetInfo StreetCodeNavigation { get; set; }
        public virtual ICollection<PlaceInfo> PlaceInfo { get; set; }


        public BuildingInfo(string buildingNumber, short streetCode)
        {
            BuildingNumber = buildingNumber;
            StreetCode = streetCode;
        }
    }
}

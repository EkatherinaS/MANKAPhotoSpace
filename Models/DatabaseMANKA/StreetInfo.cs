using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PracticalTraining.Models.DatabaseMANKA
{
    public partial class StreetInfo
    {
        public StreetInfo()
        {
            BuildingInfo = new HashSet<BuildingInfo>();
        }

        public short StreetCode { get; set; }

        [Required(ErrorMessage = "Введите название улыцы")]
        public string StreetName { get; set; }

        [Required(ErrorMessage = "Введите город")]
        public short CityCode { get; set; }

        public virtual CityInfo CityCodeNavigation { get; set; }
        public virtual ICollection<BuildingInfo> BuildingInfo { get; set; }


        public StreetInfo(string streetName, short cityCode)
        {
            StreetName = streetName;
            CityCode = cityCode;
        }
    }
}

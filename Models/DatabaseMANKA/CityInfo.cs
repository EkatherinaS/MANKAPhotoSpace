using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PracticalTraining.Models.DatabaseMANKA
{
    public partial class CityInfo
    {
        public CityInfo()
        {
            StreetInfo = new HashSet<StreetInfo>();
        }

        public short CityCode { get; set; }

        [Required(ErrorMessage = "Введите город")]
        public string CityName { get; set; }

        public virtual ICollection<StreetInfo> StreetInfo { get; set; }

        public CityInfo(string cityName)
        {
            CityName = cityName;
        }
    }
}

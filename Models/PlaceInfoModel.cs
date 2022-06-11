using PracticalTraining.Models.DatabaseMANKA;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace PracticalTraining.Models
{
    public class PlaceInfoModel
    {
        public short AddressCode { get; set; }
        public short PlaceCode { get; set; }


        [Required(ErrorMessage = "Введите название помещения")]
        public string PlaceName { get; set; }
        [Required(ErrorMessage = "Введите максимальное количество человек")]
        public short MaxPeopleNumber { get; set; }


        [Required (ErrorMessage = "Введите город")]
        public string City { get; set; }

        [Required(ErrorMessage = "Введите улицу")]
        public string Street { get; set; }

        [Required(ErrorMessage = "Введите номер здания")]
        public string Building { get; set; }



        [Required(ErrorMessage = "Введите город")]
        public List<CityInfo> Cities { get; set; }

        [Required(ErrorMessage = "Введите улицу")]
        public List<StreetInfo> Streets { get; set; }

        [Required(ErrorMessage = "Введите номер здания")]
        public List<BuildingInfo> Buildings { get; set; }


        public PlaceInfoModel(PlaceInfo place)
        {
            MANKAContext dbConnection = new MANKAContext();

            PlaceCode = place.PlaceCode;
            PlaceName = place.PlaceName;
            MaxPeopleNumber = place.MaxPeopleNumber;
            AddressCode = place.AddressCode;

            Cities = dbConnection.CityInfo.ToList();
            Streets = dbConnection.StreetInfo.ToList();
            Buildings = dbConnection.BuildingInfo.ToList();

            int code;

            Building = Buildings.FirstOrDefault(b => b.AddressCode == place.AddressCode).BuildingNumber;
            code = Buildings.FirstOrDefault(b => b.AddressCode == place.AddressCode).StreetCode;
            Street = Streets.FirstOrDefault(s => s.StreetCode == code).StreetName;
            code = int.Parse(Streets.FirstOrDefault(s => s.StreetCode == code).CityCode.ToString());
            City = Cities.FirstOrDefault(c => c.CityCode == code).CityName;
        }

        public PlaceInfoModel() { }

        public void CreateBase()
        {
            MANKAContext dbConnection = new MANKAContext();

            Cities = dbConnection.CityInfo.ToList();
            Streets = dbConnection.StreetInfo.ToList();
            Buildings = dbConnection.BuildingInfo.ToList();
        }

        public short UpdateAddress()
        {
            MANKAContext dbConnection = new MANKAContext();

            CityInfo city = dbConnection.CityInfo.FirstOrDefault(c => c.CityName == City);
            if (city == null) 
            { 
                dbConnection.CityInfo.Add(new CityInfo(City));
                dbConnection.SaveChanges();
                city = dbConnection.CityInfo.FirstOrDefault(c => c.CityName == City);
            }

            StreetInfo street = dbConnection.StreetInfo.FirstOrDefault(c => c.StreetName == Street);
            if (street == null || street.CityCode != city.CityCode)
            {
                dbConnection.StreetInfo.Add(new StreetInfo(Street, city.CityCode));
                dbConnection.SaveChanges();
                street = dbConnection.StreetInfo.FirstOrDefault(s => s.StreetName == Street && s.CityCode == city.CityCode);
            }

            BuildingInfo building = dbConnection.BuildingInfo.FirstOrDefault(c => c.BuildingNumber == Building);
            if (building == null || building.StreetCode != street.StreetCode)
            {
                dbConnection.BuildingInfo.Add(new BuildingInfo(Building, street.StreetCode));
                dbConnection.SaveChanges();
                building = dbConnection.BuildingInfo.FirstOrDefault(b => b.BuildingNumber == Building && b.StreetCode == street.StreetCode);
            }

            return building.AddressCode;
        }
    }
}

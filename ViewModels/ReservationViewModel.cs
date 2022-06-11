using PracticalTraining.Models;
using System.Collections.Generic;
using PracticalTraining.Models.DatabaseMANKA;
using System.Linq;
using System;
using System.ComponentModel.DataAnnotations;

namespace PracticalTraining.ViewModels
{
    public class ReservationViewModel
    {
        public string GuestName { get; set; }

        public List<ElementWithId> Places { get; set; }
        public List<ElementWithId> Services { get; set; }
        public List<ElementWithId> Payments { get; set; }
        public List<ElementWithId> Times { get; set; }


        [Required(ErrorMessage = "Выберите поемещение")]
        public string Place { get; set; }
        public string MaxNumber { get; set; }

        [Required(ErrorMessage = "Выберите дату")]
        public string Date { get; set; }

        [Required(ErrorMessage = "Введите количество часов")]
        public string HourAmount { get; set; }

        [Required(ErrorMessage = "Выберите время")]
        public string Time { get; set; }

        [Required(ErrorMessage = "Выберите услугу")]
        public string Service { get; set; }

        [Required(ErrorMessage = "Выберите способ оплаты")]
        public string Payment { get; set; }

        [Required(ErrorMessage = "Введите количество человек")]
        public string PeopleNumber { get; set; }
        public string Comment { get; set; }


        public ReservationViewModel(string guestName)
        {
            GuestName = guestName;

            SetPlaces();
            SetMaxNumber();
            SetServices();
            SetPayments();

            SetBaseInfo();
            SetTimes();
        }



        public void UpdateReservationBaseInfo(string place, string date, string hourAmount, string time, string service, string payment, string peopleNumber, string comment)
        {
            Place = place;
            Date = date;
            HourAmount = hourAmount;
            Time = time;
            Service = service;
            PeopleNumber = peopleNumber;
            Comment = comment;
            Payment = payment;
            Times = GetAvailableTime();
            MaxNumber = GetMaxNumber();
        }

        private string GetMaxNumber()
        {
            int placeId = int.Parse(Place);
            return Places.FirstOrDefault(p=>p.Id == placeId).AddditionalInfo.ToString();
        }

        private List<ElementWithId> GetAvailableTime()
        {
            MANKAContext dbConnection = new MANKAContext();

            List<ElementWithId> availableHours = new List<ElementWithId>();
            List<ReservationInfo> reservationInfos = dbConnection.ReservationInfo.ToList()
                                                                                  .Where(r => (r.CancelDate == null &&
                                                                                               r.ReservationDate.ToString("yyyy-MM-dd") == Date &&
                                                                                               r.PlaceCode == short.Parse(Place)))
                                                                                  .OrderBy(r => r.StartTime)
                                                                                  .ToList();

            int duration = int.Parse(HourAmount);
            int currentReservation = 0;
            TimeSpan start;
            TimeSpan finish;
            ReservationInfo reservationInfo;

            if (reservationInfos.Count > 0)
            {
                for (int i = 9; i < 23 - duration; i++)
                {
                    start = new TimeSpan(i, 0, 0);
                    finish = new TimeSpan(i + duration, 0, 0);
                    reservationInfo = reservationInfos[currentReservation];

                    if (reservationInfo.FinishTime == start && currentReservation < reservationInfos.Count - 1) { currentReservation++; }

                    if ((start < reservationInfo.StartTime && finish <= reservationInfo.StartTime) ||
                        (start >= reservationInfo.FinishTime && finish > reservationInfo.FinishTime))
                    {
                        availableHours.Add(new ElementWithId(i, start.ToString(@"hh\:mm") + " - " + finish.ToString(@"hh\:mm")));
                    }
                }
            }
            else
            {
                for (int i = 9; i < 23 - duration; i++)
                {
                    start = new TimeSpan(i, 0, 0);
                    finish = new TimeSpan(i + duration, 0, 0);
                    availableHours.Add(new ElementWithId(i, start.ToString(@"hh\:mm") + " - " + finish.ToString(@"hh\:mm")));
                }
            }

            return availableHours;
        }


        private void SetBaseInfo()
        {
            Date = DateTime.Now.ToString("yyyy-MM-dd");
            HourAmount = "1";
            PeopleNumber = "1";
            Place = Places[0].Id.ToString();
        }


        private void SetTimes()
        {
            Times = GetAvailableTime();
        }


        private void SetPayments()
        {
            MANKAContext dbConnection = new MANKAContext();
            Payments = dbConnection.PaymentInfo.Where(p => p.DateExploration == null).Select(p => new ElementWithId(p.PaymentCode, p.PaymentName)).ToList();
        }


        private void SetServices()
        {
            MANKAContext dbConnection = new MANKAContext();
            Services = dbConnection.ServiceInfo.Where(s => s.DateExploration == null).Select(s => new ElementWithId(s.ServiceCode, s.ServiceName)).ToList();
        }


        private void SetPlaces()
        {
            MANKAContext dbConnection = new MANKAContext();

            List<PlaceInfo> places = dbConnection.PlaceInfo.Where(p => p.PlaceCloseDate == null).ToList();
            int placeCount = places.Count;
            Places = new List<ElementWithId>();

            int[] placeId = new int[placeCount];
            int[] placePeopleNum = new int[placeCount];
            string[] placeName = new string[placeCount];

            int[] id = new int[placeCount];
            string[] placeBuilding = new string[placeCount];
            string[] placeStreet = new string[placeCount];
            string[] placeCity = new string[placeCount];

            for (int i = 0; i < placeCount; i++)
            {
                id[i] = places[i].AddressCode;
                placeId[i] = places[i].PlaceCode;
                placeName[i] = places[i].PlaceName;
                placePeopleNum[i] = places[i].MaxPeopleNumber;
            }

            for (int i = 0; i < placeCount; i++)
            {
                BuildingInfo building = dbConnection.BuildingInfo.FirstOrDefault(b => b.AddressCode == id[i]);
                id[i] = building.StreetCode;
                placeBuilding[i] = building.BuildingNumber.ToString();
            }

            for (int i = 0; i < placeCount; i++)
            {
                StreetInfo street = dbConnection.StreetInfo.FirstOrDefault(s => s.StreetCode == id[i]);
                id[i] = (int)street.CityCode;
                placeStreet[i] = street.StreetName;
            }

            for (int i = 0; i < placeCount; i++)
            {
                placeCity[i] = dbConnection.CityInfo.FirstOrDefault(c => c.CityCode == id[i]).CityName;
                placeName[i] += " - г. " + placeCity[i] + ", ул. " + placeStreet[i] + ", " + placeBuilding[i];
                Places.Add(new ElementWithId(placeId[i], placeName[i], placePeopleNum[i]));
            }
        }


        private void SetMaxNumber()
        {
            if (Places.Count > 0)
            {
                MaxNumber = Places[0].AddditionalInfo.ToString();
            }
            else
            {
                MaxNumber = "0";
            }
        }
    }
}

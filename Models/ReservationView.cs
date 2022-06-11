using System;
using System.Linq;
using PracticalTraining.Models.DatabaseMANKA;

namespace PracticalTraining.Models
{
    public class ReservationView
    {
        public int id { get; set; }
        public string ReservationDate { get; set; }
        public string Time { get; set; }
        public string Place { get; set; }
        public string Service { get; set; }
        public string Cancelled { get; set; }
        public string GuestName { get; set; }
        public string GuestPhone { get; set; }
        public string GuestSocNet { get; set; }
        public string GuestMail { get; set; }
        public string AdminPhone { get; set; }
        public string Comment { get; set; }

        public ReservationView(ReservationInfo reservation)
        {
            MANKAContext dbConnection = new MANKAContext();
            id = reservation.ReservationCode;
            FinalReservations final = dbConnection.FinalReservations.FirstOrDefault(x => x.ReservationCode == reservation.ReservationCode);
            if (final != null)
            {
                AdminPhone = final.StaffPhone;
            }
            else
            {
                AdminPhone = "";
            }

            Comment = reservation.ReservationComment;
            GuestInfo guest = dbConnection.GuestInfo.FirstOrDefault(g => g.GuestPhone == reservation.GuestPhone);
            GuestName = guest.GuestName + " " + guest.GuestFamilyName;
            guest.GuestPhone = PhoneNumber.PhoneNumberNormalView(reservation.GuestPhone);

            GuestPhone = guest.GuestPhone;
            if (guest.GuestSocialNetwork == null)
            {
                GuestSocNet = "Нет информации о соц. сетях";
            }
            else
            {
                GuestSocNet = guest.GuestSocialNetwork;
            }
            if (guest.GuestSocialNetwork == null)
            {
                GuestMail = "Нет информации об электронной почте";
            }
            else
            {
                GuestMail = guest.GuestEmail;
            }
            ReservationDate = reservation.ReservationDate.ToString("dd/MM/yyyy");
            Time = reservation.StartTime.ToString(@"hh\:mm") + " - " + reservation.FinishTime.ToString(@"hh\:mm");

            PlaceInfo place = dbConnection.PlaceInfo.FirstOrDefault(p => p.PlaceCode == reservation.PlaceCode);
            BuildingInfo buildingInfo = dbConnection.BuildingInfo.FirstOrDefault(p => p.AddressCode == place.AddressCode);
            StreetInfo streetInfo = dbConnection.StreetInfo.FirstOrDefault(p => p.StreetCode == buildingInfo.StreetCode);
            CityInfo cityInfo = dbConnection.CityInfo.FirstOrDefault(p => p.CityCode == streetInfo.CityCode);

            Place = place.PlaceName + " г." + cityInfo.CityName + " ул." + streetInfo.StreetName + " " + buildingInfo.BuildingNumber;
            Service = dbConnection.ServiceInfo.FirstOrDefault(p => p.ServiceCode == reservation.ServiceCode).ServiceName;
            if (dbConnection.ReservationInfo.FirstOrDefault(p => p.ReservationCode == reservation.ReservationCode).CancelDate != null)
            {
                Cancelled = "Отменено";
            }
            else
            {
                Cancelled = "";
            }
        }
    }
}

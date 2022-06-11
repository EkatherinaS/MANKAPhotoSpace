using PracticalTraining.Models.DatabaseMANKA;
using System;
using System.Linq;

namespace PracticalTraining.ViewModels
{
    public class MainPageViewModel
    {
        public string GuestName { get; set; }
        public string[] Imgs { get; set; }
        public string BaseText { get; set; }
        public string[] ListText { get; set; }


        public MainPageViewModel()
        {
            MANKAContext dbConnection = new MANKAContext();

            string[] text = dbConnection.BaseInfo.First().BasicText.Split("\n");
            ListText = new ArraySegment<string>(text, 1, text.Length - 1).ToArray();
            Imgs = new string[] { "~/Content/img1.jpg", "~/Content/img2.jpg", "~/Content/img3.jpg", "~/Content/img4.jpg", "~/Content/img5.jpg" };
            BaseText = text[0];
        }


        public MainPageViewModel(string guestName)
        {
            MANKAContext dbConnection = new MANKAContext();

            string[] text = dbConnection.BaseInfo.First().BasicText.Split("\n");
            ListText = new ArraySegment<string>(text, 1, text.Length - 1).ToArray();
            Imgs = new string[] { "~/Content/img1.jpg", "~/Content/img2.jpg", "~/Content/img3.jpg", "~/Content/img4.jpg", "~/Content/img5.jpg" };
            BaseText = text[0];
            GuestName = guestName;
        }
    }
}

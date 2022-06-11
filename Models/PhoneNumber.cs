using System;
using System.Text.RegularExpressions;

namespace PracticalTraining.Models
{
    public class PhoneNumber
    {
        public static string PhoneNumberNormalView(string number)
        {
            number = "+7 (" + number.Substring(0, 3) + ") " + number.Substring(3, 3) + "-" + number.Substring(6, 2) + "-" + number.Substring(8, 2);
            return number;
        }

        public static string PhoneNumberDatabaseView(string number)
        {
            number = Regex.Replace(number, @"[ \-)(]", "");
            number = number.Replace("+7", "");
            return number;
        }
    }
}

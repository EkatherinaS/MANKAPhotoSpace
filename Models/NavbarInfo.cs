using System;
using System.Collections.Generic;

namespace PracticalTraining.Models
{
    public class NavbarInfo: List<NavbarItem>
    {
        public NavbarInfo() { }
    }

    public class NavbarItem
    {
        string Controller { get; set; }
        string Action { get; set; }
        string Text { get; set; }

        public NavbarItem(string controller, string action, string text) 
        {
            Controller = controller;
            Action = action;
            Text = text;
        }
    }
}

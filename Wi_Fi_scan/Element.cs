using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Wi_Fi_scan
{
    class Element
    {

        string title;
        string security;
        int level;

        public Element(string title, string security, int level)
        {
            this.title = title;
            this.security = security;
            this.level = level;
        }

        public string Title { get => title; set => title = value; }
        public string Security { get => security; set => security = value; }
        public int Level { get => level; set => level = value; }
    }
}
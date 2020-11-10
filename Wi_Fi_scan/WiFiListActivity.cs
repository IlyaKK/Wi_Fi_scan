using System;
using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Widget;



namespace Wi_Fi_scan
{
    [Activity(Label = "WiFiListActivity")]
    public class WiFiListActivity : ListActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            var scanResultList = Intent.Extras.GetStringArrayList("wifi_list") ?? new string[0];
            this.ListAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, scanResultList);
        }
    }
}
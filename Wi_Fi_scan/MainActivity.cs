using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Net.Wifi;
using Android.Content;
using System.Collections.Generic;
using System.Linq;
using static Android.Resource;
using Android.Views;
using System;
using Android.Support.Design.Widget;

namespace Wi_Fi_scan
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    
    public class MainActivity : AppCompatActivity
    {
        static Element[] nets;
        private WifiManager wifiManager;
        private IList<ScanResult> wifilist;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            FloatingActionButton fab = (FloatingActionButton)FindViewById(Resource.Id.btn_wifi);

            fab.Click += (sender, e) =>
            {
                detectWifi();
            }; 
        }

        public void detectWifi()
        {
            this.wifiManager = (WifiManager)GetSystemService(Context.WifiService);
            this.wifiManager.StartScan();
            this.wifilist = this.wifiManager.ScanResults;

            nets = new Element[wifilist.Count];
            int i = 0;
            foreach (ScanResult wifinetwork in wifilist)
            {
                string ssid = wifinetwork.Ssid;
                string security = wifinetwork.Bssid;
                int level = wifinetwork.Level;
                nets[i] = new Element(ssid, security, level);
                i++;
            }
            /*for (int i = 0; i < wifilist.Count; i++)
            {
                string item = wifilist[i].ToString();
                string[] vector_item = item.Split(",");
                string item_essid = vector_item[0];
                string item_capabilities = vector_item[2];
                string item_level = vector_item[3];
                string ssid = item_essid.Split(": ")[1];
                string security = item_capabilities.Split(": ")[1];
                string level = item_level.Split(":")[1];
                nets[i] = new Element(ssid, security, level);
            }*/

            AdapterElements adapterElements = new AdapterElements(this);
            ListView netList = (ListView)FindViewById(Resource.Id.listItems);
            netList.Adapter = adapterElements;
        }

        public class AdapterElements:ArrayAdapter<Object>
        {
            Activity context;
            public AdapterElements(Activity context) : base(context, Resource.Layout.items, nets)
            {
                this.context = context;
            }
            public override View GetView(int position, View convertView, ViewGroup parent)
            {
                LayoutInflater inflater = context.LayoutInflater;
                View item = inflater.Inflate(Resource.Layout.items, null);

                TextView tvSsid = (TextView)item.FindViewById(Resource.Id.tvSSID);
                tvSsid.Text = nets[position].Title;

                TextView tvSecurity = (TextView)item.FindViewById(Resource.Id.tvSecurity);
                tvSecurity.Text = nets[position].Security;

                TextView tvLevel = (TextView)item.FindViewById(Resource.Id.tvLevel);
                int level = nets[position].Level;

                int i = level;
                if (i > -50)
                {
                    tvLevel.Text = "Высокий";
                }
                else if (i <= -50 && i > -80)
                {
                    tvLevel.Text = "Средний";
                }
                else if (i <= -80)
                {
                    tvLevel.Text = "Низкий";
                }
                return item;
            }
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}
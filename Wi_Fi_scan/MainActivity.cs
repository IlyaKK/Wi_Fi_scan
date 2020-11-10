using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Net.Wifi;
using Android.Content;
using System.Collections.Generic;
using System.Linq;

namespace Wi_Fi_scan
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        static List<string> scanResultList = new List<string>();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            Button btnWiFi = FindViewById<Button>(Resource.Id.btn_wifi);

            btnWiFi.Click += (sender, e) =>
            {

                var intent = new Intent(this, typeof(WiFiListActivity));
                WifiManager wifi = (WifiManager)GetSystemService(Context.WifiService);
                ScanReceiver scanReceiver = new ScanReceiver();
                RegisterReceiver(scanReceiver, new IntentFilter(WifiManager.ScanResultsAvailableAction));
                wifi.StartScan();
                intent.PutStringArrayListExtra("wifi_list", scanResultList);
                StartActivity(intent);
            };
            
        }
        //[BroadcastReceiver(Enabled = true, Exported = false)]
        public class ScanReceiver : BroadcastReceiver
        {
            public override void OnReceive(Context context, Intent intent)
            {
                WifiManager wifi = (WifiManager)Android.App.Application.Context.GetSystemService(Context.WifiService);
                IList<ScanResult> scanwifinetworks = wifi.ScanResults;
                foreach (ScanResult wifinetwork in scanwifinetworks)
                {
                    scanResultList.Add(wifinetwork.Ssid);
                }
            }
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}
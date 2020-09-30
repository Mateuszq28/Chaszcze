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

namespace Chaszcze
{
    [Activity(Label = "Akcje")]
    public class Akcje : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.akcje_);
            // Create your application here

            Button pk1 = FindViewById<Button>(Resource.Id.button1);

            pk1.Click += (sender, e) =>
            {
                // Translate user's alphanumeric phone number to numeric
                var intent = new Intent(this, typeof(qrakcja));
                StartActivity(intent);
            };
        }
    }
}
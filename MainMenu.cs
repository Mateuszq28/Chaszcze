﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

//using Android.Support.V7.App;

namespace Chaszcze
{
    [Activity(Label = "MainMenu")]
    public class MainMenu : Activity
    {
       

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.main_menu);


            TimePicker timePicker = FindViewById<TimePicker>(Resource.Id.timePicker);


            timePicker.SetIs24HourView((Java.Lang.Boolean)true);
            Button Start = FindViewById<Button>(Resource.Id.button1);

            // Add code to translate number
            Start.Click += (sender, e) =>
            {
                // Translate user's alphanumeric phone number to numeric
                var intent = new Intent(this, typeof(Akcje));
                StartActivity(intent);
            };


        }

      

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

    }
}
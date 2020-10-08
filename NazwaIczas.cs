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
    [Activity(Label = "Nazwa i czas")]
    public class NazwaIczas : Activity
    {
       
        //Metoda wywołuje się w momencie tworzenia obiektu
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.nazwaiczas_);

            //Przypisz zmienne elementom interfejsu
            TimePicker timePicker = FindViewById<TimePicker>(Resource.Id.timePicker);
            var text = FindViewById<EditText>(Resource.Id.textInputEditText1);
            Button Start = FindViewById<Button>(Resource.Id.button1);

            //Ustaw zegarek na 24-godzinny
            timePicker.SetIs24HourView((Java.Lang.Boolean)true);
            

            //Zaczyna nową grę - reset wszystkiego
            Start.Click += (sender, e) =>
            {
                if (text.Text.Length >= 1)
                {
                    //Ustaw zmienne
                    Zarzadzanie.reset();
                    Zarzadzanie.nazwaPatrolu = text.Text;
                    Zarzadzanie.minutaStartowa = DateTime.Parse(timePicker.CurrentHour + ":" + timePicker.CurrentMinute);
                    Zarzadzanie.czasRozpoczecia = DateTime.Now;
                    Zarzadzanie.czyGraTrwa = true;
                    //Zapisz grę
                    Akcje.SaveCountAsync();
                    
                    var intent = new Intent(this, typeof(Akcje));
                    StartActivity(intent);
                }
                else
                {
                    Toast.MakeText(this, "Podaj nazwę patrolu!", ToastLength.Long).Show();
                }
            };
        }


        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

    }
}
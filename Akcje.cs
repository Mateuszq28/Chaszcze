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

using Android.Graphics;

namespace Chaszcze
{
    [Activity(Label = "Akcje")]
    public class Akcje : Activity
    {
        static public Button pk1, pk2, pk3, pk4, pk5, pk6, pk7, pk8, pk9, pk10, pk11, pk12;
        static Button zakoncz;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.akcje_);
            // Create your application here

          

            pk1 = FindViewById<Button>(Resource.Id.button1);
            pk2 = FindViewById<Button>(Resource.Id.button2);
            pk3 = FindViewById<Button>(Resource.Id.button3);
            pk4 = FindViewById<Button>(Resource.Id.button4);
            pk5 = FindViewById<Button>(Resource.Id.button5);
            pk6 = FindViewById<Button>(Resource.Id.button6);
            pk7 = FindViewById<Button>(Resource.Id.button7);
            pk8 = FindViewById<Button>(Resource.Id.button8);
            pk9 = FindViewById<Button>(Resource.Id.button9);
            pk10 = FindViewById<Button>(Resource.Id.button10);
            pk11 = FindViewById<Button>(Resource.Id.button11);
            pk12 = FindViewById<Button>(Resource.Id.button12);
            zakoncz = FindViewById<Button>(Resource.Id.button13);
            TextView textGodz = FindViewById<TextView>(Resource.Id.textView1);

            Zarzadzanie.dekonstruktor();
            textGodz.Text += " " + Zarzadzanie.czasRozpoczecia.ToString("hh:mm") + " (" + Zarzadzanie.minutaStartowa.ToString("hh:mm") + ")";

            EditText podsumowanie = FindViewById<EditText>(Resource.Id.podsumowanie);

            pk1.Click += (sender, e) =>
            {
                // Translate user's alphanumeric phone number to numeric
                var intent = new Intent(this, typeof(qrakcja));
                string nr = "1";
                intent.PutExtra("nrPunktu", nr);
                StartActivity(intent);
            };
            pk2.Click += (sender, e) =>
            {
                // Translate user's alphanumeric phone number to numeric
                var intent = new Intent(this, typeof(qrakcja));
                string nr= "2";
                intent.PutExtra("nrPunktu", nr);
                StartActivity(intent);
            };
            pk3.Click += (sender, e) =>
            {
                // Translate user's alphanumeric phone number to numeric
                var intent = new Intent(this, typeof(qrakcja));
                string nr = "3";
                intent.PutExtra("nrPunktu", nr);
                StartActivity(intent);
            };
            pk4.Click += (sender, e) =>
            {
                // Translate user's alphanumeric phone number to numeric
                var intent = new Intent(this, typeof(qrakcja));
                string nr = "4";
                intent.PutExtra("nrPunktu", nr);
                StartActivity(intent);
            };
            pk5.Click += (sender, e) =>
            {
                // Translate user's alphanumeric phone number to numeric
                var intent = new Intent(this, typeof(qrakcja));
                string nr = "5";
                intent.PutExtra("nrPunktu", nr);
                StartActivity(intent);
            };
            pk6.Click += (sender, e) =>
            {
                // Translate user's alphanumeric phone number to numeric
                var intent = new Intent(this, typeof(qrakcja));
                string nr = "6";
                intent.PutExtra("nrPunktu", nr);
                StartActivity(intent);
            };
            pk7.Click += (sender, e) =>
            {
                // Translate user's alphanumeric phone number to numeric
                var intent = new Intent(this, typeof(qrakcja));
                string nr = "7";
                intent.PutExtra("nrPunktu", nr);
                StartActivity(intent);
            };
            pk8.Click += (sender, e) =>
            {
                // Translate user's alphanumeric phone number to numeric
                var intent = new Intent(this, typeof(qrakcja));
                string nr = "8";
                intent.PutExtra("nrPunktu", nr);
                StartActivity(intent);
            };
            pk9.Click += (sender, e) =>
            {
                // Translate user's alphanumeric phone number to numeric
                var intent = new Intent(this, typeof(qrakcja));
                string nr = "9";
                intent.PutExtra("nrPunktu", nr);
                StartActivity(intent);
            };
            pk10.Click += (sender, e) =>
            {
                // Translate user's alphanumeric phone number to numeric
                var intent = new Intent(this, typeof(qrakcja));
                string nr = "10";
                intent.PutExtra("nrPunktu", nr);
                StartActivity(intent);
            };
            pk11.Click += (sender, e) =>
            {
                // Translate user's alphanumeric phone number to numeric
                var intent = new Intent(this, typeof(qrakcja));
                string nr = "11";
                intent.PutExtra("nrPunktu", nr);
                StartActivity(intent);
            };
            pk12.Click += (sender, e) =>
            {
                // Translate user's alphanumeric phone number to numeric
                var intent = new Intent(this, typeof(qrakcja));
                string nr = "12";
                intent.PutExtra("nrPunktu", nr);
                StartActivity(intent);
            };

            

            zakoncz.Click += (sender, e) =>
            {
                if (Zarzadzanie.czyGraTrwa)
                {
                    Android.App.AlertDialog.Builder dialog = new AlertDialog.Builder(this);
                    AlertDialog alert = dialog.Create();
                    alert.SetTitle("Uwaga!");
                    alert.SetMessage("Czy na pewno chcesz zakończyć grę?");
                    alert.SetButton("TAK", (c, ev) =>
                    {
                        Zarzadzanie.zakonczenie();
                        podsumowanie.Text = "asdasd";

                    });
                    alert.SetButton2("ANULUJ", (c, ev) => { });
                    alert.Show();
                }
                else
                {
                    Toast.MakeText(this, "Gra się już zakończyła!", ToastLength.Long).Show();
                }
            };


        }

        static public void zmienKolor(String nr, string option)
        {
            Button pk;
            switch (nr)
            {
                case "1":
                    pk = pk1;
                    break;
                case "2":
                    pk = pk2;
                    break;
                case "3":
                    pk = pk3;
                    break;
                case "4":
                    pk = pk4;
                    break;
                case "5":
                    pk = pk5;
                    break;
                case "6":
                    pk = pk6;
                    break;
                case "7":
                    pk = pk7;
                    break;
                case "8":
                    pk = pk8;
                    break;
                case "9":
                    pk = pk9;
                    break;
                case "10":
                    pk = pk10;
                    break;
                case "11":
                    pk = pk11;
                    break;
                case "12":
                    pk = pk12;
                    break;

                default:
                    pk = pk1;
                    break;
            }

            switch (option)
            {
                case "red":
                    pk.SetBackgroundColor(Color.Red);
                    break;
                case "green":
                    pk.SetBackgroundColor(Color.Green);
                    break;
                case "yellow":
                    pk.SetBackgroundColor(Color.Yellow);
                    break;
                case "orange":
                    pk.SetBackgroundColor(Color.Orange);
                    break;
                case "black":
                    pk.SetBackgroundColor(Color.Black);
                    pk.SetTextColor(Color.White);
                    break;
                case "white":
                    pk.SetBackgroundColor(Color.White);
                    break;
                default:
                    pk.SetBackgroundColor(Color.LightGray);
                    break;
            }
            
        }
        


    }
}
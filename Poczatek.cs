using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Content;


namespace Chaszcze
{
    //[Activity(Label = "Chaszcze", Theme = "@style/AppTheme", MainLauncher = true)]
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class Poczatek : AppCompatActivity
    {
        
        //Metoda wywoływana podczas tworzenia obiektu
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.poczatek_);

            //Przypisz elementy interfejsu do zmiennych
            Button NowaGra = FindViewById<Button>(Resource.Id.button1);
            Button Wczytaj = FindViewById<Button>(Resource.Id.button2);


            //Przypisz przyciskom funkcje
            NowaGra.Click += (sender, e) =>
            {
                var intent = new Intent(this, typeof(NazwaIczas));
                StartActivity(intent);
                this.Finish();
            };

            Wczytaj.Click += (sender, e) =>
            {
                var intent = new Intent(this, typeof(Akcje));
                StartActivity(intent);
                this.Finish();
            };

            Zarzadzanie.ReadGame();
            if (Zarzadzanie.czyGraTrwa)
            {
                var intent = new Intent(this, typeof(Akcje));
                StartActivity(intent);
                this.Finish();
            }
        }


        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}
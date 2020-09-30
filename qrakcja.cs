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
using Android.Support.V7.App;
using ZXing;
using EDMTDev.ZXingXamarinAndroid;
using Com.Karumi.Dexter;
using Android;
using Com.Karumi.Dexter.Listener.Single;
using Com.Karumi.Dexter.Listener;

namespace Chaszcze
{
    [Activity(Label = "qrakcja")]
    public class qrakcja : AppCompatActivity,IPermissionListener
    {
        private ZXingScannerView scannerView;
        private TextView txtResult;

        public void OnPermissionDenied(PermissionDeniedResponse p0)
        {
            throw new NotImplementedException();
        }

        public void OnPermissionGranted(PermissionGrantedResponse p0)
        {
            scannerView.StartCamera();
        }

        public void OnPermissionRationaleShouldBeShown(PermissionRequest p0, IPermissionToken p1)
        {
            
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.qrekran);
            // Create your application here
            scannerView = FindViewById<ZXingScannerView>(Resource.Id.zxcan);

            Dexter.WithActivity(this)
                .WithPermission(Manifest.Permission.Camera)
                .WithListener(this)
                .Check();
        }
    }

}
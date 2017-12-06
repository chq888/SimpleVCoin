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
using Android.Util;
using Android.Support.V4.Content;

namespace VCoin.Droid.Service
{
    /// <summary>
    /// To capture the message in background you need to use a BroadcastReceiver
    /// </summary>
    public class FirebaseDataReceiver : WakefulBroadcastReceiver
    {

        private String TAG = "FirebaseDataReceiver";


        public override void OnReceive(Context context, Intent intent)
        {
            Log.Debug(TAG, "I'm in!!!");

            Bundle dataBundle = intent.GetBundleExtra("data");
            //Try with intent.getExtras()
            Log.Debug(TAG, dataBundle.ToString());

        }
    }
    //and add this to your manifest:

    //<receiver
    //    android:name= "MY_PACKAGE_NAME.FirebaseDataReceiver"
    //    android:exported= "true"
    //    android:permission= "com.google.android.c2dm.permission.SEND" >
    //    < intent - filter >
    //        < action android:name= "com.google.android.c2dm.intent.RECEIVE" />
    //    </ intent - filter >
    //</ receiver >
}
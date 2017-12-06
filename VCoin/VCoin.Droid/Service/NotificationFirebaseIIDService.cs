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
using Firebase.Iid;
using Android.Util;
using Firebase.Messaging;

namespace VCoin.Droid
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.INSTANCE_ID_EVENT" })]
    public class MyFirebaseIIDService : FirebaseInstanceIdService
    {

        const string TAG = "MyFirebaseIIDService";
        private static string _Token = String.Empty;


        public static string GetToken(Context context = null)
        {
            if (String.IsNullOrWhiteSpace(_Token))
            {
                return _Token;
            }
            //If you just try to retrieve the token without checking GetApps this happens
            //Java.Lang.IllegalStateException: Default FirebaseApp is not initialized in this process com.shane.firebasefun. Make sure to call FirebaseApp.initializeApp(Context) first.
            else if (Firebase.FirebaseApp.GetApps(context ?? Android.App.Application.Context).Count != 0)
            {
                _Token = FirebaseInstanceId.Instance.Token;
            }

            return _Token;
        }

        public override void OnTokenRefresh()
        {
            base.OnTokenRefresh();
            var refreshedToken = FirebaseInstanceId.Instance.Token;
            _Token = refreshedToken;

            var token = FirebaseInstanceId.Instance.Token;
            if (!string.IsNullOrEmpty(token))
            {
                // send fcm token to forms or server directly...
                Log.Debug("FCM token :{0}", token);
                FirebaseMessaging.Instance.SubscribeToTopic("LatestRate");
            }
        }

    }

}

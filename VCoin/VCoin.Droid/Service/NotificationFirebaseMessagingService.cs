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
using Firebase.Messaging;
using Java.Lang;
using Android.Util;
using Android.Media;

namespace VCoin.Droid
{

    [Service]
    [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
    public class MyFirebaseMessagingService : FirebaseMessagingService
    {

        public static string Body;

        public override void OnMessageReceived(RemoteMessage message)
        {
            base.OnMessageReceived(message);

            //var asdf = DateTimeOffset.FromUnixTimeMilliseconds(1509358919606);
            var notification = message.GetNotification();
            var data = message.Data;
            //string customData = (string)data["customData"];

            Log.Debug("MyFireMessagingService", "From: " + message.From);
            Log.Debug("MyFireMessagingService", "Notification Message Body: " + notification.Body);
            SendNotification(notification.Title, notification.Body);
        }

        void SendNotification(string title, string body)
        {
            Body = body;
            var intent = new Intent(this, typeof(MainActivity));
            intent.PutExtra("title", title);
            intent.PutExtra("body", body);

            intent.AddFlags(ActivityFlags.ClearTop);
            var pendingIntent = PendingIntent.GetActivity(this, 0, intent, PendingIntentFlags.OneShot);

            long[] pattern = { 500, 500, 500, 500, 500 };

            var notificationBuilder = new Notification.Builder(this)
                .SetSmallIcon(Resource.Drawable.ic_save)
                .SetContentTitle("Coin Rate")
                .SetContentText(body)
                .SetAutoCancel(true)
                .SetVibrate(pattern)
                .SetLights(Color.Blue, 1, 1)
                .SetSound(RingtoneManager.GetDefaultUri(RingtoneType.Notification))
                .SetContentIntent(pendingIntent);

            //.setContentTitle(URLDecoder.decode(getString(R.string.app_name), "UTF-8"))
            //.setContentText(URLDecoder.decode(message, "UTF-8"))

            notificationBuilder.SetLargeIcon(BitmapFactory.DecodeResource(Resources, Resource.Drawable.xamarin_logo));
            notificationBuilder.SetSubText("Tap to view");
            //notificationBuilder.SetNumber(2);
            //notificationBuilder.SetWhen
            //notificationBuilder.SetTicker("you received another value");

            var notificationManager = NotificationManager.FromContext(this);
            notificationManager.Notify(0, notificationBuilder.Build());
        }

        public override void HandleIntent(Intent intent)
        {
            try
            {
                //if (intent.Extras != null && intent.Extras.Get("your_data_key") != null)
                //{
                //    string strNotificaiton = intent.Extras.Get("your_data_key").ToString();
                //}

                if (intent.Extras != null)
                {
                    var builder = new RemoteMessage.Builder("MyFirebaseMessagingService");

                    foreach (string key in intent.Extras.KeySet())
                    {
                        builder.AddData(key, intent.Extras.Get(key).ToString());
                    }

                    OnMessageReceived(builder.Build());
                }
                else
                {
                    base.HandleIntent(intent);
                }
            }
            catch (System.Exception ex)
            {
                Log.Debug("MyFireMessagingService", "From: " + ex.StackTrace);

                base.HandleIntent(intent);
            }
        }

    }

}

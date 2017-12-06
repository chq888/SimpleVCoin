using System;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Support.V4.Widget;
using Android.Support.V4.View;
using Android.Widget;
using System.Collections.Generic;
using Android.App;
using Android.Gms.Common;
using AndroidSupportV7App = Android.Support.V7.App;
using Android.Content.Res;
using Android.Support.V4.App;
using Firebase.Iid;
using Android.Util;
using Firebase.Messaging;

namespace VCoin.Droid
{
    [Activity(Label = "@string/app_name", Icon = "@mipmap/icon",
        LaunchMode = LaunchMode.SingleInstance,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation,
        ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : BaseActivity
    {

        protected override int LayoutResource => Resource.Layout.activity_main;

        ViewPager pager;
        TabsAdapter adapter;

        public string Link;
        public string Rate;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            adapter = new TabsAdapter(this, SupportFragmentManager);
            pager = FindViewById<ViewPager>(Resource.Id.viewpager);
            var tabs = FindViewById<TabLayout>(Resource.Id.tabs);
            pager.Adapter = adapter;
            tabs.SetupWithViewPager(pager);
            pager.OffscreenPageLimit = 3;

            pager.PageSelected += (sender, args) =>
            {
                var fragment = adapter.InstantiateItem(pager, args.Position) as IFragmentVisible;

                fragment?.BecameVisible();
            };

            Toolbar.MenuItemClick += (sender, e) =>
            {
                var addItemActivityIntent = new Intent(this, typeof(AddItemActivity));
                StartActivity(addItemActivityIntent);
            };

            SupportActionBar.SetDisplayHomeAsUpEnabled(false);
            SupportActionBar.SetHomeButtonEnabled(false);

            if (IsPlayServicesAvailable())
            {
                RegisterForNotificationFCM();
            }

            Intent intent = this.Intent;
            if (intent != null && intent.Extras != null)
            {
                Link = intent.GetStringExtra("title");
                Rate = intent.GetStringExtra("body");

                if (string.IsNullOrEmpty(Link))
                {
                    Bundle bundle = intent.Extras;
                    String user_name = bundle.GetString("data");
                    Link = bundle.GetString("title");
                    Rate = bundle.GetString("body");
                    //extras.GetString("tag");
                }
            }
        }

        public override void OnCreate(Bundle savedInstanceState, PersistableBundle persistentState)
        {
            base.OnCreate(savedInstanceState, persistentState);
        }

        public override View OnCreateView(string name, Context context, IAttributeSet attrs)
        {
            return base.OnCreateView(name, context, attrs);
        }

        public override View OnCreateView(View parent, string name, Context context, IAttributeSet attrs)
        {
            return base.OnCreateView(parent, name, context, attrs);
        }

        public override void OnConfigurationChanged(Configuration newConfig)
        {
            base.OnConfigurationChanged(newConfig);
        }

        protected override void OnRestoreInstanceState(Bundle savedInstanceState)
        {
            base.OnRestoreInstanceState(savedInstanceState);
        }

        public override void OnRestoreInstanceState(Bundle savedInstanceState, PersistableBundle persistentState)
        {
            base.OnRestoreInstanceState(savedInstanceState, persistentState);
        }

        protected override void OnSaveInstanceState(Bundle outState)
        {
            base.OnSaveInstanceState(outState);
        }

        public override void OnSaveInstanceState(Bundle outState, PersistableBundle outPersistentState)
        {
            base.OnSaveInstanceState(outState, outPersistentState);
        }

        protected override void OnResume()
        {
            base.OnResume();
        }

        protected override void OnRestart()
        {
            base.OnRestart();
            //(adapter.CurrentItem as BrowseFragment).ViewModel.AddItemCommand(new Item { Id = Guid.NewGuid().ToString(), Text = "Rate", Description = MyFireMessagingService.Body });

            BrowseFragment.HasNotified = true;
        }

        protected override void OnNewIntent(Intent intent)
        {
            base.OnNewIntent(intent);

            if (intent != null && intent.Extras != null)
            {
                Link = intent.GetStringExtra("title");
                Rate = intent.GetStringExtra("body");

                if (string.IsNullOrEmpty(Link))
                {
                    Bundle bundle = intent.Extras;
                    String user_name = bundle.GetString("data");
                    Link = bundle.GetString("title");
                    Rate = bundle.GetString("body");
                    //extras.GetString("tag");
                }
            }
        }

        protected override void OnPause()
        {
            base.OnPause();
        }

        protected virtual void RegisterForNotificationFCM()
        {
            try
            {
                //Default FirebaseApp is not initialized in this process com.hdmedia.VCoin. Make sure to call FirebaseApp.initializeApp(Context) first.
                if (FirebaseInstanceId.Instance != null)
                {
                    var token = FirebaseInstanceId.Instance.Token;
                    if (!string.IsNullOrEmpty(token))
                    {
                        // send fcm token to forms or server directly...
                        Log.Debug("FCM token :{0}", token);
                        FirebaseMessaging.Instance.SubscribeToTopic("LatestRate");
                    }
                }
            }
            catch (Exception ex)
            {

            }

            //string googleAppId = Resources.GetString(Resource.String.google_app_id);
            //if (googleAppId.Equals("1:947351246136:android:d9e2b7351962c41e"))
            //{
            //    //throw new System.Exception("Invalid Json file");
            //    Task.Run(() =>
            //    {
            //        var instanceId = FirebaseInstanceId.Instance;
            //        if (instanceId != null)
            //        {
            //            if (!string.IsNullOrEmpty(instanceId.Token))
            //            {
            //                // send fcm token to forms or server directly...
            //                //Log.Debug("FCM token :{0}", token);
            //                FirebaseMessaging.Instance.SubscribeToTopic("SMS");
            //            }
            //        }

            //        //instanceId.DeleteInstanceId();
            //        //Android.Util.Log.Debug("TAG", "{0} {1}", instanceId.Token, instanceId.GetToken(GetString(Resource.String.gcm_defaultSenderId), Firebase.Messaging.FirebaseMessaging.InstanceIdScope));
            //    });
            //}
        }

        public bool IsPlayServicesAvailable()
        {
            int resultCode = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(this);
            if (resultCode != ConnectionResult.Success)
            {
                if (GoogleApiAvailability.Instance.IsUserResolvableError(resultCode))
                {
                    //msgText.Text = GoogleApiAvailability.Instance.GetErrorString(resultCode);
                }
                else
                {
                    //msgText.Text = "This device is not supported";
                    Finish();
                }

                return false;
            }
            else
            {
                //msgText.Text = "Google Play Services is available.";
                return true;
            }
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.top_menus, menu);
            return base.OnCreateOptionsMenu(menu);
        }
    }

    class TabsAdapter : FragmentStatePagerAdapter
    {
        string[] titles;

        public override int Count => titles.Length;

        public Android.Support.V4.App.Fragment CurrentItem { get; set; }

        public TabsAdapter(Context context, Android.Support.V4.App.FragmentManager fm) : base(fm)
        {
            titles = context.Resources.GetTextArray(Resource.Array.sections);
        }

        public override Java.Lang.ICharSequence GetPageTitleFormatted(int position) =>
                            new Java.Lang.String(titles[position]);

        public override Android.Support.V4.App.Fragment GetItem(int position)
        {
            switch (position)
            {
                case 0: return CurrentItem = BrowseFragment.NewInstance();
                case 1: return CurrentItem = AboutFragment.NewInstance();
            }

            return null;
        }

        public override int GetItemPosition(Java.Lang.Object frag) => PositionNone;
    }
}

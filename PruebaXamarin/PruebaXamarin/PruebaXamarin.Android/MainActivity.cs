namespace PruebaXamarin.Droid
{
    using Android.App;
    using Android.Content.PM;
    using Android.OS;
    using HockeyApp.Android;
    using HockeyApp.Android.Metrics;

    [Activity(Label = "PruebaXamarin", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        private string AppId = "815f9568690c4d428ae7180035bd171b";
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            CrashManager.Register(this, AppId);
            MetricsManager.Register(Application, AppId);

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());
            CheckForUpdates();
        }

        private void CheckForUpdates()
        {            
            UpdateManager.Register(this, AppId);
        }

        private void UnregisterManagers()
        {
            UpdateManager.Unregister();
        }

        protected override void OnPause()
        {
            base.OnPause();
            UnregisterManagers();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            UnregisterManagers();
        }

        protected override void OnResume()
        {
            base.OnResume();
            CrashManager.Register(this, AppId);
        }
    }
}


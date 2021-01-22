using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Content;
using Android.Content.PM;

namespace FinalProject
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        Button game;
        Button online;
        Button offline;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.homescreen);
            game = FindViewById<Button>(Resource.Id.game);
            online = FindViewById<Button>(Resource.Id.online);
            offline = FindViewById<Button>(Resource.Id.offline);

            game.Click += Game_Click;
            offline.Click += Offline_Click;
        }

        private void Offline_Click(object sender, System.EventArgs e)
        {
            Intent gameActivity = new Intent(this,typeof(GameActivity));
            StartActivity(gameActivity);
        }

        private void Game_Click(object sender, System.EventArgs e)
        {
            online.Visibility = Android.Views.ViewStates.Visible;
            offline.Visibility = Android.Views.ViewStates.Visible;
            online.LayoutParameters = new LinearLayout.LayoutParams(300, LinearLayout.LayoutParams.WrapContent);
            offline.LayoutParameters = new LinearLayout.LayoutParams(300, LinearLayout.LayoutParams.WrapContent);
        }
    }
}
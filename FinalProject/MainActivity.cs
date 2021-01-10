using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;

namespace FinalProject
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        public static BoardGame boardGame;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            boardGame = new BoardGame(this);
            SetContentView(boardGame);
        }

        private void MainActivity_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            throw new System.NotImplementedException();
        }
    }
}
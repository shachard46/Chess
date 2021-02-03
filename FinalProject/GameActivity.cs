
using System;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Widget;

namespace FinalProject
{
    [Activity(Label = "GameActivity", ScreenOrientation = ScreenOrientation.Landscape)]
    public class GameActivity : Activity
    {
        public static BoardGame boardGame;
        FrameLayout frameLayout;

        TextView yourTime;
        TextView opponentTime;
        Button back;
        bool toggle;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.game_board);
            frameLayout = FindViewById<FrameLayout>(Resource.Id.frame);
            yourTime = FindViewById<TextView>(Resource.Id.your);
            opponentTime = FindViewById<TextView>(Resource.Id.opponent);
            back = FindViewById<Button>(Resource.Id.back);

            boardGame = new BoardGame(this, new TimerHandler(yourTime, this),
                new TimerHandler(opponentTime, this),
                Intent.GetBooleanExtra("hint", false), Intent.GetIntExtra("duration", 60));
            frameLayout.AddView(boardGame);

            back.Click += Back_Click;
        }

        private void Back_Click(object sender, EventArgs e)
        {
            var builder = new AlertDialog.Builder(this);
            builder.SetTitle("Are you sure you want to go back?");

            builder.SetPositiveButton("Yes", (sender, args) =>
            {
                StartActivity(new Android.Content.Intent(this, typeof(MainActivity)));
            });
            builder.SetNegativeButton("No", (sender, args)=> { });
            var dialog = builder.Create();

            dialog.Show();
        }
    }
}

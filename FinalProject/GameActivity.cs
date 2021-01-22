
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace FinalProject
{
    [Activity(Label = "GameActivity", ScreenOrientation = ScreenOrientation.Landscape)]
    public class GameActivity : Activity
    {
        public static BoardGame boardGame;
        FrameLayout frameLayout;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.game_board);
            boardGame = new BoardGame(this);
            frameLayout = FindViewById<FrameLayout>(Resource.Id.frame);
            frameLayout.AddView(boardGame);
        }
    }
}

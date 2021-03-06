
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Preferences;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace FinalProject.activitys
{
    [Activity(Label = "StoreActivity")]
    public class StoreActivity : Activity
    {
        List<ImageView> images;
        GridLayout linearLayout;
        int[] resources = new int[]
        {
            Resource.Drawable.gold_knight,
            Resource.Drawable.lightgold__knight,
            Resource.Drawable.real_knight,
        };
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.store);
            linearLayout = FindViewById<GridLayout>(Resource.Id.ll);
            images = new List<ImageView>();
            foreach (int resource in resources)
            {
                GridLayout.LayoutParams layoutParams = new GridLayout.LayoutParams();
                layoutParams.Width = Util.convertDpToPixel(170, this);
                layoutParams.Height = Util.convertDpToPixel(170, this);
                layoutParams.RowSpec = GridLayout.InvokeSpec(images.Count / 2);
                layoutParams.ColumnSpec = GridLayout.InvokeSpec(images.Count % 2);
                ImageView imageView = new ImageView(this);
                imageView.SetImageResource(resource);
                imageView.LayoutParameters = layoutParams;
                images.Add(imageView);
                linearLayout.AddView(imageView);
                imageView.Click += ImageView_Click;
            }
        }

        private void ImageView_Click(object sender, EventArgs e)
        {
            ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(this);
            ISharedPreferencesEditor editor = prefs.Edit();
            editor.PutInt("skin", resources[images.IndexOf((ImageView)sender)]);
            editor.Apply();
        }
    }
}

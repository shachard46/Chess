using System;
using Android.Content;
using Android.Util;

namespace FinalProject
{
    public class Util
    {
        public Util()
        { }

        public static int convertDpToPixel(float dp, Context context)
        {
            return (int)(dp * (float)context.Resources.DisplayMetrics.DensityDpi / (float)DisplayMetricsDensity.Default);
        }

        public static float convertPixelsToDp(float px, Context context)
        {
            return px / (float)context.Resources.DisplayMetrics.DensityDpi / (float)DisplayMetricsDensity.Default;
        }
    }
}

using System;
using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.Views;

namespace FinalProject
{
    public class Image : Shape
    {
        int height;
        int width;

        public Bitmap image { get; set; }
        public static View View { get; set; }

        public Image(float x, float y, int res) : base(x, y, Color.White)
        {
            BitmapFactory.Options options = new BitmapFactory.Options();
            options.InMutable = true;
            options.InScaled = true;
            options.InDensity = 1000;
            options.InTargetDensity = 200;
            image = BitmapFactory.DecodeResource(View.Resources, res,options);
        }
        public override void Draw(Canvas canvas)
        {
            base.Draw(canvas);
            canvas.DrawBitmap(image, GetX(), GetY(), GetPainter());
            width = image.GetScaledWidth(canvas);
            height = image.GetScaledHeight(canvas);
        }

        public override bool IsInArea(float x, float y)
        {

            return (GetX() <= x + width / 2 && GetX() >= x - width / 2) && (GetY() <= y + height / 2 && GetY() >= y - height / 2);
        }
    }
}

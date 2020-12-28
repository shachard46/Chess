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
            image = BitmapFactory.DecodeResource(View.Resources, res);
            image = image.Copy(Bitmap.Config.Argb8888, true);
            float ratio = image.Width / image.Height;
            image = Bitmap.CreateScaledBitmap(image, (int)(200 * ratio), 200, false);
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

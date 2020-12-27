using System;
using Android.Content.Res;
using Android.Graphics;

namespace FinalProject
{
    public class Image : Shape
    {

        public Bitmap image { get; set; }
        int height; 
        int width; 

        public Image(float x, float y, int res) : base(x, y, Color.White)
        {
            this.image = BitmapFactory.DecodeResource(Resources.System, res);
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

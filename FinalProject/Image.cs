using System;
using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.Views;
using Android.Widget;

namespace FinalProject
{
    public class Image : Shape
    {
        int height;
        int width;
        bool flip;
        public Bitmap image { get; set; }
        public static View View { get; set; }

        public Image(float x, float y, float density, int res, bool flip) : base(x, y, Color.White)
        {
            this.flip = flip;
            BitmapFactory.Options options = new BitmapFactory.Options();
            options.InMutable = true;
            options.InScaled = true;
            options.InDensity = 1000;
            options.InTargetDensity = (int)density;
            image = BitmapFactory.DecodeResource(View.Resources, res, options);
        }
        public Image(float x, float y, int res, bool flip) : this(x, y, BoardGame.Constants.SQUARE_SIDE * 2, res, flip)
        {
        }
        public override void Draw(Canvas canvas)
        {
            base.Draw(canvas);
            width = image.GetScaledWidth(canvas);
            height = image.GetScaledHeight(canvas);
            Matrix matrix = new Matrix();
            if (flip)
            {
                matrix.SetRotate(180, width / 2, height / 2);
            }
            matrix.PostTranslate(GetX() - BoardGame.Constants.SQUARE_SIDE - 10,
                GetY() - BoardGame.Constants.SQUARE_SIDE);
            canvas.DrawBitmap(image, matrix, GetPainter());
        }

        public override bool IsInArea(float x, float y)
        {

            return (GetX() <= x + width / 2 && GetX() >= x - width / 2) && (GetY() <= y + height / 2 && GetY() >= y - height / 2);
        }
    }
}

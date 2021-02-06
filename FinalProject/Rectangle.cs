using System;
using Android.Graphics;

namespace FinalProject
{
    public class Rectangle : Shape
    {
        private float h, w;

        public Rectangle(float x, float y, float h, float w, Color color) : base(x, y, color)
        {
            this.h = h;
            this.w = w;
        }

        public override void Draw(Canvas canvas)
        {
            base.Draw(canvas);
            canvas.DrawRect(GetX() - w / 2, GetY() - h / 2, GetX() + w / 2, GetY() + h / 2, GetPainter());
        }

        public void AddBorder(Canvas canvas, float border_width, Color color)
        {
            Paint paint = new Paint();
            paint.SetStyle(Paint.Style.Stroke);
            paint.Color = color;
            paint.StrokeWidth = border_width;
            canvas.DrawRect(GetX() - w / 2, GetY() - h / 2, GetX() + w / 2, GetY() + h / 2, paint);
        }
        public override Shape Flip()
        {
            return Flip(0,0);
        }

        public override Shape Flip(float addx, float addy)
        {
            return new Rectangle(GetY() + addx, GetX() + addy, w, h, GetPainter().Color);
        }

        public override bool IsInArea(float x, float y)
        {
            return (GetX() <= x + w / 2 && GetX() >= x - w / 2) && (GetY() <= y + h / 2 && GetY() >= y - h / 2);
        }

        public float GetHalfH()
        {
            return h / 2;
        }

        public float GetHalfW()
        {
            return w / 2;
        }

        public void SetWidth(float width)
        {
            this.w = width;
        }
        public void SetHeight(float h)
        {
            this.h = h;
        }
    }

}

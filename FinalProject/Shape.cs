using System;
using Android.Graphics;
namespace FinalProject
{
    public class Shape
    {
        private float x, y;
        private Paint painter;
        private Color color;

        public Shape(float x, float y, Color color)
        {
            this.x = x;
            this.y = y;
            painter = new Paint();
            this.color = color;
        }
        public virtual void Draw(Canvas canvas)
        {
            painter.Color = color;
            painter.SetStyle(Paint.Style.Fill);
        }

        public void SetX(float x)
        {
            this.x = x;
        }


        public void SetY(float y)
        {
            this.y = y;
        }

        public float GetX()
        {
            return x;
        }


        public float GetY()
        {
            return y;
        }

        public Paint GetPainter()
        {
            return painter;
        }

        public virtual bool IsInArea(float x, float y)
        {
            return x == this.x && y == this.y;
        }
    }
}
using System;
using Android.Graphics;

namespace FinalProject
{
    public class Text : Shape
    {
        private string text;
        private int textSize = 90;

        public int TextSize { get => textSize; set => textSize = value; }

        public Text(string text, float x, float y, Color color) : this(text, x, y, color, 90) { }

        public Text(string text, float x, float y, Color color, int textSize) : base(x, y, color)
        {
            this.textSize = textSize;
            this.text = text;
            GetPainter().TextAlign = Paint.Align.Center;
            SetY(GetY());
        }

        public void setText(string text)
        {
            this.text = text;
        }

        public override void Draw(Canvas canvas)
        {
            base.Draw(canvas);
            GetPainter().TextSize = textSize;
            canvas.DrawText(text, GetX(), GetY(), GetPainter());
        }

        public override Shape Flip()
        {
            return Flip(0, 0);
        }

        public override Shape Flip(float addx, float addy)
        {
            return new Text(text, GetY() + addx, GetX()+ addy, GetPainter().Color, textSize);

        }
    }
}

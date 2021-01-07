using System;
using Android.Graphics;

namespace FinalProject
{
    public class Text : Shape
    {
        private string text;
        private int textSize = 90;

        public int TextSize { get => textSize; set => textSize = value; }

        public Text(string text, float x, float y, Color color) : base(x, y, color)
        {
            this.text = text;
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
    }
}

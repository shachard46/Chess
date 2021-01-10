using System;
using Android.Graphics;
using FinalProject.Pieces;

namespace FinalProject
{
    public class BoardSquare
    {
        public static Paint possible;
        private Rectangle rectangle;
        public BoardSquare(Rectangle rectangle, Piece currentPiece)
        {
            this.rectangle = rectangle;
            Center = new float[] { rectangle.GetX(), rectangle.GetY() };
            CurrentPiece = currentPiece;
            possible = new Paint();
            possible.SetStyle(Paint.Style.Stroke);
            possible.StrokeWidth = 4;
            possible.Color = Color.Red;
        }

        public float[] Center { get; set; }
        public Piece CurrentPiece { get; set; } = null;

        public bool IsInArea(Piece piece)
        {
            return IsInArea(piece.GetX(), piece.GetY());
        }
        public bool IsInArea(float x, float y)
        {
            return rectangle.IsInArea(x, y);
        }

        public void Draw(Canvas canvas)
        {
            rectangle.Draw(canvas);
        }

    }
}

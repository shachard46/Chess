using System;
using Android.Graphics;
using FinalProject.Pieces;

namespace FinalProject
{
    public class BoardSquare
    {
        public static Paint possible;
        public BoardSquare(float x, float y, Piece currentPiece)
        {
            Center = new float[] { x, y };
            CurrentPiece = currentPiece;
            possible = new Paint();
            possible.SetStyle(Paint.Style.Stroke);
            possible.StrokeWidth = 4;
            possible.Color = Color.Red;
        }

        public float[] Center { get; set; }
        public Paint Paint { get; set; } = new Paint();
        public int SideLength { get; set; }
        public Piece CurrentPiece { get; set; }

        public bool IsInArea(Piece piece)
        {
            float x = piece.GetX();
            float y = piece.GetY();
            bool isInX = Center[0] + SideLength / 2 > x && Center[0] - SideLength / 2 < x;
            bool isInY = Center[1] + SideLength / 2 > y && Center[1] - SideLength / 2 < y;
            return isInX && isInY;
        }
        public bool IsInArea(float x, float y)
        {
            bool isInX = Center[0] + SideLength / 2 > x && Center[0] - SideLength / 2 < x;
            bool isInY = Center[1] + SideLength / 2 > y && Center[1] - SideLength / 2 < y;
            return isInX && isInY;
        }
    }
}

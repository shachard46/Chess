using System;
namespace FinalProject
{
    public class BoardSquare
    {
        public BoardSquare(int x, int y, Piece currentPiece)
        {
            Center = new int[] { x, y };
            CurrentPiece = currentPiece;
        }

        public int[] Center { get; set; }
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
    }
}

using System;
using Android.Graphics;

namespace FinalProject.Pieces
{
    public class Knight : Piece
    {
        public Knight(float x, float y, Bitmap bitmap, bool side, bool eaten) : base(x, y, bitmap, side, eaten)
        {
        }

        public override BoardSquare[,] GetPossiblePlaces(BoardSquare[,] squares)
        {
            throw new NotImplementedException();
        }
    }
}

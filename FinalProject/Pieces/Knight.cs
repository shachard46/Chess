using System;
using System.Collections.Generic;
using Android.Graphics;

namespace FinalProject.Pieces
{
    public class Knight : Piece
    {
        public Knight(float[] cords, side side) : base(cords, side == side.Black ? Resource.Drawable.black_knight : Resource.Drawable.white_knight, side)
        {
        }

        public override List<BoardSquare> GetPossiblePlaces(BoardSquare[,] squares)
        {
            throw new NotImplementedException();
        }
    }
}

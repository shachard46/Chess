using System;
using System.Collections.Generic;
using Android.Graphics;

namespace FinalProject.Pieces
{
    public class Queen : Piece
    {
        public Queen(float[] cords, side side) : base(cords, side == side.Black ? Resource.Drawable.black_queen : Resource.Drawable.white_queen, side)
        {
        }

        public override List<BoardSquare> GetPossiblePlaces(BoardSquare[,] squares)
        {
            return null;
        }
    }
}

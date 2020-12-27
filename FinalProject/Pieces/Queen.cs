using System;
using System.Collections.Generic;
using Android.Graphics;

namespace FinalProject.Pieces
{
    public class Queen : Piece
    {
        public Queen(float[] cords, int res, side side) : base(cords, res, side)
        {
        }

        public override List<BoardSquare> GetPossiblePlaces(BoardSquare[,] squares)
        {
            return null;
        }
    }
}

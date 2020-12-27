using System;
using System.Collections.Generic;
using Android.Graphics;

namespace FinalProject.Pieces
{
    public class Knight : Piece
    {
        public Knight(float[] cords, int res, side side) : base(cords, res, side)
        {
        }

        public override List<BoardSquare> GetPossiblePlaces(BoardSquare[,] squares)
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Collections.Generic;
using Android.Graphics;

namespace FinalProject.Pieces
{
    public class King : Piece, CastledPiece
    {
        public King(float[] cords, int res, side side) : base(cords, res, side)
        {
        }

        public bool CanCastle()
        {
            throw new NotImplementedException();
        }

        public override List<BoardSquare> GetPossiblePlaces(BoardSquare[,] squares)
        {
            List<BoardSquare> possibilities = new List<BoardSquare>();
            int x = GetBoardSquareCords(squares)[0],
                y = GetBoardSquareCords(squares)[1];

            for (int i = y - 1; i <= y + 1; i++)
            {
                for (int j = x - 1; j <= x + 1; j++)
                {
                    if (squares[j, i].CurrentPiece == null && squares[j, i].CurrentPiece.Side != this.Side)
                    {
                        possibilities.Add(squares[j, i]);
                    }
                }
            }
            if (CanCastle())
            {
                // hatsracha
            }
            return possibilities;
        }
    }
}

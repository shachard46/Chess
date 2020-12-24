using System;
using System.Collections.Generic;
using Android.Graphics;

namespace FinalProject.Pieces
{
    public class Bishop : Piece
    {
        public Bishop(float x, float y, Bitmap bitmap, bool side, bool eaten) : base(x, y, bitmap, side, eaten)
        {
        }

        public override List<BoardSquare> GetPossiblePlaces(BoardSquare[,] squares)
        {
            List<BoardSquare> possibilities = new List<BoardSquare>();
            int x = GetBoardSquare(squares)[0],
                y = GetBoardSquare(squares)[1];
            int counter = 0;
            for (int i = 0; i < squares.GetLength(1); i++)
            {
                for (int j = 0; j < squares.GetLength(0); j++)
                {
                    if (Math.Abs(x - j) == Math.Abs(y - i) && counter == 0)
                    {
                        possibilities.Add(squares[j, i]);
                    }
                }
            }
            return possibilities;
        }
    }
}

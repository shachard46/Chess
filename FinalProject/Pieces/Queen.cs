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
            List<BoardSquare> possibilities = new List<BoardSquare>();
            int x = GetBoardSquareCords(squares)[0],
                y = GetBoardSquareCords(squares)[1];

            bool first = true, second = true, third = true, fourth = true,
                 plusx = true, minusx = true, plusy = true, minusy = true;
            for (int i = 0; i < squares.GetLength(1); i++)
            {
                int a, b;
                if (first)
                {
                    a = x + i + 1;
                    b = y - i - 1;
                    first = CheckIfPossible(a, b, a < squares.GetLength(0) && b >= 0, squares, possibilities);
                }

                if (second)
                {
                    a = x - i - 1;
                    b = y - i - 1;
                    second = CheckIfPossible(a, b, a >= 0 && b >= 0, squares, possibilities);
                }
                if (third)
                {
                    a = x - i - 1;
                    b = y + i + 1;
                    third = CheckIfPossible(a, b, a >= 0 && b < squares.GetLength(1), squares, possibilities);
                }
                if (fourth)
                {
                    a = x + i + 1;
                    b = y + i + 1;
                    fourth = CheckIfPossible(a, b, a < squares.GetLength(0) && b < squares.GetLength(1), squares, possibilities);
                }
                if (plusx)
                {
                    a = x + i + 1;
                    b = y;
                    plusx = CheckIfPossible(a, b, a < squares.GetLength(0), squares, possibilities);
                }

                if (minusx)
                {
                    a = x - i - 1;
                    b = y;
                    minusx = CheckIfPossible(a, b, a >= 0, squares, possibilities);
                }
                if (plusy)
                {
                    a = x;
                    b = y + i + 1;
                    plusy = CheckIfPossible(a, b, b < squares.GetLength(1), squares, possibilities);
                }

                if (minusy)
                {
                    a = x;
                    b = y - i - 1;
                    minusy = CheckIfPossible(a, b, b >= 0, squares, possibilities);
                }
            }
            return possibilities;
        }
    }
}

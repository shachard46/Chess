using System;
using System.Collections.Generic;
using Android.Graphics;

namespace FinalProject.Pieces
{
    public class Bishop : Piece
    {
        public Bishop(float[] cords, side side) : base(cords, side == side.Black ? Resource.Drawable.black_bishop : Resource.Drawable.white_bishop, side)
        {
        }

        public override List<BoardSquare> GetPossiblePlaces(BoardSquare[,] squares)
        {
            List<BoardSquare> possibilities = new List<BoardSquare>();
            int x = GetBoardSquareCords(squares)[0],
                y = GetBoardSquareCords(squares)[1];
            bool first = true, second = true, third = true, fourth = true;
            for (int i = 0; i < squares.GetLength(1); i++)
            {
                if (first)
                {
                    int a = x + i + 1;
                    int b = y - i - 1;
                    first = CheckIfPossible(a, b, a < squares.GetLength(0) && b >= 0, squares, possibilities);
                }

                if (second)
                {
                    int a = x - i - 1;
                    int b = y - i - 1;
                    second = CheckIfPossible(a, b, a >= 0  && b >=0, squares, possibilities);
                }
                if (third)
                {
                    int a = x - i - 1;
                    int b = y + i + 1;
                    third = CheckIfPossible(a, b, a >= 0 && b < squares.GetLength(1), squares, possibilities);
                }

                if (fourth)
                {
                    int a = x + i + 1;
                    int b = y + i + 1;
                    fourth = CheckIfPossible(a, b, a < squares.GetLength(0) && b < squares.GetLength(1), squares, possibilities);
                }
            }
            return possibilities;
        }
        private bool CheckIfPossible(int x, int y, bool limit, BoardSquare[,] squares, List<BoardSquare> possibilities)
        {
            if (limit)
            {
                if (squares[y, x].CurrentPiece == null)
                {
                    possibilities.Add(squares[y, x]);
                    return true;
                }
                else if (squares[y, x].CurrentPiece != null && squares[y, x].CurrentPiece.Side == this.Side)
                {
                    return false;
                }
                else
                {
                    possibilities.Add(squares[y, x]);
                    return false;
                }
            }
            return false;
        }
    }
}


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
            List<BoardSquare> possibilities = new List<BoardSquare>();
            int x = GameActivity.boardGame.GetBoardSquareCords(this)[0],
                y = GameActivity.boardGame.GetBoardSquareCords(this)[1];
            for (int i = -1; i < 2; i+=2)
            {
                for (int j = -1; j < 2; j += 2)
                {
                    int a = x + 2 * j;
                    int b = y + i;
                    CheckIfPossible(a, b, a < squares.GetLength(0) && a >= 0 && b < squares.GetLength(1) && b >= 0, squares, possibilities);
                }
                for (int j = -1; j < 2; j += 2)
                {
                    int a = x + i;
                    int b = y + 2 * j;
                    CheckIfPossible(a, b, a < squares.GetLength(0) && a >= 0 && b < squares.GetLength(1) && b >= 0, squares, possibilities);
                }
            }
            return possibilities;
        }
    }
}

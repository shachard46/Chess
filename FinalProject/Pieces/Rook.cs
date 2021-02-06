using System;
using System.Collections.Generic;
using Android.Graphics;
using Java.Util.Functions;
using Android.Media;
using System.Drawing;

namespace FinalProject.Pieces
{
    public class Rook : Piece, CastledPiece
    {
        private bool hasMoved;
        public Rook(float[] cords, Side side) : base(cords,
            side == Side.Black ? Resource.Drawable.black_rook : Resource.Drawable.white_rook, side)
        {
        }

        public bool CanCastle(BoardSquare[,] squares)
        {
            return !hasMoved;
        }

        public override List<BoardSquare> GetPossiblePlaces(BoardSquare[,] squares)
        {
            List<BoardSquare> possibilities = new List<BoardSquare>();
            int x = GameActivity.boardGame.GetBoardSquareCords(this)[0],
                y = GameActivity.boardGame.GetBoardSquareCords(this)[1];
            bool plusx = true, minusx = true, plusy = true, minusy = true;
            for (int i = 0; i < squares.GetLength(1); i++)
            {
                if (plusx)
                {
                    plusx = CheckIfPossible(x + i + 1, y, x + i + 1 < squares.GetLength(0), squares, possibilities);
                }

                if (minusx)
                {
                    minusx = CheckIfPossible(x - i - 1, y, x - i - 1 >= 0, squares, possibilities);
                }
                if (plusy)
                {
                    plusy = CheckIfPossible(x, y + i + 1, y + i + 1 < squares.GetLength(1), squares, possibilities);
                }

                if (minusy)
                {
                    minusy = CheckIfPossible(x, y - i - 1, y - i - 1 >= 0, squares, possibilities);
                }
            }
            return possibilities;
        }

        public void HasMoved(bool moved)
        {
            hasMoved = moved;
        }
    }
}


using System;
using System.Collections.Generic;
using Android.Graphics;

namespace FinalProject.Pieces
{
    public class King : Piece, CastledPiece
    {
        private bool hasMoved;
        public King(float[] cords, side side) : base(cords, side == side.Black ? Resource.Drawable.black_king : Resource.Drawable.white_king, side)
        {
            hasMoved = false;
        }

        public int CanCastle(BoardSquare[,] squares)
        {
            return 0;
            //if (hasMoved)
            //{
            //    return 0;
            //}
            //foreach(BoardSquare square in squares)
            //{
            //    if(square.CurrentPiece != null && square.CurrentPiece is CastledPiece && square.CurrentPiece.Side == Side)
            //    {

            //    }
            //}
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
                    CheckIfPossible(j, i,
                        !(x == squares.GetLength(0) - 1 && j > x)
                        && !(x == 0 && j < x)
                        && !(y == squares.GetLength(1) - 1 && i > y)
                        && !(y == 0 && i < y), squares, possibilities);
                }
            }
            //if (CanCastle())
            //{
            //    // hatsracha
            //}
            return possibilities;
        }

        public void HasMoved(bool moved)
        {
            hasMoved = moved;
        }
    }
}

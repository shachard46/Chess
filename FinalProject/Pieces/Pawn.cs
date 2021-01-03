using System;
using System.Collections.Generic;
using Android.Graphics;

namespace FinalProject.Pieces
{
    public class Pawn : Piece
    {
        public Pawn(float[] cords, side side) : base(cords, side == side.Black ? Resource.Drawable.black_pawn : Resource.Drawable.white_pawn, side)
        {
        }

        public override List<BoardSquare> GetPossiblePlaces(BoardSquare[,] squares)
        {
            List<BoardSquare> possibilities = new List<BoardSquare>();
            int x = MainActivity.boardGame.GetBoardSquareCords(this)[0],
                y = MainActivity.boardGame.GetBoardSquareCords(this)[1];
            int direction = Side == side.White ? -1 : 1;
            for (int i = -1; i <= 1; i++)
            {
                switch (i)
                {

                    case -1:
                    case 1:
                        if (!(x == squares.GetLength(0) - 1 && i > 0) && !(x == 0 && i < 0))
                        {
                            if (squares[y + direction, x + i].CurrentPiece != null
                                && squares[y + direction, x + i].CurrentPiece.Side != this.Side)
                            {
                                possibilities.Add(squares[y + direction, x + i]);
                            }
                        }
                        break;
                    case 0:
                        if (squares[y + direction, x + i].CurrentPiece == null)
                        {
                            possibilities.Add(squares[y + direction, x]);
                        }
                        break;
                }

            }
            if ((y == 1 && Side == side.Black) || (y == 6 && Side == side.White))
            {
                possibilities.Add(squares[y + 2 * direction, x]);
            }
            return possibilities;
        }
    }
}

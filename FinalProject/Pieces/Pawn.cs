using System;
using System.Collections.Generic;
using Android.Graphics;

namespace FinalProject.Pieces
{
    public class Pawn : Piece
    {
        public Pawn(float x, float y, Bitmap bitmap, bool side, bool eaten) : base(x, y, bitmap, side, eaten)
        {
        }

        public override List<BoardSquare> GetPossiblePlaces(BoardSquare[,] squares)
        {
            List<BoardSquare> possibilities = new List<BoardSquare>();
            int x = GetBoardSquare(squares)[0],
                y = GetBoardSquare(squares)[1];
            for (int i = -1; i <= 1; i++)
            {
                switch (i)
                {

                    case -1:
                    case 1:
                        if (squares[x + i, y + 1].CurrentPiece != null && squares[x + i, y + 1].CurrentPiece.Side != this.Side)
                        {
                            possibilities.Add(squares[x + i, y + 1]);
                        }
                        break;
                    case 0:
                        if(squares[x + i, y + 1].CurrentPiece != null)
                        {
                            possibilities.Add(squares[x, y + 1]);
                        }
                        break;
                }

            }
            return possibilities;
        }
    }
}

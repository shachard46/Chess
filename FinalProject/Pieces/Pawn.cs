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
            int x = GameActivity.boardGame.GetBoardSquareCords(this)[0],
                y = GameActivity.boardGame.GetBoardSquareCords(this)[1];
            int direction = Side == side.White ? -1 : 1;
            for (int i = -1; i <= 1; i++)
            {
                switch (i)
                {

                    case -1:
                    case 1:
                        if (!(x == squares.GetLength(0) - 1 && i > 0) && !(x == 0 && i < 0) &&
                            !(y == squares.GetLength(1) - 1 && direction > 0) && !(y == 0 && direction < 0))
                        {
                            if (squares[y + direction, x + i].CurrentPiece != null
                                && squares[y + direction, x + i].CurrentPiece.Side != this.Side)
                            {
                                if (CheckIfMoveLegal(GameActivity.boardGame.Squares[y + direction, x + i]))
                                {
                                    possibilities.Add(squares[y + direction, x + i]);
                                }
                            }
                        }
                        break;
                    case 0:
                        if (!(y == squares.GetLength(1) - 1 && direction > 0) && !(y == 0 && direction < 0) &&
                            squares[y + direction, x].CurrentPiece == null)
                        {
                            if (CheckIfMoveLegal(GameActivity.boardGame.Squares[y + direction, x]))
                            {
                                possibilities.Add(squares[y + direction, x]);
                            }
                        }
                        break;
                }

            }
            if (((y == 1 && Side == side.Black) || (y == 6 && Side == side.White)) &&
                CheckIfMoveLegal(GameActivity.boardGame.Squares[y + 2 * direction, x]) &&
                squares[y + 2 * direction, x].CurrentPiece == null)
            {
                possibilities.Add(squares[y + 2 * direction, x]);
            }
            return possibilities;
        }
    }
}

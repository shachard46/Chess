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
        public Rook(float[] cords, side side) : base(cords,
            side == side.Black ? Resource.Drawable.black_rook : Resource.Drawable.white_rook, side)
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
            //checkBlocks(x, squares.GetLength(0) - 1, squares, possibilities);
            //checkBlocks(squares.GetLength(1), y, squares, possibilities);
            int plus = y + 1, minus = y - 1;
            for (int i = 0; i < squares.GetLength(1); i++)
            {
                if (plus < squares.GetLength(1))
                {
                    if (squares[plus, x].CurrentPiece == null)
                    {
                        possibilities.Add(squares[plus, x]);
                        plus += 1;
                    }
                    else if (squares[plus, x].CurrentPiece != null && squares[plus, x].CurrentPiece.Side == this.Side)
                    {
                        plus = squares.GetLength(1);
                    }
                    else
                    {
                        possibilities.Add(squares[plus, x]);
                        plus = squares.GetLength(1);
                    }
                }
                if (minus >= 0)
                {
                    if (squares[minus, x].CurrentPiece == null)
                    {
                        possibilities.Add(squares[minus, x]);
                        minus -= 1;
                    }
                    else if (squares[minus, x].CurrentPiece != null && squares[minus, x].CurrentPiece.Side == this.Side)
                    {
                        minus = -1;
                    }
                    else
                    {
                        possibilities.Add(squares[minus, x]);
                        minus = -1;
                    }
                }
            }
            plus = x + 1;
            minus = x - 1;
            for (int i = 0; i < squares.GetLength(0); i++)
            {
                if (plus < squares.GetLength(0))
                {
                    if (squares[y, plus].CurrentPiece == null)
                    {
                        possibilities.Add(squares[y, plus]);
                        plus += 1;
                    }
                    else if (squares[y, plus].CurrentPiece != null && squares[y, plus].CurrentPiece.Side == this.Side)
                    {
                        plus = squares.GetLength(0);
                    }
                    else
                    {
                        possibilities.Add(squares[i, x]);
                        plus = squares.GetLength(0);
                    }
                }
                if (minus >= 0)
                {
                    if (squares[y, minus].CurrentPiece == null)
                    {
                        possibilities.Add(squares[y, minus]);
                        minus -= 1;
                    }
                    else if (squares[y, minus].CurrentPiece != null && squares[y, minus].CurrentPiece.Side == this.Side)
                    {
                        minus = -1;
                    }
                    else
                    {
                        possibilities.Add(squares[y, minus]);
                        minus = -1;
                    }
                }
            }
            return possibilities;
        }
        private void checkBlocks(int x, int y, BoardSquare[,] squares, List<BoardSquare> possibilities)
        {
            for (int i = x; i < squares.GetLength(1); i++)
            {
                for (int j = y; j < squares.GetLength(0); j++)
                {
                    if (squares[i, j].CurrentPiece == null)
                    {
                        possibilities.Add(squares[i, j]);
                    }
                    else if (squares[i, j].CurrentPiece != null && squares[i, j].CurrentPiece.Side == this.Side)
                    {
                        break;
                    }
                    else
                    {
                        possibilities.Add(squares[i, j]);
                        break;
                    }
                }
            }
        }
    }
}


using System;
using System.Collections.Generic;
using Android.Content.Res;
using Android.Graphics;
using Android.Widget;

namespace FinalProject
{
    public abstract class Piece : Image
    {
        public Piece(float x, float y, int res, side side) : base(x, y, res)
        {
            Side = side;
            Eaten = false;
        }
        public Piece(float[] cords, int res, side side) : base(cords[0], cords[1], res)
        {
            Side = side;
            Eaten = false;
        }
        public enum side
        {
            White, Black
        }
        public side Side { get; set; }
        public bool Eaten { get; set; }
        public void SetCords(float[] cords)
        {
            SetX(cords[0]);
            SetY(cords[1]);
        }

        public abstract List<BoardSquare> GetPossiblePlaces(BoardSquare[,] squares);

        public int[] GetBoardSquareCords(BoardSquare[,] squares)
        {
            int x = 0, y = 0;
            for (int i = 0; i < squares.GetLength(0); i++)
            {
                for (int j = 0; j < squares.GetLength(1); j++)
                {
                    if (squares[i, j].IsInArea(this))
                    {
                        x = j;
                        y = i;
                        return new int[] { x, y };
                    }
                }
            }
            return new int[] { x, y };
        }
        public BoardSquare GetBoardSquare(BoardSquare[,] squares)
        {
            return squares[GetBoardSquareCords(squares)[1], GetBoardSquareCords(squares)[0]];
        }

        protected bool CheckIfPossible(int x, int y, bool limit, BoardSquare[,] squares, List<BoardSquare> possibilities)
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

using System;
using System.Collections.Generic;
using Android.Content.Res;
using Android.Graphics;
using Android.Widget;
using FinalProject.Pieces;

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



        protected bool CheckIfPossible(int x, int y, bool limit, BoardSquare[,] squares, List<BoardSquare> possibilities)
        {
            if (limit)
            {
                if (squares[y, x].CurrentPiece == null)
                {
                    if (CheckIfMoveLegal(MainActivity.boardGame.Squares[y, x]))
                    {
                        possibilities.Add(squares[y, x]);
                    }
                    return true;
                }
                else if (squares[y, x].CurrentPiece != null && squares[y, x].CurrentPiece.Side == this.Side)
                {
                    return false;
                }
                else
                {
                    if (CheckIfMoveLegal(MainActivity.boardGame.Squares[y, x]))
                    {
                        possibilities.Add(squares[y, x]);
                    }
                    return false;
                }
            }
            return false;
        }
        public bool CheckIfMoveLegal(BoardSquare target)
        {
            var c = target.CurrentPiece;
            var org = MainActivity.boardGame.GetBoardSquareByPiece(this);
            if ((int)MainActivity.boardGame.turn == (int)org.CurrentPiece.Side)
            {
                target.CurrentPiece = org.CurrentPiece;
                org.CurrentPiece = null;
                target.CurrentPiece.SetCords(target.Center);
                MainActivity.boardGame.GetYourKing(Side).OnCheck = false;
                MainActivity.boardGame.GetYourKing(Side).IsOnCheck(MainActivity.boardGame.Squares);
                org.CurrentPiece = target.CurrentPiece;
                target.CurrentPiece = c;
                org.CurrentPiece.SetCords(org.Center);
                if (!MainActivity.boardGame.GetYourKing(Side).OnCheck)
                {
                    return true;
                }
                return false;
            }
            return true;
        }
    }
}

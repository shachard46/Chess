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
        public Piece(float x, float y, int res, side side) : base(x, y, res, side == side.Black)
        {
            Side = side;
            Eaten = false;
        }
        public Piece(float[] cords, int res, side side) : this(cords[0], cords[1], res, side)
        {
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
                    if (CheckIfMoveLegal(GameActivity.boardGame.Squares[y, x]))
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
                    if (CheckIfMoveLegal(GameActivity.boardGame.Squares[y, x]))
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
            var org = GameActivity.boardGame.GetBoardSquareByPiece(this);
            if (org.CurrentPiece != null && (int)GameActivity.boardGame.turn == (int)org.CurrentPiece.Side)
            {
                target.CurrentPiece = org.CurrentPiece;
                org.CurrentPiece = null;
                GameActivity.boardGame.GetYourKing(Side).OnCheck = false;
                GameActivity.boardGame.GetYourKing(Side).IsOnCheck(GameActivity.boardGame.Squares);
                org.CurrentPiece = target.CurrentPiece;
                target.CurrentPiece = c;
                if (!GameActivity.boardGame.GetYourKing(Side).OnCheck)
                {
                    return true;
                }
                return false;
            }
            return true;
        }
    }
}

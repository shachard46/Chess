﻿using System;
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
        public bool IsOnCheck(BoardSquare[,] squares)
        {
            foreach(BoardSquare square in squares)
            {
                if(square.CurrentPiece != null)
                {
                    if (square.CurrentPiece.GetPossiblePlaces(squares).Contains(GetBoardSquare(squares)))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public bool CanCastle(BoardSquare[,] squares)
        {
            int x = GetBoardSquareCords(squares)[0],
                y = GetBoardSquareCords(squares)[1];
            bool can1 = true, can2 = true;
            for (int i = 1; i < 4; i++)
            {
                if (x + i < 7 && squares[y, x + i].CurrentPiece != null)
                {
                    can1 = false;
                }
                if(x - i > 0 && squares[y, x - i].CurrentPiece != null)
                {
                    can2 = false;
                }
                if(!can1 && !can2)
                {
                    return false;
                }
            }
            return (can1 || can2) && !hasMoved;
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
            return possibilities;
        }

        public void HasMoved(bool moved)
        {
            hasMoved = moved;
        }
    }
}

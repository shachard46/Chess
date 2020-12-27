﻿using System;
using System.Collections.Generic;
using Android.Graphics;
using Java.Util.Functions;
using Android.Media;

namespace FinalProject.Pieces
{
    public class Rook : Piece, CastledPiece
    {
        public Rook(float[] cords, int res, side side) : base(cords, res, side)
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
            checkBlocks(x, squares.GetLength(0) - 1, squares, possibilities);
            checkBlocks(squares.GetLength(1), y, squares, possibilities);
            //for (int i = 0; i < squares.GetLength(0); i++)
            //{
            //    if (squares[x, i].CurrentPiece == null)
            //    {
            //        possibilities.Add(squares[x, i]);
            //    }
            //    else if (squares[x, i].CurrentPiece != null && squares[x, i].CurrentPiece.Side == this.Side)
            //    {
            //        break;
            //    }
            //    else
            //    {
            //        possibilities.Add(squares[x, i]);
            //        break;
            //    }
            //}
            //for (int i = 0; i < squares.GetLength(1); i++)
            //{
            //    if (squares[x, i].CurrentPiece == null)
            //    {
            //        possibilities.Add(squares[i, y]);
            //    }
            //    else if (squares[i, y].CurrentPiece != null && squares[i, y].CurrentPiece.Side == this.Side)
            //    {
            //        break;
            //    }
            //    else
            //    {
            //        possibilities.Add(squares[i, y]);
            //        break;
            //    }
            //}
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


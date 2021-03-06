﻿using System;
using System.Collections.Generic;
using Android.Graphics;

namespace FinalProject.Pieces
{
    public class Bishop : Piece
    {
        public Bishop(float[] cords, Side side) : base(cords, side == Side.Black ? Resource.Drawable.black_bishop : Resource.Drawable.white_bishop, side)
        {
        }

        public override List<BoardSquare> GetPossiblePlaces(BoardSquare[,] squares)
        {
            List<BoardSquare> possibilities = new List<BoardSquare>();
            int x = GameActivity.boardGame.GetBoardSquareCords(this)[0],
                y = GameActivity.boardGame.GetBoardSquareCords(this)[1];
            bool first = true, second = true, third = true, fourth = true;
            for (int i = 0; i < squares.GetLength(1); i++)
            {
                if (first)
                {
                    int a = x + i + 1;
                    int b = y - i - 1;
                    first = CheckIfPossible(a, b, a < squares.GetLength(0) && b >= 0, squares, possibilities);
                }

                if (second)
                {
                    int a = x - i - 1;
                    int b = y - i - 1;
                    second = CheckIfPossible(a, b, a >= 0  && b >=0, squares, possibilities);
                }
                if (third)
                {
                    int a = x - i - 1;
                    int b = y + i + 1;
                    third = CheckIfPossible(a, b, a >= 0 && b < squares.GetLength(1), squares, possibilities);
                }

                if (fourth)
                {
                    int a = x + i + 1;
                    int b = y + i + 1;
                    fourth = CheckIfPossible(a, b, a < squares.GetLength(0) && b < squares.GetLength(1), squares, possibilities);
                }
            }
            return possibilities;
        }
    }
}


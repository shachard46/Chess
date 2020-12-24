using System;
using System.Collections.Generic;
using Android.Graphics;

namespace FinalProject
{
    public abstract class Piece : Image
    {
        public Piece(float x, float y, Bitmap bitmap, bool side, bool eaten) : base(x, y, bitmap)
        {
            Side = side;
            Eaten = eaten;
        }

        public bool Side { get; set; }
        public bool Eaten { get; set; }
        public void SetCords(int[] cords)
        {
            SetX(cords[0]);
            SetY(cords[1]);
        }

        public abstract List<BoardSquare> GetPossiblePlaces(BoardSquare[,] squares);

        public int[] GetBoardSquare(BoardSquare[,] squares)
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
                        break;
                    }
                }
            }
            return new int[]{x, y};
        }
    }
}

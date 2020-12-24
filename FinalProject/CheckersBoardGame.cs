using System;
using Android.Content;
using Android.Graphics;
using Android.Views;
using FinalProject.Pieces;

namespace FinalProject
{
    public class ChessBoardGame : View
    {
        private char[] soldiersOrder = { 'r', 'n', 'b', 'k', 'q', 'b', 'n', 'r', 'p' };
        private Piece[] black;
        private Piece[] white;
        private const int NUMBER_OF_SQUARES = 64;
        private BoardSquare[,] squares;


        public ChessBoardGame(Context context) : base(context)
        {
            squares = GetBoardSquares(0, 0, 0);
            black = new Piece[12];
            white = new Piece[12];
            //new black and white
            squares = GetBoardSquares(0, 0, 0);
        }
        public void DrawBoard(Canvas canvas)
        {
            canvas.DrawBitmap(null, 0, 0, null);
            for (int i = 0; i < 12; i++)
            {
                if(!black[i].Eaten)
                    black[i].Draw(canvas);
                if(!white[i].Eaten)
                    white[i].Draw(canvas);
            }
        }

        BoardSquare[,] GetBoardSquares(int leftTopCornerX, int leftTopCornerY, int rightTopCornerX)
        {
            int sideLength = (leftTopCornerX - rightTopCornerX) / 64;
            BoardSquare[,] squares = new BoardSquare[8, 8];
            for (int i = 0; i < NUMBER_OF_SQUARES / 8; i++)
            {
                int y = leftTopCornerY + i * sideLength + sideLength / 2;
                for (int j = 0; j < NUMBER_OF_SQUARES / 8; j++)
                {
                    int x = leftTopCornerX + j * sideLength + sideLength / 2;
                    squares[j, i] = new BoardSquare(x, y, null);
                }
            }
            return squares;
        }
    }
}

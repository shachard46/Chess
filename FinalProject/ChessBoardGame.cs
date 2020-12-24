using System;
using System.Collections.Generic;
using Android.Content;
using Android.Graphics;
using Android.Views;
using FinalProject.Pieces;
using SQLite;

namespace FinalProject
{
    public class ChessBoardGame : View
    {
        private char[] soldiersOrder = { 'r', 'n', 'b', 'k', 'q', 'b', 'n', 'r', 'p' };
        private Piece[] black;
        private Piece[] white;
        private const int NUMBER_OF_SQUARES = 64;
        private BoardSquare[,] squares;
        public GameState state;
        public enum GameState
        {
            Idle, Holding
        }


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
            for (int i = 0; i < 8; i++)
            {
                Paint paint = new Paint();
                paint.Color = i % 2 == 0 ? Color.White : Color.Black;
                for (int j = 0; j < 8; j++)
                {
                    paint.Color = paint.Color == Color.White ? Color.Black : Color.White;
                    canvas.DrawRect(j * 100, i * 100, 100 * j + 100, i * 100 + 100, paint);
                }
            }
            for (int i = 0; i < 12; i++)
            {
                if (!black[i].Eaten)
                    black[i].Draw(canvas);
                if (!white[i].Eaten)
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

        public void Move(BoardSquare source, BoardSquare destinaiton, Canvas canvas)
        {
            if (source.CurrentPiece.GetPossiblePlaces(squares).Contains(destinaiton))
            {
                source.CurrentPiece.SetCords(destinaiton.Center);
                if (destinaiton.CurrentPiece != null)
                {
                    destinaiton.CurrentPiece.Eaten = true;
                    destinaiton.CurrentPiece = source.CurrentPiece;
                }
            }
            state = GameState.Idle;
        }

        public void ShowPossibleMoves(BoardSquare clicked)
        {
            List<BoardSquare> possibles = clicked.CurrentPiece.GetPossiblePlaces(squares);
            foreach (BoardSquare square in possibles)
            {
                square.
            }
        }
    }
}

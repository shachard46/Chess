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
            black = new Piece[12];
            white = new Piece[12];
            //new black and white
            for (int i = 0; i < 8; i++)
            {
                Color color= i % 2 == 0 ? Color.White : Color.Black;
                for (int j = 0; j < 8; j++)
                {
                    color = color == Color.White ? Color.Black : Color.White;
                    squares[j, i] = new BoardSquare(j * 100 + 50, i * 100 + 50, null);
                    squares[j, i].Color = color;
                }
            }
        }
        public void DrawBoard(Canvas canvas)
        {
            foreach(BoardSquare square in squares)
            {
                Paint paint = new Paint();
                paint.Color = square.Color;
                canvas.DrawRect(square.Center[0] - 50, square.Center[1] - 50,
                    square.Center[0] + 50, square.Center[1] + 50, paint);
            }
            for (int i = 0; i < 12; i++)
            {
                if (!black[i].Eaten)
                    black[i].Draw(canvas);
                if (!white[i].Eaten)
                    white[i].Draw(canvas);
            }
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
                square.Color = Color.Gray;
            }
        }
    }
}

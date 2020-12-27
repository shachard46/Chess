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
        private Piece[] black;
        private Piece[] white;
        private BoardSquare[,] squares;
        private Piece played;
        private GameState state;
        public Turn turn = Turn.White;
        private enum GameState
        {
            Idle, Holding
        }
        public enum Turn
        {
            White, Black
        }

        public ChessBoardGame(Context context) : base(context)
        {
            for (int i = 0; i < 7; i += 6)
            {
                squares[0, i].CurrentPiece = new Rook(squares[0, i].Center,
                     i == 0 ? Resource.Drawable.black_rook : Resource.Drawable.white_rook,
                     i == 0 ? Piece.side.Black : Piece.side.White);
                squares[1, i].CurrentPiece = new Knight(squares[1, i].Center,
                    i == 0 ? Resource.Drawable.black_knight : Resource.Drawable.white_knight,
                    i == 0 ? Piece.side.Black : Piece.side.White);
                squares[2, i].CurrentPiece = new Bishop(squares[2, i].Center,
                    i == 0 ? Resource.Drawable.black_bishop : Resource.Drawable.white_bishop,
                    i == 0 ? Piece.side.Black : Piece.side.White);
                squares[3, i].CurrentPiece = new Queen(squares[3, i].Center,
                    i == 0 ? Resource.Drawable.black_queen : Resource.Drawable.white_queen,
                    i == 0 ? Piece.side.Black : Piece.side.White);
                squares[4, i].CurrentPiece = new King(squares[4, i].Center,
                    i == 0 ? Resource.Drawable.black_king : Resource.Drawable.white_king,
                    i == 0 ? Piece.side.Black : Piece.side.White);
                squares[5, i].CurrentPiece = new Bishop(squares[5, i].Center,
                    i == 0 ? Resource.Drawable.black_bishop : Resource.Drawable.white_bishop,
                    i == 0 ? Piece.side.Black : Piece.side.White);
                squares[6, i].CurrentPiece = new Knight(squares[6, i].Center,
                    i == 0 ? Resource.Drawable.black_knight : Resource.Drawable.white_knight,
                    i == 0 ? Piece.side.Black : Piece.side.White);
                squares[7, i].CurrentPiece = new Rook(squares[7, i].Center,
                    i == 0 ? Resource.Drawable.black_rook : Resource.Drawable.white_rook,
                    i == 0 ? Piece.side.Black : Piece.side.White);
            }
            for (int i = 0; i < 8; i++)
            {
                for (int j = 1; j < 8; j += 6)
                {
                    squares[j, i].CurrentPiece = new Pawn(squares[0, 0].Center,
                        i == 0 ? Resource.Drawable.black_pawn : Resource.Drawable.white_pawn,
                        i == 0 ? Piece.side.Black : Piece.side.White);
                }
            }
            for (int i = 0; i < 8; i++)
            {
                Color color = i % 2 == 0 ? Color.White : Color.Black;
                for (int j = 0; j < 8; j++)
                {
                    color = color == Color.White ? Color.Black : Color.White;
                    squares[j, i] = new BoardSquare(j * 100 + 50, i * 100 + 50, null);
                    squares[j, i].Paint.Color = color;
                }
            }
        }
        protected override void OnDraw(Canvas canvas)
        {
            base.OnDraw(canvas);
            foreach (BoardSquare square in squares)
            {
                canvas.DrawRect(square.Center[0] - 50, square.Center[1] - 50,
                    square.Center[0] + 50, square.Center[1] + 50, square.Paint);
                if (square.CurrentPiece != null)
                    square.CurrentPiece.Draw(canvas);
                if (state == GameState.Holding)
                    ShowPossibleMoves(played.GetBoardSquare(squares), canvas);
            }
        }

        public override bool OnTouchEvent(MotionEvent e)
        {
            switch (state)
            {
                case GameState.Idle:
                    played = GetSquareByCords(e.GetX(), e.GetY()).CurrentPiece;
                    if (played != null)
                        state = GameState.Holding;
                    break;
                case GameState.Holding:
                    if (Move(played.GetBoardSquare(squares), GetSquareByCords(e.GetX(), e.GetY())))
                    {
                        turn = turn == Turn.Black ? turn = Turn.White : Turn.Black;
                        state = GameState.Idle;
                    }
                    break;
            }
            return base.OnTouchEvent(e);
        }

        public bool Move(BoardSquare source, BoardSquare destinaiton)
        {
            if (source.CurrentPiece.GetPossiblePlaces(squares).Contains(destinaiton))
            {
                source.CurrentPiece.SetCords(destinaiton.Center);
                if (destinaiton.CurrentPiece != null)
                {
                    destinaiton.CurrentPiece.Eaten = true;
                    destinaiton.CurrentPiece = source.CurrentPiece;
                    source.CurrentPiece = null;
                }
                return true;
            }
            else if (destinaiton.CurrentPiece.Side.Equals(source.CurrentPiece.Side))
            {
                played = destinaiton.CurrentPiece;
            }
            return false;
        }

        public void ShowPossibleMoves(BoardSquare clicked, Canvas canvas)
        {
            if (clicked.CurrentPiece != null && (int)clicked.CurrentPiece.Side == (int)turn)
            {
                List<BoardSquare> possibles = clicked.CurrentPiece.GetPossiblePlaces(squares);
                foreach (BoardSquare square in possibles)
                {
                    canvas.DrawRect(square.Center[0] - 50, square.Center[1] - 50,
                    square.Center[0] + 50, square.Center[1] + 50, BoardSquare.possible);
                }
            }
        }
        public BoardSquare GetSquareByCords(float x, float y)
        {
            foreach (BoardSquare square in squares)
            {
                if (square.IsInArea(x, y))
                {
                    return square;
                }
            }
            return null;
        }
    }
}

using System;
using System.Collections.Generic;
using Android.Content;
using Android.Graphics;
using Android.Views;
using FinalProject.Pieces;
using SQLite;

namespace FinalProject
{
    public class BoardGame : View
    {
        private BoardSquare[,] squares;
        private BoardSquare played;
        private struct Constants
        {
            public const int SQUARE_SIDE = 90;
            public const int R_AND_C = 8;
        }
        private GameState state = GameState.Idle;
        public Turn turn = Turn.White;
        private enum GameState
        {
            Idle, Holding
        }
        public enum Turn
        {
            White, Black
        }

        public BoardGame(Context context) : base(context)
        {
            Image.View = this;
            squares = new BoardSquare[Constants.R_AND_C, Constants.R_AND_C];
            for (int i = 0; i < Constants.R_AND_C; i++)
            {
                Color color = i % 2 == 0 ? Color.White : Color.Black;
                for (int j = 0; j < Constants.R_AND_C; j++)
                {
                    color = color == Color.White ? Color.Black : Color.White;
                    squares[i, j] = new BoardSquare(
                        j * Constants.SQUARE_SIDE + Constants.SQUARE_SIDE / 2,
                        i * Constants.SQUARE_SIDE + Constants.SQUARE_SIDE / 2, null);
                    squares[i, j].Paint.Color = color;
                    squares[i, j].SideLength = Constants.SQUARE_SIDE;
                }
            }
            for (int i = 0; i < Constants.R_AND_C; i += 7)
            {
                squares[i, 0].CurrentPiece = new Rook(squares[i, 0].Center,
                     i == 0 ? Piece.side.Black : Piece.side.White);
                squares[i, 1].CurrentPiece = new Knight(squares[i, 1].Center,
                    i == 0 ? Piece.side.Black : Piece.side.White);
                squares[i, 2].CurrentPiece = new Bishop(squares[i, 2].Center,
                    i == 0 ? Piece.side.Black : Piece.side.White);
                squares[i, 3].CurrentPiece = new Queen(squares[i, 3].Center,
                    i == 0 ? Piece.side.Black : Piece.side.White);
                squares[i, 4].CurrentPiece = new King(squares[i, 4].Center,
                    i == 0 ? Piece.side.Black : Piece.side.White);
                squares[i, 5].CurrentPiece = new Bishop(squares[i, 5].Center,
                    i == 0 ? Piece.side.Black : Piece.side.White);
                squares[i, 6].CurrentPiece = new Knight(squares[i, 6].Center,
                    i == 0 ? Piece.side.Black : Piece.side.White);
                squares[i, 7].CurrentPiece = new Rook(squares[i, 7].Center,
                    i == 0 ? Piece.side.Black : Piece.side.White);
            }
            for (int i = 1; i < Constants.R_AND_C; i += 5)
                for (int j = 0; j < Constants.R_AND_C; j++)
                {
                    {
                        squares[i, j].CurrentPiece = new Pawn(squares[i, j].Center,
                            i == 1 ? Piece.side.Black : Piece.side.White);
                    }
                }
        }
        protected override void OnDraw(Canvas canvas)
        {
            base.OnDraw(canvas);
            foreach (BoardSquare square in squares)
            {
                canvas.DrawRect(
                    square.Center[0] - Constants.SQUARE_SIDE / 2,
                    square.Center[1] - Constants.SQUARE_SIDE / 2,
                    square.Center[0] + Constants.SQUARE_SIDE / 2,
                    square.Center[1] + Constants.SQUARE_SIDE / 2, square.Paint);
                if (square.CurrentPiece != null && !square.CurrentPiece.Eaten)
                {
                    //square.CurrentPiece.Draw(canvas);
                    using (Paint p = new Paint())
                    {
                        if (square.CurrentPiece is Pawn)
                            p.Color = square.CurrentPiece.Side == Piece.side.Black ? Color.Red : Color.Blue;
                        if (square.CurrentPiece is King)
                            p.Color = Color.Brown;
                        if (square.CurrentPiece is Rook)
                            p.Color = Color.Green;
                        canvas.DrawCircle(square.CurrentPiece.GetX(), square.CurrentPiece.GetY(), 30, p);
                    }
                }
                if (state == GameState.Holding)
                    ShowPossibleMoves(played, canvas);
            }
        }

        public override bool OnTouchEvent(MotionEvent e)
        {
            switch (state)
            {
                case GameState.Idle:
                    played = GetSquareByCords(e.GetX(), e.GetY());
                    if (played != null && played.CurrentPiece != null)
                    {
                        if ((int)played.CurrentPiece.Side != (int)turn)
                            played = null;
                    }
                    if (played != null && played.CurrentPiece != null)
                    {
                        state = GameState.Holding;
                        Invalidate();
                    }
                    break;
                case GameState.Holding:
                    if (Move(played, GetSquareByCords(e.GetX(), e.GetY())))
                    {
                        turn = turn == Turn.Black ? turn = Turn.White : Turn.Black;
                        state = GameState.Idle;
                        Invalidate();
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
                }
                destinaiton.CurrentPiece = source.CurrentPiece;
                source.CurrentPiece = null;
                return true;
            }
            else if (destinaiton.CurrentPiece != null && destinaiton.CurrentPiece.Side.Equals(source.CurrentPiece.Side))
            {
                played = destinaiton;
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
                    float stroke = BoardSquare.possible.StrokeWidth;
                    canvas.DrawRect(
                        square.Center[0] - Constants.SQUARE_SIDE / 2,
                        square.Center[1] - Constants.SQUARE_SIDE / 2,
                        square.Center[0] + Constants.SQUARE_SIDE / 2,
                        square.Center[1] + Constants.SQUARE_SIDE / 2,
                        BoardSquare.possible);
                }
                Invalidate();
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

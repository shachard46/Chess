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
            }
            foreach (BoardSquare square in squares)
            {
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
                        if (square.CurrentPiece is Bishop)
                            p.Color = Color.Cyan;
                        canvas.DrawCircle(square.CurrentPiece.GetX(), square.CurrentPiece.GetY(), Constants.SQUARE_SIDE * 0.4f, p);
                    }
                }
            }

            //{

            //}
            using (Paint p = new Paint())
            {
                {
                    if (played != null && played.CurrentPiece != null)
                    {
                        //played.CurrentPiece.Draw(canvas);
                        if (played.CurrentPiece is Pawn)
                            p.Color = played.CurrentPiece.Side == Piece.side.Black ? Color.Red : Color.Blue;
                        if (played.CurrentPiece is King)
                            p.Color = Color.Brown;
                        if (played.CurrentPiece is Rook)
                            p.Color = Color.Green;
                        if (played.CurrentPiece is Bishop)
                            p.Color = Color.Cyan;
                        canvas.DrawCircle(played.CurrentPiece.GetX(), played.CurrentPiece.GetY(), 30, p);
                    }
                }
            }
        }

        public override bool OnTouchEvent(MotionEvent e)
        {
            if (state == GameState.Idle)
            {
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
            }
            if (state == GameState.Holding)
            {
                if (e.Action == MotionEventActions.Down)
                {
                    BoardSquare clicked = GetSquareByCords(e.GetX(), e.GetY());
                    if (clicked.CurrentPiece == null || (int)clicked.CurrentPiece.Side != (int)turn)
                    {
                        played.CurrentPiece.SetCords(new float[] { e.GetX(), e.GetY() });
                    }
                    else
                    {
                        if (played.CurrentPiece is King && clicked.CurrentPiece is Rook && clicked.CurrentPiece.Side == played.CurrentPiece.Side)
                        {

                        }
                        else
                        {
                            played = GetSquareByCords(e.GetX(), e.GetY());
                        }
                    }
                }
                else if (e.Action == MotionEventActions.Move)
                {
                    played.CurrentPiece.SetCords(new float[] { e.GetX(), e.GetY() });
                }
                else if (e.Action == MotionEventActions.Up)
                {
                    var clicked = GetSquareByCords(e.GetX(), e.GetY());
                    if (played.CurrentPiece is King && clicked.CurrentPiece is Rook && clicked.CurrentPiece.Side == played.CurrentPiece.Side)
                    {
                        if (((CastledPiece)(played.CurrentPiece)).CanCastle(squares)
                                && ((CastledPiece)(clicked.CurrentPiece)).CanCastle(squares))
                        {
                            if (Math.Abs(played.Center[0] - clicked.Center[0]) > 2 * Constants.SQUARE_SIDE)
                            {
                                Castle(played, clicked);
                                turn = turn == Turn.Black ? turn = Turn.White : Turn.Black;
                                state = GameState.Idle;
                            }
                        }
                    }
                    else if (Move(played, clicked))
                    {
                        turn = turn == Turn.Black ? turn = Turn.White : Turn.Black;
                        state = GameState.Idle;
                    }
                    else
                    {
                        played.CurrentPiece.SetCords(played.Center);
                    }
                }
                Invalidate();
            }
            return true;
        }

        public bool Move(BoardSquare source, BoardSquare destinaiton)
        {
            GetYourKing(source.CurrentPiece.Side).IsOnCheck(squares);
            source.CurrentPiece.SetCords(source.Center);
            if (source.CurrentPiece.GetPossiblePlaces(squares).Contains(destinaiton))
            {
                source.CurrentPiece.SetCords(destinaiton.Center);
                if (destinaiton.CurrentPiece != null)
                {
                    destinaiton.CurrentPiece.Eaten = true;
                }
                destinaiton.CurrentPiece = source.CurrentPiece;
                source.CurrentPiece = null;
                if (destinaiton.CurrentPiece is CastledPiece)
                {
                    ((CastledPiece)destinaiton.CurrentPiece).HasMoved(true);
                }
                return true;
            }
            else if (destinaiton.CurrentPiece != null && destinaiton.CurrentPiece.Side.Equals(source.CurrentPiece.Side))
            {
                played = destinaiton;
            }
            return false;
        }
        public void Castle(BoardSquare king, BoardSquare rook)
        {
            int direction = king.Center[0] > rook.Center[0] ? -1 : 1;
            BoardSquare kingDest = GetSquareByCords(king.Center[0] +
                direction  * Math.Abs(king.Center[0] - rook.Center[0] + direction * Constants.SQUARE_SIDE), king.Center[1]);
            BoardSquare rookDest = GetSquareByCords(rook.Center[0]
                - direction * 2 * Constants.SQUARE_SIDE, rook.Center[1]);
            king.CurrentPiece.SetCords(kingDest.Center);
            kingDest.CurrentPiece = king.CurrentPiece;
            king.CurrentPiece = null;
            rook.CurrentPiece.SetCords(rookDest.Center);
            rookDest.CurrentPiece = rook.CurrentPiece;
            rook.CurrentPiece = null;
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
        public int[] GetBoardSquareCords(Piece piece)
        {
            int x = 0, y = 0;
            for (int i = 0; i < squares.GetLength(0); i++)
            {
                for (int j = 0; j < squares.GetLength(1); j++)
                {
                    if (squares[i, j].IsInArea(piece))
                    {
                        x = j;
                        y = i;
                        return new int[] { x, y };
                    }
                }
            }
            return new int[] { x, y };
        }
        public BoardSquare GetBoardSquareByPiece(Piece piece)
        {
            return squares[GetBoardSquareCords(piece)[1], GetBoardSquareCords(piece)[0]];
        }

        public King GetYourKing(Piece.side side)
        {
            foreach(BoardSquare square in squares)
            {
                if(square.CurrentPiece is King && square.CurrentPiece.Side == side)
                {
                    return (King)square.CurrentPiece;
                }
            }
            return null;
        }
    }
}

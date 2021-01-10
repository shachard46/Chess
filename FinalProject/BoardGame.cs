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
        private Text gameOver;
        private struct Constants
        {
            public const int SQUARE_SIDE = 85;
            public const int R_AND_C = 8;
        }
        private GameState state = GameState.Idle;
        public Turn turn = Turn.White;

        public BoardSquare[,] Squares { get => squares; set => squares = value; }

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
            Squares = new BoardSquare[Constants.R_AND_C, Constants.R_AND_C];
            for (int i = 0; i < Constants.R_AND_C; i++)
            {
                Color color = i % 2 == 0 ? Color.White : Color.Black;
                for (int j = 0; j < Constants.R_AND_C; j++)
                {
                    color = color == Color.White ? Color.Black : Color.White;
                    Rectangle rectangle = new Rectangle(
                        j * Constants.SQUARE_SIDE + Constants.SQUARE_SIDE * 1.25f,
                        i * Constants.SQUARE_SIDE + Constants.SQUARE_SIDE * 1.25f,
                        Constants.SQUARE_SIDE, Constants.SQUARE_SIDE, color);
                    Squares[i, j] = new BoardSquare(rectangle, null);
                }
            }
            for (int i = 0; i < Constants.R_AND_C; i += 7)
            {
                Squares[i, 0].CurrentPiece = new Rook(Squares[i, 0].Center,
                     i == 0 ? Piece.side.Black : Piece.side.White);
                Squares[i, 1].CurrentPiece = new Knight(Squares[i, 1].Center,
                    i == 0 ? Piece.side.Black : Piece.side.White);
                Squares[i, 2].CurrentPiece = new Bishop(Squares[i, 2].Center,
                    i == 0 ? Piece.side.Black : Piece.side.White);
                Squares[i, 3].CurrentPiece = new Queen(Squares[i, 3].Center,
                    i == 0 ? Piece.side.Black : Piece.side.White);
                Squares[i, 4].CurrentPiece = new King(Squares[i, 4].Center,
                    i == 0 ? Piece.side.Black : Piece.side.White);
                Squares[i, 5].CurrentPiece = new Bishop(Squares[i, 5].Center,
                    i == 0 ? Piece.side.Black : Piece.side.White);
                Squares[i, 6].CurrentPiece = new Knight(Squares[i, 6].Center,
                    i == 0 ? Piece.side.Black : Piece.side.White);
                Squares[i, 7].CurrentPiece = new Rook(Squares[i, 7].Center,
                    i == 0 ? Piece.side.Black : Piece.side.White);
            }
            for (int i = 1; i < Constants.R_AND_C; i += 5)
            {
                for (int j = 0; j < Constants.R_AND_C; j++)
                {
                    {
                        Squares[i, j].CurrentPiece = new Pawn(Squares[i, j].Center,
                            i == 1 ? Piece.side.Black : Piece.side.White);
                    }
                }
            }
            gameOver = new Text("", 0, 0, Color.Red);
        }
        protected override void OnDraw(Canvas canvas)
        {
            base.OnDraw(canvas);
            switch (IsGameOver())
            {
                case 0:
                    gameOver.setText("Black Wins");
                    break;
                case 1:
                    gameOver.setText("White Wins");
                    break;
                case 2:
                    gameOver.setText("Draw");
                    break;
                default:
                    DrawBoard(canvas);
                    DrawPieces(canvas);
                    break;
            }
            gameOver.SetX(canvas.Width / 2);
            gameOver.SetY(canvas.Height / 2);
            gameOver.Draw(canvas);
        }

        private void DrawBoard(Canvas canvas)
        {
            Rectangle squareIndexRect = new Rectangle(Constants.SQUARE_SIDE * 0.25f,
                Constants.SQUARE_SIDE * 0.375f, Constants.SQUARE_SIDE * 0.75f, Constants.SQUARE_SIDE, Color.White);
            Text squareIndex = new Text("", Constants.SQUARE_SIDE * 0.25f,
                Constants.SQUARE_SIDE * 0.375f, Color.Black, 30);

            for (int i = 0; i < Constants.R_AND_C; i++)
            {
                char horz = (char)(i + 65);
                int vert = i + 1;
                squareIndex.SetX(squareIndex.GetX() + Constants.SQUARE_SIDE);
                squareIndexRect.SetX(squareIndexRect.GetX() + Constants.SQUARE_SIDE);

                squareIndex.setText(horz.ToString());

                squareIndexRect.Draw(canvas);
                squareIndex.Draw(canvas);

                squareIndex.setText(vert.ToString());
                squareIndexRect.Flip().Draw(canvas);
                squareIndex.Flip().Draw(canvas);
            }
            foreach (BoardSquare square in Squares)
            {
                square.Draw(canvas);
            }
        }

        private void DrawPieces(Canvas canvas)
        {
            foreach (BoardSquare square in Squares)
            {
                if (square.CurrentPiece != null && !square.CurrentPiece.Eaten)
                {
                    //square.CurrentPiece.Draw(canvas);
                    using (Paint p = new Paint())
                    {
                        Text text = new Text("", square.CurrentPiece.GetX(),
                            square.CurrentPiece.GetY(), Color.Black, 30);
                        if (square.CurrentPiece is Pawn)
                            text.setText("P");
                        if (square.CurrentPiece is King)
                            text.setText("K");
                        if (square.CurrentPiece is Rook)
                            text.setText("R");
                        if (square.CurrentPiece is Bishop)
                            text.setText("B");
                        if (square.CurrentPiece is Queen)
                            text.setText("Q");
                        if (square.CurrentPiece is Knight)
                            text.setText("N");
                        p.Color = square.CurrentPiece.Side == Piece.side.Black ? Color.LightSalmon : Color.LightBlue;
                        canvas.DrawCircle(square.CurrentPiece.GetX(), square.CurrentPiece.GetY(), Constants.SQUARE_SIDE * 0.4f, p);
                        text.Draw(canvas);
                    }
                }
            }

            using (Paint p = new Paint())
            {
                {
                    if (played != null && played.CurrentPiece != null)
                    {
                        //played.CurrentPiece.Draw(canvas);
                        Text text = new Text("", played.CurrentPiece.GetX(), played.CurrentPiece.GetY(), Color.Black);
                        text.TextSize = 30;
                        if (played.CurrentPiece is Pawn)
                            text.setText("P");
                        if (played.CurrentPiece is King)
                            text.setText("K");
                        if (played.CurrentPiece is Rook)
                            text.setText("R");
                        if (played.CurrentPiece is Bishop)
                            text.setText("B");
                        if (played.CurrentPiece is Queen)
                            text.setText("Q");
                        if (played.CurrentPiece is Knight)
                            text.setText("N");
                        p.Color = played.CurrentPiece.Side == Piece.side.Black ? Color.Salmon : Color.DeepSkyBlue;
                        canvas.DrawCircle(played.CurrentPiece.GetX(), played.CurrentPiece.GetY(), Constants.SQUARE_SIDE * 0.4f, p);
                        text.Draw(canvas);
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
                        if (((CastledPiece)(played.CurrentPiece)).CanCastle(Squares)
                                && ((CastledPiece)(clicked.CurrentPiece)).CanCastle(Squares))
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
            GetYourKing(source.CurrentPiece.Side).IsOnCheck(Squares);
            source.CurrentPiece.SetCords(source.Center);
            if (source.CurrentPiece.GetPossiblePlaces(Squares).Contains(destinaiton))
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
                direction * Math.Abs(king.Center[0] - rook.Center[0] + direction * Constants.SQUARE_SIDE), king.Center[1]);
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
            foreach (BoardSquare square in Squares)
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
            for (int i = 0; i < Squares.GetLength(0); i++)
            {
                for (int j = 0; j < Squares.GetLength(1); j++)
                {
                    if (Squares[i, j].IsInArea(piece))
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
            return Squares[GetBoardSquareCords(piece)[1], GetBoardSquareCords(piece)[0]];
        }

        public King GetYourKing(Piece.side side)
        {
            foreach (BoardSquare square in Squares)
            {
                if (square.CurrentPiece is King && square.CurrentPiece.Side == side)
                {
                    return (King)square.CurrentPiece;
                }
            }
            return null;
        }
        public int IsGameOver()
        {
            bool black = true, white = true;
            foreach (BoardSquare square in squares)
            {
                if (square.CurrentPiece != null)
                {
                    if (square.CurrentPiece.Side == Piece.side.White && black)
                    {
                        black = square.CurrentPiece.GetPossiblePlaces(squares).Count == 0;
                    }
                    else if (square.CurrentPiece.Side == Piece.side.Black && white)
                    {
                        white = square.CurrentPiece.GetPossiblePlaces(squares).Count == 0;
                    }
                }
            }
            if (black)
            {
                if (!GetYourKing(Piece.side.White).OnCheck)
                    return 2;
                return 0;
            }
            if (white)
            {
                if (!GetYourKing(Piece.side.Black).OnCheck)
                    return 2;
                return 1;
            }
            else
                return -1;
        }
    }
}

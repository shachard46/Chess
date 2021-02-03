using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Views;
using Android.Widget;
using FinalProject.Pieces;
using SQLite;

namespace FinalProject
{
    public class BoardGame : View
    {
        private static Context context;
        public enum GameState
        {
            Idle, Holding, GameOver, Promotion
        }
        public enum Turn
        {
            White, Black
        }
        public struct Constants
        {
            public static int SQUARE_SIDE = Util.convertDpToPixel(37, context);
            public const int R_AND_C = 8;
        }

        private Timer whiteTimer;
        private Timer blackTimer;

        private BoardSquare[,] squares;
        private BoardSquare[] promotionSquares;

        private BoardSquare played;
        private Text gameOver;
        private bool isOnline;
        private GameActions actions;
        public static Piece.side yourSide; // destined for online
        private GameState state = GameState.Idle;
        public Turn turn = Turn.White;
        private bool hint;
        private int duration;
        public BoardSquare[,] Squares { get => squares; set => squares = value; }
        public BoardSquare Played { get => played; set => played = value; }
        public GameState State { get => state; set => state = value; }
        public Timer WhiteTimer { get => whiteTimer; set => whiteTimer = value; }
        public Timer BlackTimer { get => blackTimer; set => blackTimer = value; }

        public BoardGame(Context context, TimerHandler whiteHandler, TimerHandler blackHandler, bool hint, int duration) : base(context)
        {
            BoardGame.context = context;
            this.hint = hint;
            this.duration = duration;
            actions = new GameActions(this);
            Image.View = this;
            GenerateBoard();
            GeneratePieces(Piece.side.Black, Piece.side.White);
            GeneratePromotionChooser();
            gameOver = new Text("", 0, 0, Color.Red);
            whiteTimer = new Timer(whiteHandler, 0, Turn.White);
            blackTimer = new Timer(blackHandler, 0, Turn.Black);
            whiteTimer.Start();
        }

        private void GenerateBoard()
        {
            squares = new BoardSquare[Constants.R_AND_C, Constants.R_AND_C];
            promotionSquares = new BoardSquare[4];
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
        }
        private void GeneratePieces(Piece.side yourSide, Piece.side opponetSide)
        {
            BoardGame.yourSide = yourSide;
            for (int i = 0; i < Constants.R_AND_C; i += 7)
            {
                Piece.side side = i == 0 ? yourSide : opponetSide;
                Squares[i, 0].CurrentPiece = new Rook(Squares[i, 0].Center, side);
                Squares[i, 1].CurrentPiece = new Knight(Squares[i, 1].Center, side);
                Squares[i, 2].CurrentPiece = new Bishop(Squares[i, 2].Center, side);
                Squares[i, 3].CurrentPiece = new Queen(Squares[i, 3].Center, side);
                Squares[i, 4].CurrentPiece = new King(Squares[i, 4].Center, side);
                Squares[i, 5].CurrentPiece = new Bishop(Squares[i, 5].Center, side);
                Squares[i, 6].CurrentPiece = new Knight(Squares[i, 6].Center, side);
                Squares[i, 7].CurrentPiece = new Rook(Squares[i, 7].Center, side);
            }
            for (int i = 1; i < Constants.R_AND_C; i += 5)
            {
                for (int j = 0; j < Constants.R_AND_C; j++)
                {
                    {
                        Squares[i, j].CurrentPiece = new Pawn(Squares[i, j].Center,
                            i == 1 ? yourSide : opponetSide);
                    }
                }
            }
        }
        private void GeneratePromotionChooser()
        {
            for (int i = 0; i < 4; i++)
            {
                promotionSquares[i] = new BoardSquare(new Rectangle(
                    squares[4, 0].Center[0] + Constants.SQUARE_SIDE / 2 + 2 * i * Constants.SQUARE_SIDE,
                    squares[4, 0].Center[1], Constants.SQUARE_SIDE * 2, Constants.SQUARE_SIDE * 2, Color.White), null);
            }
        }
        protected override void OnDraw(Canvas canvas)
        {
            base.OnDraw(canvas);
            switch (IsGameOver())
            {
                case 0:
                    gameOver.setText("Black Wins");
                    state = GameState.GameOver;
                    break;
                case 1:
                    gameOver.setText("White Wins");
                    state = GameState.GameOver;
                    break;
                case 2:
                    gameOver.setText("Draw");
                    state = GameState.GameOver;
                    break;
            }
            DrawBoard(canvas);
            DrawPieces(canvas);
            if (state == GameState.Holding && hint)
            {
                ShowPossibleMoves(played, canvas);
            }
            gameOver.SetX(canvas.Width / 2);
            gameOver.SetY(canvas.Height / 2);
            gameOver.Draw(canvas);
        }

        private void DrawBoard(Canvas canvas)
        {
            for (int s = 0; s < 2; s++)
            {

                Rectangle squareIndexRect = new Rectangle(Constants.SQUARE_SIDE * 0.25f,
                    Constants.SQUARE_SIDE * 0.375f
                    + s * (Constants.R_AND_C * Constants.SQUARE_SIDE + Constants.SQUARE_SIDE * 0.75f),
                    Constants.SQUARE_SIDE * 0.75f, Constants.SQUARE_SIDE, Color.White);
                Text squareIndex = new Text("", Constants.SQUARE_SIDE * 0.25f,
                    Constants.SQUARE_SIDE * 0.375f
                    + s * (Constants.R_AND_C * Constants.SQUARE_SIDE + Constants.SQUARE_SIDE * 0.75f), Color.Black, 30);

                for (int i = 0; i < Constants.R_AND_C; i++)
                {
                    char horz = (char)(i + 65);
                    int vert = i + 1;
                    squareIndex.SetX(squareIndex.GetX() + Constants.SQUARE_SIDE);
                    squareIndexRect.SetX(squareIndexRect.GetX() + Constants.SQUARE_SIDE);

                    squareIndex.setText(horz.ToString());

                    squareIndexRect.Draw(canvas);
                    canvas.Save();
                    canvas.Rotate(180 * s, squareIndex.GetX(), squareIndex.GetY());
                    squareIndex.Draw(canvas);
                    canvas.Restore();

                    squareIndex.setText(vert.ToString());
                    squareIndexRect.Flip().Draw(canvas);
                    canvas.Save();
                    canvas.Rotate(180 * s, squareIndex.Flip().GetX(), squareIndex.Flip().GetY());
                    squareIndex.Flip().Draw(canvas);
                    canvas.Restore();

                }
            }
            foreach (BoardSquare square in Squares)
            {
                square.Draw(canvas);
            }
            if (state == GameState.Promotion)
            {
                DrawPromotionChooser(canvas);
            }
        }

        private void DrawPromotionChooser(Canvas canvas)
        {
            Image piece;
            var res = turn == Turn.Black ? new int[]
            {
                Resource.Drawable.black_rook,
                Resource.Drawable.black_bishop,
                Resource.Drawable.black_queen,
                Resource.Drawable.black_knight,
            } : new int[]
            {
                Resource.Drawable.white_rook,
                Resource.Drawable.white_bishop,
                Resource.Drawable.white_queen,
                Resource.Drawable.white_knight,
            };
            for (int i = 0; i < 4; i++)
            {
                using (Paint p = new Paint())
                {
                    p.Color = turn == Turn.Black ? Color.LightSalmon : Color.LightBlue;
                    canvas.DrawCircle(promotionSquares[i].Center[0], promotionSquares[i].Center[1], Constants.SQUARE_SIDE * 0.8f, p);
                }
                piece = new Image(promotionSquares[i].Center[0],
                    promotionSquares[i].Center[1], Constants.SQUARE_SIDE * 2, res[i], (int)turn == (int)yourSide);
                piece.Draw(canvas);
            }
            //    var label = new string[]
            //{
            //    "R", "B", "Q", "N"
            //};
            //using (Paint p = new Paint())
            //{
            //    Text text = new Text("", 0, 0, Color.Black, 50);
            //    p.Color = turn == Turn.Black ? Color.LightSalmon : Color.LightBlue;
            //    for (int i = 0; i < 4; i++)
            //    {
            //        text.setText(label[i]);
            //        text.SetX(promotionSquares[i].Center[0]);
            //        text.SetY(promotionSquares[i].Center[1]);
            //        canvas.DrawCircle(promotionSquares[i].Center[0], promotionSquares[i].Center[1], Constants.SQUARE_SIDE * 0.8f, p);
            //        text.Draw(canvas);
            //    }
            //}

        }

        private void DrawPieces(Canvas canvas)
        {
            foreach (BoardSquare square in Squares)
            {
                if (square.CurrentPiece != null && !square.CurrentPiece.Eaten)
                {
                    square.CurrentPiece.Draw(canvas);
                }
            }

            using (Paint p = new Paint())
            {
                {
                    if (played != null && played.CurrentPiece != null)
                    {
                        played.CurrentPiece.Draw(canvas);
                    }
                }
            }
        }
        public override bool OnTouchEvent(MotionEvent e)
        {
            switch (state)
            {
                case GameState.Idle:
                    SetPlayed(e.GetX(), e.GetY());
                    break;
                case GameState.Holding:
                    if (e.Action == MotionEventActions.Down)
                    {
                        BoardSquare clicked = GetSquareByCords(e.GetX(), e.GetY());
                        if (clicked.CurrentPiece == null || (int)clicked.CurrentPiece.Side != (int)turn)
                        {
                            played.CurrentPiece.SetCords(new float[] { e.GetX(), e.GetY() });
                        }
                        else
                        {
                            if (!(played.CurrentPiece is King && clicked.CurrentPiece is Rook && clicked.CurrentPiece.Side == played.CurrentPiece.Side))
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
                                    actions.Castle(played, clicked);
                                    turn = turn == Turn.Black ? turn = Turn.White : Turn.Black;
                                    state = GameState.Idle;
                                }
                            }
                        }
                        else if (actions.Move(played, clicked))
                        {
                            if (state != GameState.Promotion)
                            {
                                turn = turn == Turn.Black ? turn = Turn.White : Turn.Black;
                                if (turn == Turn.Black) blackTimer.Start(); else whiteTimer.Start();
                                state = GameState.Idle;
                            }
                        }
                        else
                        {
                            played.CurrentPiece.SetCords(played.Center);
                        }
                    }
                    Invalidate();
                    break;
                case GameState.GameOver:
                    Invalidate();
                    break;
                case GameState.Promotion:
                    for (int i = 0; i < promotionSquares.Length; i++)
                    {
                        if (promotionSquares[i].IsInArea(e.GetX(), e.GetY()))
                        {
                            actions.Promote(i);
                        }
                    }
                    break;
            }
            return true;
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
                    if (Squares[i, j].CurrentPiece != null && Squares[i, j].CurrentPiece.Equals(piece))
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

        public void ShowPossibleMoves(BoardSquare clicked, Canvas canvas)
        {
            if (clicked.CurrentPiece != null && (int)clicked.CurrentPiece.Side == (int)turn)
            {
                List<BoardSquare> possibles = clicked.CurrentPiece.GetPossiblePlaces(squares);
                Rectangle margins;
                foreach (BoardSquare square in possibles)
                {
                    canvas.DrawRect(
                        square.Center[0] - Constants.SQUARE_SIDE / 2,
                        square.Center[1] - Constants.SQUARE_SIDE / 2,
                        square.Center[0] + Constants.SQUARE_SIDE / 2,
                        square.Center[1] + Constants.SQUARE_SIDE / 2,
                        BoardSquare.possible);
                    //float stroke = BoardSquare.possible.StrokeWidth;
                    //margins = new Rectangle(square.Center[0],
                    //    square.Center[1], Constants.SQUARE_SIDE, Constants.SQUARE_SIDE, Color.Blue);
                    //margins.SetPainter(BoardSquare.possible);
                    //margins.Draw(canvas);
                }
            }
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
                        black = square.CurrentPiece.GetPossiblePlaces(squares).Count == 0 || whiteTimer.IsOver(duration);
                    }
                    else if (square.CurrentPiece.Side == Piece.side.Black && white)
                    {
                        white = square.CurrentPiece.GetPossiblePlaces(squares).Count == 0 || blackTimer.IsOver(duration);
                    }
                }
            }
            if (black)
            {
                if (!GetYourKing(Piece.side.White).OnCheck && !whiteTimer.IsOver(duration))
                {
                    Invalidate();
                    return 2;
                }
                Invalidate();
                return 0;
            }
            if (white)
            {
                if (!GetYourKing(Piece.side.Black).OnCheck && !blackTimer.IsOver(duration))
                {
                    Invalidate();
                    return 2;
                }
                Invalidate();
                return 1;
            }
            else
            {
                Invalidate();
                return -1;
            }
        }

        protected void SetPlayed(float x, float y)
        {
            played = GetSquareByCords(x, y);
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
    }
}

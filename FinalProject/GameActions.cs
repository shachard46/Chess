using System;
using FinalProject.Pieces;

namespace FinalProject
{
    public class GameActions
    {
        BoardGame boardGame;
        public GameActions(BoardGame boardGame)
        {
            this.boardGame = boardGame;
        }
        public bool Move(BoardSquare source, BoardSquare destination)
        {
            source.CurrentPiece.SetCords(source.Center);
            if (source.CurrentPiece.GetPossiblePlaces(boardGame.Squares).Contains(destination))
            {
                source.CurrentPiece.SetCords(destination.Center);
                if (destination.CurrentPiece != null)
                {
                    destination.CurrentPiece.Eaten = true;
                }
                destination.CurrentPiece = source.CurrentPiece;
                source.CurrentPiece = null;
                if (destination.CurrentPiece is Pawn)
                {
                    var temp = new BoardSquare[64];
                    int w = BoardGame.Constants.R_AND_C; // width
                    int h = BoardGame.Constants.R_AND_C; // height

                    for (int y = 0; y < h; y++)
                    {
                        for (int x = 0; x < w; x++)
                        {
                            temp[BoardGame.Constants.R_AND_C * y + x] = boardGame.Squares[y, x];
                        }
                    }
                    if (Array.IndexOf(temp, destination) < 8 || Array.IndexOf(temp, destination) > 55)
                    {
                        boardGame.State = BoardGame.GameState.Promotion;
                        boardGame.Played = destination;
                        boardGame.Invalidate();
                    }
                }
                if (destination.CurrentPiece is CastledPiece)
                {
                    ((CastledPiece)destination.CurrentPiece).HasMoved(true);
                }
                return true;
            }
            else if (destination != null && destination.CurrentPiece != null &&
                destination.CurrentPiece.side.Equals(source.CurrentPiece.side))
            {
                boardGame.Played = destination;
            }
            return false;
        }

        public void Promote(int type)
        {

            switch (type)
            {
                case 0:
                    boardGame.Played.CurrentPiece = new Rook(boardGame.Played.Center, boardGame.Played.CurrentPiece.side,
                        boardGame.Played.CurrentPiece.Res);
                    break;
                case 1:
                    boardGame.Played.CurrentPiece = new Bishop(boardGame.Played.Center, boardGame.Played.CurrentPiece.side,
                        boardGame.Played.CurrentPiece.Res);
                    break;
                case 2:
                    boardGame.Played.CurrentPiece = new Queen(boardGame.Played.Center, boardGame.Played.CurrentPiece.side,
                        boardGame.Played.CurrentPiece.Res);
                    break;
                case 3:
                    boardGame.Played.CurrentPiece = new Knight(boardGame.Played.Center, boardGame.Played.CurrentPiece.side,
                        boardGame.Played.CurrentPiece.Res);
                    break;
            }
            boardGame.turn = boardGame.turn == BoardGame.Turn.Black ? boardGame.turn = BoardGame.Turn.White : BoardGame.Turn.Black;
            boardGame.State = BoardGame.GameState.Idle;
            boardGame.Played = null;
            boardGame.Invalidate();
        }

        public void Castle(BoardSquare king, BoardSquare rook)
        {
            int direction = king.Center[0] > rook.Center[0] ? -1 : 1;
            BoardSquare kingDest = boardGame.GetSquareByCords(king.Center[0] +
                direction * Math.Abs(king.Center[0] - rook.Center[0] + direction * BoardGame.Constants.SQUARE_SIDE), king.Center[1]);
            BoardSquare rookDest = boardGame.GetSquareByCords(rook.Center[0]
                - direction * 2 * BoardGame.Constants.SQUARE_SIDE, rook.Center[1]);
            king.CurrentPiece.SetCords(kingDest.Center);
            kingDest.CurrentPiece = king.CurrentPiece;
            king.CurrentPiece = null;
            rook.CurrentPiece.SetCords(rookDest.Center);
            rookDest.CurrentPiece = rook.CurrentPiece;
            rook.CurrentPiece = null;
        }
    }
}

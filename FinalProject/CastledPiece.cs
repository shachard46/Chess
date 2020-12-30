using System;
using FinalProject.Pieces;

namespace FinalProject
{
    public interface CastledPiece
    {
        //int CanCastle(BoardGame bigCasle, BoardGame smallCastle);
        bool CanCastle(BoardSquare[,] squares);
        void HasMoved(bool moved);
    }
}

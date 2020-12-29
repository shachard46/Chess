using System;
using FinalProject.Pieces;

namespace FinalProject
{
    public interface CastledPiece
    {
        int CanCastle(BoardGame bigCasle, BoardGame smallCastle);
        int CanCastle();
    }
}

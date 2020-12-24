using System;
using Android.Graphics;

namespace FinalProject
{
    public abstract class ChessPiece : Piece
    {
        //k q p r n b 
        public ChessPiece(int[] cords, Color color, bool side, bool eaten, char type) : base(cords[0], cords[1], color, side, eaten)
        {
            Type = type;
        }

        public char Type { get; set; }

        public abstract BoardSquare[,] GetPossiblePlaces(BoardSquare[,] squares, ChessPiece[] you, ChessPiece[] enemy);
    }
}

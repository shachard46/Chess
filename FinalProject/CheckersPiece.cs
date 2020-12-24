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
      
            //int[] cords = new int[2];
            //BoardSquare[,] possibllities = new BoardSquare[8, 8];
            //for (int i = 0; i < 8; i++) {
            //    for (int j = 0; j < 8; j++)
            //    {
            //        if (squares[i, j].IsInArea((int)GetX(), (int)GetY()))
            //        {
            //            cords[0] = i;
            //            cords[j] = i;
            //            break;
            //        }
            //    }
            //}
            //switch(Type)
            //{
            //    case 'k':
            //        for (int i = cords[1] - 1; i <= cords[1] + 1; i++)
            //        {
            //            for (int j = cords[0] - 1; j <= cords[0] + 1; j++)
            //            {
            //                if(squares[i, j].Taken && (squares[i, j].Side == Side))
            //                {
            //                    possibllities[i, j] = squares[i, j];
            //                }
            //            }
            //        }
            //        break;
            //    case 'q':
            //        for (int i = 8; i <= 0; i--)
            //        {
            //            for (int j = -1; j <= 1; j++)
            //            {
            //            }
            //        }
            //                break;
            //    case 'p':
            //        break;
            //    case 'r':
            //        break;
            //    case 'n':
            //        break;
            //    case 'b':
            //        break;
            //}
            //return possibllities;
    }
}

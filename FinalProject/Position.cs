using Google.Cloud.Firestore;
using System;
namespace FinalProject
{
    [FirestoreData]
    public class Position
    {
        [FirestoreProperty]
        public string Piece { set; get; }

        [FirestoreProperty]
        public int X { set; get; }

        [FirestoreProperty]
        public int Y { set; get; }

        public Position(string piece, int x, int y)
        {
            Piece = piece;
            X = x;
            Y = y;
        }
    }
}

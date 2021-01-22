using Google.Cloud.Firestore;
using System;
namespace FinalProject
{
    [FirestoreData]
    public class Place
    {
        [FirestoreProperty]
        public string Piece { set; get; }

        [FirestoreProperty]
        public string X { set; get; }

        [FirestoreProperty]
        public string Y { set; get; }

        public Place(string piece, string x, string y)
        {
            Piece = piece;
            X = x;
            Y = y;
        }
    }
}

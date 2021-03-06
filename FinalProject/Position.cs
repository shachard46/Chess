using Firebase;
using Java.Util;
using System;
namespace FinalProject
{
    public class Position
    {
        public string Piece { set; get; }

        public int X { set; get; }

        public int Y { set; get; }

        public Position(string piece, int x, int y)
        {
            Piece = piece;
            X = x;
            Y = y;
        }

        public HashMap ToMap()
        {
            HashMap map = new HashMap();
            map.Put("piece", Piece);
            map.Put("x", X);
            map.Put("y", Y);
            return map;
        }
    }
}

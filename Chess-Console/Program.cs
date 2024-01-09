using System.Collections.Generic;
using System;
using System.Globalization;
using ClassOfParts;
using Board;

namespace Chess 
{
    class progam 
    {
        static void Main(string[] args) 
        {
            PositionChess pos = new PositionChess('c', 7);
            Console.WriteLine(pos);
            Console.WriteLine(pos.ToPosition());
        }
    }
}

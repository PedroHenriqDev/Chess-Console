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
            try
            {
                ChessBoard board = new ChessBoard(8, 8);
                board.putPart(new Castle(board, Color.Black), new Position(1, 2));
                board.putPart(new Castle(board, Color.Black), new Position(0, 3));
                board.putPart(new King(board, Color.Black), new Position(1, 3));
                board.putPart(new Castle(board, Color.White), new Position(3, 5));
                Screen.PrintChessBoard(board);
            }
            catch (ChessExcepetion e) 
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}

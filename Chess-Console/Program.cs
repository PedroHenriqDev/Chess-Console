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
                board.putPart(new Castle(board, Color.Black), new Position(0, 0));
                board.putPart(new Castle(board, Color.Black), new Position(0, 0));
                board.putPart(new King(board, Color.Black), new Position(0, 0));
                Screen.PrintChessBoard(board);
            }
            catch (ChessExcepetion e) 
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}

using System.Collections.Generic;
using System;
using System.Globalization;
using MechanicChess;
using Board;

namespace Chess_Console 
{
    class Progam
    {
        static void Main(string[] args) 
        {
            try
            {
                MatchOfChess match = new MatchOfChess();

                while (!match.Termined) 
                {
                    Console.Clear();
                    Screen.PrintChessBoard(match.Board);
                    Console.WriteLine();
                    Console.Write("Origin: ");
                    Position origin = Screen.ReadPositionChess().ToPosition();
                    Console.Write("Destiny");
                    Position destiny = Screen.ReadPositionChess().ToPosition();
                    match.PerformMoviment(origin, destiny);
                }
            }
            catch (ChessExcepetion e) 
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}

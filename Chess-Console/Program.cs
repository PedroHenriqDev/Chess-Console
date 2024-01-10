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
                    Screen.PrintChessBoard(match.ChessBoard);
                    Console.WriteLine();

                    Console.Write("Origin: ");
                    Position origin = Screen.ReadPositionChess().ToPosition();

                    bool[,] possiblePositions = match.ChessBoard.ReturnPiece(origin).PossibleMovements();

                    Console.Clear();
                    Screen.PrintChessBoard(match.ChessBoard, possiblePositions);
                    
                    Console.WriteLine();
                    Console.Write("Destiny: ");
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

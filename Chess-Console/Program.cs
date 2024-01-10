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
                    try
                    {
                        Console.Clear();
                        Screen.PrintMatch(match);
                        Console.Write("\nOrigin: ");
                        Position origin = Screen.ReadPositionChess().ToPosition();
                        match.ValidedPositionOfOrigin(origin);

                        bool[,] possiblePositions = match.ChessBoard.ReturnPiece(origin).PossibleMovements();

                        Console.Clear();
                        Screen.PrintChessBoard(match.ChessBoard, possiblePositions);

                        Console.WriteLine();
                        Console.Write("\nDestiny: ");
                        Position destiny = Screen.ReadPositionChess().ToPosition();
                        match.ValidedPostitionOfDestiny(origin, destiny);
                        match.MakePlay(origin, destiny);
                    }
                    catch (ChessExcepetion e)
                    {
                        Console.WriteLine(e.Message);
                        Console.ReadLine();
                    }
                }
                Console.Clear();
                Screen.PrintMatch(match);
                Console.ReadLine();
            }
            catch (ChessExcepetion e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}

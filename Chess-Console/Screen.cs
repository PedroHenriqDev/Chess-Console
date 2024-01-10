using Board;
using System;
using System.Collections.Generic;
using System.Globalization;
using MechanicChess;
using System.Security.Cryptography.X509Certificates;

namespace Chess_Console
{
    class Screen
    {
        public static void PrintMatch(MatchOfChess match)
        {
            Screen.PrintChessBoard(match.ChessBoard);
            Console.WriteLine();
            PrintPieceCaptured(match);
            Console.WriteLine("\nRound: " + match.Round);
            Console.WriteLine("Waiting for move: " + match.PlayerCurrent);
        }

        public static void PrintPieceCaptured(MatchOfChess match) 
        {
            Console.WriteLine("Captured piece");
            Console.Write("Whites: ");
            PrintSet(match.PieceCaptured(Color.White));
            Console.Write("\nBlacks: ");
            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            PrintSet(match.PieceCaptured(Color.Black));
            Console.ForegroundColor = aux;
            Console.WriteLine();
        }

        public static void PrintSet(HashSet<Piece> set) 
        {
            Console.Write("[");
            foreach(Piece x in set)
            {
                Console.Write(x + ", ");
            }
            Console.Write("]");
        }
        public static void PrintChessBoard(ChessBoard chessBoard)
        {
            for (int i = 0; i < chessBoard.Lines; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < chessBoard.Columns; j++)
                {
                    PrintPiece(chessBoard.ReturnPiece(i, j));
                }
                Console.WriteLine();
            }
            Console.WriteLine("  A B C D E F G H");
        }

        public static void PrintChessBoard(ChessBoard chessBoard, bool[,] possiblePosition)
        {
            ConsoleColor backgroundOriginal = Console.BackgroundColor;
            ConsoleColor backgroundChanged = ConsoleColor.DarkGray;

            for (int i = 0; i < chessBoard.Lines; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < chessBoard.Columns; j++)
                {
                    if (possiblePosition[i, j])
                    {
                        Console.BackgroundColor = backgroundChanged;
                    }
                    else
                    {
                        Console.BackgroundColor = backgroundOriginal;
                    }
                    PrintPiece(chessBoard.ReturnPiece(i, j));
                    Console.BackgroundColor = backgroundOriginal;
                }
                Console.WriteLine();
            }
            Console.WriteLine("  A B C D E F G H");
            Console.BackgroundColor = backgroundOriginal;
        }

        public static PositionChess ReadPositionChess()
        {
            string s = Console.ReadLine();
            char column = s[0];
            int line = int.Parse(s[1] + "");
            return new PositionChess(column, line);
        }

        public static void PrintPiece(Piece piece)
        {
            if (piece == null)
            {
                Console.Write("- ");
            }
            else
            {

                if (piece.ColorPart == Color.White)
                {
                    Console.Write(piece);
                }
                else
                {
                    ConsoleColor aux = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(piece);
                    Console.ForegroundColor = aux;
                }
                Console.Write(" ");
            }
        }
    }
}

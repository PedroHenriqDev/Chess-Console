using System;
using Board;
using Chess_Console;

namespace MechanicChess
{
    class MatchOfChess
    {
        public ChessBoard Board { get; private set; }
        private int Turn;
        private Color playerCurrent;
        public bool Termined { get; private set; }

        public MatchOfChess()
        {
            Board = new ChessBoard(8, 8);
            Turn = 1;
            playerCurrent = Color.White;
            Termined = false;
            PlacePiece();
        }

        private void PlacePiece() 
        {
            Board.putPart(new Castle(Board, Color.White), new PositionChess('c', 2).ToPosition());
            Board.putPart(new Castle(Board, Color.White), new PositionChess('c', 1).ToPosition());
            Board.putPart(new Castle(Board, Color.White), new PositionChess('d', 2).ToPosition());
            Board.putPart(new Castle(Board, Color.White), new PositionChess('e', 2).ToPosition());
            Board.putPart(new Castle(Board, Color.White), new PositionChess('e', 1).ToPosition());
            Board.putPart(new King(Board, Color.White), new PositionChess('d', 1).ToPosition());

            Board.putPart(new Castle(Board, Color.Black), new PositionChess('c', 7).ToPosition());
            Board.putPart(new Castle(Board, Color.Black), new PositionChess('c', 8).ToPosition());
            Board.putPart(new Castle(Board, Color.Black), new PositionChess('d', 7).ToPosition());
            Board.putPart(new Castle(Board, Color.Black), new PositionChess('e', 7).ToPosition());
            Board.putPart(new Castle(Board, Color.Black), new PositionChess('e', 8).ToPosition());
            Board.putPart(new King(Board, Color.Black), new PositionChess('d', 8).ToPosition());


        }

        public void PerformMoviment(Position origin, Position destiny) 
        {
            Piece p = Board.removePart(origin);
            p.IncreaseAmountMovement();
            Piece capturedPart = Board.removePart(destiny);
            Board.putPart(p, destiny);
        }
    }
}

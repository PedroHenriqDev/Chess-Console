using System;
using Board;
using Chess_Console;

namespace MechanicChess
{
    class MatchOfChess
    {
        public ChessBoard ChessBoard { get; private set; }
        private int Turn;
        private Color PlayerCurrent;
        public bool Termined { get; private set; }

        public MatchOfChess()
        {
            ChessBoard = new ChessBoard(8, 8);
            Turn = 1;
            PlayerCurrent = Color.White;
            Termined = false;
            PlacePiece();
        }

        private void PlacePiece() 
        {
            ChessBoard.putPart(new Castle(ChessBoard, Color.White), new PositionChess('c', 2).ToPosition());
            ChessBoard.putPart(new Castle(ChessBoard, Color.White), new PositionChess('c', 1).ToPosition());
            ChessBoard.putPart(new Castle(ChessBoard, Color.White), new PositionChess('d', 2).ToPosition());
            ChessBoard.putPart(new Castle(ChessBoard, Color.White), new PositionChess('e', 2).ToPosition());
            ChessBoard.putPart(new Castle(ChessBoard, Color.White), new PositionChess('e', 1).ToPosition());
            ChessBoard.putPart(new King(ChessBoard, Color.White), new PositionChess('d', 1).ToPosition());

            ChessBoard.putPart(new Castle(ChessBoard, Color.Black), new PositionChess('c', 7).ToPosition());
            ChessBoard.putPart(new Castle(ChessBoard, Color.Black), new PositionChess('c', 8).ToPosition());
            ChessBoard.putPart(new Castle(ChessBoard, Color.Black), new PositionChess('d', 7).ToPosition());
            ChessBoard.putPart(new Castle(ChessBoard, Color.Black), new PositionChess('e', 7).ToPosition());
            ChessBoard.putPart(new Castle(ChessBoard, Color.Black), new PositionChess('e', 8).ToPosition());
            ChessBoard.putPart(new King(ChessBoard, Color.Black), new PositionChess('d', 8).ToPosition());


        }

        public void PerformMoviment(Position origin, Position destiny) 
        {
            Piece p = ChessBoard.removePart(origin);
             p.IncreaseAmountMovement();
            Piece capturedPart = ChessBoard.removePart(destiny);
            ChessBoard.putPart(p, destiny);
        }
    }
}

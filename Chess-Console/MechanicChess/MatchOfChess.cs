using System;
using Board;
using Chess_Console;

namespace MechanicChess
{
    class MatchOfChess
    {
        public ChessBoard ChessBoard { get; private set; }
        public int Round { get; private set; }
        public Color PlayerCurrent { get; private set; }
        public bool Termined { get; private set; }

        public MatchOfChess()
        {
            ChessBoard = new ChessBoard(8, 8);
            Round = 1;
            PlayerCurrent = Color.White;
            Termined = false;
            PlacePiece();
        }

        private void PlacePiece() 
        {
            ChessBoard.putPiece(new Castle(ChessBoard, Color.White), new PositionChess('c', 2).ToPosition());
            ChessBoard.putPiece(new Castle(ChessBoard, Color.White), new PositionChess('c', 1).ToPosition());
            ChessBoard.putPiece(new Castle(ChessBoard, Color.White), new PositionChess('d', 2).ToPosition());
            ChessBoard.putPiece(new Castle(ChessBoard, Color.White), new PositionChess('e', 2).ToPosition());
            ChessBoard.putPiece(new Castle(ChessBoard, Color.White), new PositionChess('e', 1).ToPosition());
            ChessBoard.putPiece(new King(ChessBoard, Color.White), new PositionChess('d', 1).ToPosition());

            ChessBoard.putPiece(new Castle(ChessBoard, Color.Black), new PositionChess('c', 7).ToPosition());
            ChessBoard.putPiece(new Castle(ChessBoard, Color.Black), new PositionChess('c', 8).ToPosition());
            ChessBoard.putPiece(new Castle(ChessBoard, Color.Black), new PositionChess('d', 7).ToPosition());
            ChessBoard.putPiece(new Castle(ChessBoard, Color.Black), new PositionChess('e', 7).ToPosition());
            ChessBoard.putPiece(new Castle(ChessBoard, Color.Black), new PositionChess('e', 8).ToPosition());
            ChessBoard.putPiece(new King(ChessBoard, Color.Black), new PositionChess('d', 8).ToPosition());


        }

        public void PerformMoviment(Position origin, Position destiny) 
        {
            Piece p = ChessBoard.removePiece(origin);
            p.IncreaseAmountMovement();
            Piece capturedPart = ChessBoard.removePiece(destiny);
            ChessBoard.putPiece(p, destiny);
        }

        public void MakePlay(Position origin, Position destiny) 
        {
            PerformMoviment(origin, destiny);
            Round++;
            ChangePlayer();
        }

        public void ValidedPositionOfOrigin(Position pos) 
        {
            if(ChessBoard.ReturnPiece(pos) == null) 
            {
                throw new ChessExcepetion("There is no piece in the chosen origin position!");
            }
            if(PlayerCurrent != ChessBoard.ReturnPiece(pos).ColorPart) 
            {
                throw new ChessExcepetion("The origin piece chosen is not yours!");
            }
            if (!ChessBoard.ReturnPiece(pos).ExistPossibleMovements()) 
            {
                throw new ChessExcepetion("There are no movements possible for the origin piece!");
            }
        }

        public void ValidedPostitionOfDestiny(Position origin, Position destiny) 
        {   
            if (!ChessBoard.ReturnPiece(origin).CanMoveTo(destiny)) 
            {
                throw new ChessExcepetion("invalid destination position");
            }
        }

        private void ChangePlayer() 
        {
            if(PlayerCurrent == Color.White)
            {
                PlayerCurrent = Color.Black;
            }
            else
            {
                PlayerCurrent = Color.White;
            }
        }
    }
}

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
        private HashSet<Piece> _pieces;
        private HashSet<Piece> _captured;

        public MatchOfChess()
        {
            ChessBoard = new ChessBoard(8, 8);
            Round = 1;
            PlayerCurrent = Color.White;
            Termined = false;
            _pieces = new HashSet<Piece>();
            _captured = new HashSet<Piece>();
            PlacePiece();
        }

        public HashSet<Piece> PieceCaptured(Color color) 
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach(Piece x in _captured) 
            {
                if(x.ColorPart == color) 
                {
                    aux.Add(x);
                }
            }
            return aux;
        }

        public HashSet<Piece> PieceInGame(Color color) 
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (Piece x in _captured)
            {
                if (x.ColorPart == color)
                {
                    aux.Add(x);
                }
            }
            aux.ExceptWith(PieceCaptured(color));
            return aux;
        }


        public void PutNewPiece(char column, int line, Piece piece) 
        {
            ChessBoard.putPiece(piece, new PositionChess(column, line).ToPosition());
            _pieces.Add(piece);
        }

        private void PlacePiece() 
        {
            PutNewPiece('c', 1, new Castle(ChessBoard, Color.White));
            PutNewPiece('c', 2, new Castle(ChessBoard, Color.White));
            PutNewPiece('d', 2, new Castle(ChessBoard, Color.White));
            PutNewPiece('e', 2, new Castle(ChessBoard, Color.White));
            PutNewPiece('e', 1, new Castle(ChessBoard, Color.White));
            PutNewPiece('d', 1, new King(ChessBoard, Color.White));

            PutNewPiece('c', 7, new Castle(ChessBoard, Color.Black));
            PutNewPiece('c', 8, new Castle(ChessBoard, Color.Black));
            PutNewPiece('d', 7, new Castle(ChessBoard, Color.Black));
            PutNewPiece('e', 7, new Castle(ChessBoard, Color.Black));
            PutNewPiece('e', 8, new Castle(ChessBoard, Color.Black));
            PutNewPiece('d', 8, new King(ChessBoard, Color.Black));
        }

        public void PerformMoviment(Position origin, Position destiny) 
        {
            Piece p = ChessBoard.removePiece(origin);
            p.IncreaseAmountMovement();
            Piece capturedPiece = ChessBoard.removePiece(destiny);
            ChessBoard.putPiece(p, destiny);
            if(capturedPiece != null) 
            {
                _captured.Add(capturedPiece);
            }
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
                throw new ChessExcepetion("Invalid destination position!");
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

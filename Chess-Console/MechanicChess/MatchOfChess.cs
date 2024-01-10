using System;
using System.Security.Cryptography.X509Certificates;
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
        public bool Check {  get; private set; }
        private HashSet<Piece> _pieces;
        private HashSet<Piece> _captured;

        public MatchOfChess()
        {
            ChessBoard = new ChessBoard(8, 8);
            Round = 1;
            PlayerCurrent = Color.White;
            Termined = false;
            Check = false;
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
            foreach (Piece x in _pieces)
            {
                if (x.ColorPart == color)
                {
                    aux.Add(x);
                }
            }
            aux.ExceptWith(PieceCaptured(color));
            return aux;
        }

        private Color Adversary(Color color) 
        {
            if(color == Color.White)
            {
                return Color.Black;
            }
            else
            {
                return Color.White;
            }
        }

        private Piece SearchKing(Color color) 
        {
            foreach(Piece x in PieceInGame(color)) 
            {
                if(x is King) 
                {
                    return x;
                }
            }
            return null;
        }

        public bool IsInCheck(Color color) 
        {
            Piece king = SearchKing(color);
            if(king == null) 
            {
                throw new ChessExcepetion("There is no color "+  color   + " king on the board!");
            }
            foreach(Piece x in PieceInGame(Adversary(color)))
            {
                bool[,] mat = x.PossibleMovements();
                if (mat[king.Position.Line, king.Position.Column])
                {
                    return true;
                }
            }
            return false;
        }

        public bool TestCheckMate(Color color) 
        {
            if(!IsInCheck(color)) 
            {
                return false;
            }
            foreach(Piece x in PieceInGame(color)) 
            {
                bool[,] mat = x.PossibleMovements();
                for(int i = 0; i < ChessBoard.Lines; i++) 
                {
                    for(int j = 0; j < ChessBoard.Columns; j++) 
                    {
                        if (mat[i, j])
                        {
                            Position origin = x.Position;
                            Position destiny = new Position(i, j);
                            Piece pieceCaptured = PerformMoviment(origin, destiny);
                            bool testCheck = IsInCheck(color);
                            RemoveTheMovement(origin, destiny, pieceCaptured);
                            if (!testCheck)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        public void PutNewPiece(char column, int line, Piece piece) 
        {
            ChessBoard.PutPiece(piece, new PositionChess(column, line).ToPosition());
            _pieces.Add(piece);
        }

        private void PlacePiece() 
        {
            PutNewPiece('c', 1, new Castle(ChessBoard, Color.White));
            PutNewPiece('d', 1, new King(ChessBoard, Color.White));
            PutNewPiece('h', 7, new Castle(ChessBoard, Color.White));
  
            PutNewPiece('a', 8, new King(ChessBoard, Color.Black));
            PutNewPiece('b', 8, new Castle(ChessBoard, Color.Black));
        }

        public Piece PerformMoviment(Position origin, Position destiny) 
        {
            Piece p = ChessBoard.RemovePiece(origin);
            p.IncreaseAmountMovement();
            Piece capturedPiece = ChessBoard.RemovePiece(destiny);
            ChessBoard.PutPiece(p, destiny);
            if(capturedPiece != null) 
            {
                _captured.Add(capturedPiece);
            }
            return capturedPiece;
        }

        public void RemoveTheMovement(Position origin, Position destiny, Piece pieceCaptured) 
        {
            Piece piece = ChessBoard.RemovePiece(destiny);
            piece.DecreaseAmountMovement();
            if(pieceCaptured != null) 
            {
                ChessBoard.PutPiece(pieceCaptured, destiny);
                _captured.Remove(pieceCaptured);
            }
            ChessBoard.PutPiece(piece, origin); 
        }

        public void MakePlay(Position origin, Position destiny)
        {
            Piece pieceCaptured = PerformMoviment(origin, destiny);

            if (IsInCheck(PlayerCurrent))
            {
                RemoveTheMovement(origin, destiny, pieceCaptured);
                throw new ChessExcepetion("You can't put it in CHECK!");
            }
            if (IsInCheck(Adversary(PlayerCurrent)))
            {
                Check = true;
            }
            else
            {
                Check = false;
            }

            if (TestCheckMate(Adversary(PlayerCurrent)))
            {
                Termined = true;
            }
            else
            {
                Round++;
                ChangePlayer();
            }
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
            if (!ChessBoard.ReturnPiece(origin).MovementPossible(destiny)) 
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

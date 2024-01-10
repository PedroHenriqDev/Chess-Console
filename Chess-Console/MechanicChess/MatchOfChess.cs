using System;
using System.Net.Http.Headers;
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
        public bool Check { get; private set; }
        public Piece VulnerableEnPassant;
        private HashSet<Piece> _pieces;
        private HashSet<Piece> _captured;

        public MatchOfChess()
        {
            ChessBoard = new ChessBoard(8, 8);
            Round = 1;
            PlayerCurrent = Color.White;
            Termined = false;
            Check = false;
            VulnerableEnPassant = null;
            _pieces = new HashSet<Piece>();
            _captured = new HashSet<Piece>();
            PlacePiece();
        }

        public HashSet<Piece> PieceCaptured(Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (Piece x in _captured)
            {
                if (x.ColorPart == color)
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
            if (color == Color.White)
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
            foreach (Piece x in PieceInGame(color))
            {
                if (x is King)
                {
                    return x;
                }
            }
            return null;
        }

        public bool IsInCheck(Color color)
        {
            Piece king = SearchKing(color);
            if (king == null)
            {
                throw new ChessExcepetion("There is no color " + color + " king on the board!");
            }
            foreach (Piece x in PieceInGame(Adversary(color)))
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
            if (!IsInCheck(color))
            {
                return false;
            }
            foreach (Piece x in PieceInGame(color))
            {
                bool[,] mat = x.PossibleMovements();
                for (int i = 0; i < ChessBoard.Lines; i++)
                {
                    for (int j = 0; j < ChessBoard.Columns; j++)
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
            PutNewPiece('a', 1, new Castle(ChessBoard, Color.White));
            PutNewPiece('b', 1, new Horse(ChessBoard, Color.White));
            PutNewPiece('c', 1, new Bishop(ChessBoard, Color.White));
            PutNewPiece('d', 1, new Queen(ChessBoard, Color.White));
            PutNewPiece('e', 1, new King(ChessBoard, Color.White, this));
            PutNewPiece('f', 1, new Bishop(ChessBoard, Color.White));
            PutNewPiece('g', 1, new Horse(ChessBoard, Color.White));
            PutNewPiece('h', 1, new Castle(ChessBoard, Color.White));
            PutNewPiece('a', 2, new Pawn(ChessBoard, Color.White, this));
            PutNewPiece('b', 2, new Pawn(ChessBoard, Color.White, this));
            PutNewPiece('c', 2, new Pawn(ChessBoard, Color.White, this));
            PutNewPiece('d', 2, new Pawn(ChessBoard, Color.White, this));
            PutNewPiece('e', 2, new Pawn(ChessBoard, Color.White, this));
            PutNewPiece('f', 2, new Pawn(ChessBoard, Color.White, this));
            PutNewPiece('g', 2, new Pawn(ChessBoard, Color.White, this));
            PutNewPiece('h', 2, new Pawn(ChessBoard, Color.White, this));

            PutNewPiece('a', 8, new Castle(ChessBoard, Color.Black));
            PutNewPiece('b', 8, new Horse(ChessBoard, Color.Black));
            PutNewPiece('c', 8, new Bishop(ChessBoard, Color.Black));
            PutNewPiece('d', 8, new Queen(ChessBoard, Color.Black));
            PutNewPiece('e', 8, new King(ChessBoard, Color.Black, this));
            PutNewPiece('f', 8, new Bishop(ChessBoard, Color.Black));
            PutNewPiece('g', 8, new Horse(ChessBoard, Color.Black));
            PutNewPiece('h', 8, new Castle(ChessBoard, Color.Black));
            PutNewPiece('a', 7, new Pawn(ChessBoard, Color.Black, this));
            PutNewPiece('b', 7, new Pawn(ChessBoard, Color.Black, this));
            PutNewPiece('c', 7, new Pawn(ChessBoard, Color.Black, this));
            PutNewPiece('d', 7, new Pawn(ChessBoard, Color.Black, this));
            PutNewPiece('e', 7, new Pawn(ChessBoard, Color.Black, this));
            PutNewPiece('f', 7, new Pawn(ChessBoard, Color.Black, this));
            PutNewPiece('g', 7, new Pawn(ChessBoard, Color.Black, this));
            PutNewPiece('h', 7, new Pawn(ChessBoard, Color.Black, this));
        }

        public Piece PerformMoviment(Position origin, Position destiny)
        {
            Piece p = ChessBoard.RemovePiece(origin);
            p.IncreaseAmountMovement();
            Piece capturedPiece = ChessBoard.RemovePiece(destiny);
            ChessBoard.PutPiece(p, destiny);
            if (capturedPiece != null)
            {
                _captured.Add(capturedPiece);
            }

            //SPECIALPLAY KINGSIDE CASTLING
            if (p is King && destiny.Column == origin.Column + 2)
            {
                Position originC = new Position(origin.Line, origin.Column + 3);
                Position destinyC = new Position(origin.Line, origin.Column + 1);
                Piece C = ChessBoard.RemovePiece(originC);
                C.IncreaseAmountMovement();
                ChessBoard.PutPiece(C, destinyC);
            }

            //SPECIALPLAY QUEENSIDE CASTLING
            if (p is King && destiny.Column == origin.Column - 2)
            {
                Position originC = new Position(origin.Line, origin.Column - 4);
                Position destinyC = new Position(origin.Line, origin.Column - 1);
                Piece C = ChessBoard.RemovePiece(originC);
                C.IncreaseAmountMovement();
                ChessBoard.PutPiece(C, destinyC);
            }

            //SPECIAYPLAT EN PASSANT
            if (p is Pawn)
            {
                if (origin.Column != destiny.Column && capturedPiece == null)
                {
                    Position posP;
                    if (p.ColorPart == Color.White)
                    {
                        posP = new Position(destiny.Line + 1, destiny.Column);
                    }
                    else
                    {
                        posP = new Position(destiny.Line - 1, destiny.Column);
                    }
                    capturedPiece = ChessBoard.RemovePiece(posP);
                    _captured.Add(capturedPiece);
                }
            }

            return capturedPiece;
        }

        public void RemoveTheMovement(Position origin, Position destiny, Piece pieceCaptured)
        {
            Piece p = ChessBoard.RemovePiece(destiny);
            p.DecreaseAmountMovement();
            if (pieceCaptured != null)
            {
                ChessBoard.PutPiece(pieceCaptured, destiny);
                _captured.Remove(pieceCaptured);
            }
            ChessBoard.PutPiece(p, origin);

            //SPECIALPLAY KINGSIDE CASTLING
            if (p is King && destiny.Column == origin.Column + 2)
            {
                Position originC = new Position(origin.Line, origin.Column + 3);
                Position destinyC = new Position(origin.Line, origin.Column + 1);
                Piece C = ChessBoard.RemovePiece(destinyC);
                C.DecreaseAmountMovement();
                ChessBoard.PutPiece(C, originC);
            }

            //SPECIALPLAY QUEENSIDE CASTLING
            if (p is King && destiny.Column == origin.Column - 2)
            {
                Position originC = new Position(origin.Line, origin.Column - 4);
                Position destinyC = new Position(origin.Line, origin.Column - 1);
                Piece C = ChessBoard.RemovePiece(originC);
                C.IncreaseAmountMovement();
                ChessBoard.PutPiece(C, destinyC);
            }

            //SPECIALPLAY EN PASSANT
            if (p is Pawn)
            {
                if (origin.Column != destiny.Column && pieceCaptured == VulnerableEnPassant)
                {
                    Piece pawn = ChessBoard.RemovePiece(destiny);
                    Position posP;
                    if (p.ColorPart == Color.White)
                    {
                        posP = new Position(3, destiny.Column);
                    }
                    else
                    {
                        posP = new Position(4, destiny.Column);
                    }
                    ChessBoard.PutPiece(pawn, posP);
                }
            }
        }

        public void MakePlay(Position origin, Position destiny)
        {
            Piece pieceCaptured = PerformMoviment(origin, destiny);
            Piece p = ChessBoard.ReturnPiece(destiny);

            if (IsInCheck(PlayerCurrent))
            {
                RemoveTheMovement(origin, destiny, pieceCaptured);
                throw new ChessExcepetion("You can't put it in CHECK!");
            }

            //SPECIALPLAY PROMOTION
            if (p is Pawn)
            {
                if (p.ColorPart == Color.White && destiny.Line == 0 || (p.ColorPart == Color.Black && destiny.Line == 7))
                {
                    p = ChessBoard.RemovePiece(destiny);
                    _pieces.Remove(p);
                    Piece quenn = new Queen(ChessBoard, p.ColorPart);
                    ChessBoard.PutPiece(quenn, destiny);
                    _pieces.Add(quenn);
                }
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

            //#SPECIALPLAY EN PASSANT
            if (p is Pawn && (destiny.Line == origin.Line - 2 || destiny.Line == origin.Line + 2))
            {
                VulnerableEnPassant = p;
            }
            else
            {
                VulnerableEnPassant = null;
            }
        }

        public void ValidedPositionOfOrigin(Position pos)
        {
            if (ChessBoard.ReturnPiece(pos) == null)
            {
                throw new ChessExcepetion("There is no piece in the chosen origin position!");
            }
            if (PlayerCurrent != ChessBoard.ReturnPiece(pos).ColorPart)
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
            if (PlayerCurrent == Color.White)
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

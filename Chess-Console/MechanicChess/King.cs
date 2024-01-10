using Board;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace MechanicChess
{

    class King : Piece
    {
        private MatchOfChess _match;

        public King(ChessBoard chessBoard, Color color, MatchOfChess match) : base(chessBoard, color)
        {
            _match = match;
        }

        public override string ToString()
        {
            return "K";
        }

        private bool CanMove(Position pos) 
        {
            Piece p = ChessBoard.ReturnPiece(pos);
            return p == null || p.ColorPart != ColorPart;
        }

        private bool TestCastleToCastling(Position pos) 
        {
            Piece piece = ChessBoard.ReturnPiece(pos);
            return piece != null && piece is Castle && piece.ColorPart == ColorPart && piece.AmountMovement == 0;
        }

        public override bool[,] PossibleMovements()
        {
            bool[,] mat = new bool[ChessBoard.Lines, ChessBoard.Columns];

            Position pos = new Position(0, 0);
            //ABOVE
            pos.SetValues(Position.Line - 1, Position.Column);
            if(ChessBoard.PositionValid(pos) && CanMove(pos)) 
            {
                mat[pos.Line, pos.Column] = true;
            }

            //NORTHEAST
            pos.SetValues(Position.Line - 1, Position.Column + 1);
            if (ChessBoard.PositionValid(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }

            //RIGHT
            pos.SetValues(Position.Line, Position.Column + 1);
            if (ChessBoard.PositionValid(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }

            //SOUTHEAST
            pos.SetValues(Position.Line + 1, Position.Column + 1);
            if (ChessBoard.PositionValid(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }

            //BELOW
            pos.SetValues(Position.Line + 1, Position.Column );
            if (ChessBoard.PositionValid(pos)  && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }

            //SOUTH-WEST
            pos.SetValues(Position.Line + 1, Position.Column - 1);
            if (ChessBoard.PositionValid(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }

            //LEFT
            pos.SetValues(Position.Line, Position.Column - 1);
            if (ChessBoard.PositionValid(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }

            //NORTHWEST
            pos.SetValues(Position.Line - 1, Position.Column - 1);
            if (ChessBoard.PositionValid(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }

             //#SPECIALPLAY CASTLING
            if(AmountMovement == 0 && !_match.Check) 
            {
                //#SPECIALPLAY KINGSIDE CASTLING
                Position posC1 = new Position(Position.Line, Position.Column + 3);
                if(TestCastleToCastling(posC1))
                {
                    Position p1 = new Position(Position.Line, Position.Column + 1);
                    Position p2 = new Position(Position.Line, Position.Column + 2);
                    if(ChessBoard.ReturnPiece(p1) == null && ChessBoard.ReturnPiece(p2) == null) 
                    {
                        mat[Position.Line, Position.Column + 2] = true;
                    }
                }

                //#SPECIALPLAY QUEENSIDE CASTLING
                Position posC2 = new Position(Position.Line, Position.Column - 4);
                if (TestCastleToCastling(posC2))
                {
                    Position p1 = new Position(Position.Line, Position.Column - 1);
                    Position p2 = new Position(Position.Line, Position.Column - 2);
                    Position p3 = new Position(Position.Line, Position.Column - 3);
                    if (ChessBoard.ReturnPiece(p1) == null && ChessBoard.ReturnPiece(p2) == null && ChessBoard.ReturnPiece(p3) == null)
                    {
                        mat[Position.Line, Position.Column - 2] = true;
                    }
                }

            }

            return mat;
        }
    }
}

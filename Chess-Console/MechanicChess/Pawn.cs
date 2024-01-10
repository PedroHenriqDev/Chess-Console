using Board;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MechanicChess
{
    class Pawn : Piece
    {
        private MatchOfChess _match;

        public Pawn(ChessBoard chessBoard, Color color, MatchOfChess match) : base(chessBoard, color)
        {
            _match = match;
        }

        public override string ToString()
        {
            return "P";
        }

        private bool ExistEnimie(Position pos)
        {
            Piece piece = ChessBoard.ReturnPiece(pos);
            return piece != null && piece.ColorPart != ColorPart;
        }

        private bool Free(Position pos)
        {
            return ChessBoard.ReturnPiece(pos) == null;
        }

        public override bool[,] PossibleMovements()
        {
            bool[,] mat = new bool[ChessBoard.Lines, ChessBoard.Columns];

            Position pos = new Position(0, 0);

            if (ColorPart == Color.White)
            {
                pos.SetValues(Position.Line - 1, Position.Column);
                if (ChessBoard.PositionValid(pos) && Free(pos))
                {
                    mat[pos.Line, pos.Column] = true;
                }

                pos.SetValues(Position.Line - 2, Position.Column);
                if (ChessBoard.PositionValid(pos) && Free(pos) && AmountMovement == 0)
                {
                    mat[pos.Line, pos.Column] = true;
                }

                pos.SetValues(Position.Line - 1, Position.Column - 1);
                if (ChessBoard.PositionValid(pos) && ExistEnimie(pos))
                {
                    mat[pos.Line, pos.Column] = true;
                }

                pos.SetValues(Position.Line - 1, Position.Column + 1);
                if (ChessBoard.PositionValid(pos) && ExistEnimie(pos))
                {
                    mat[pos.Line, pos.Column] = true;
                }

                //#SPECIALPLAY EN PASSANT
                if (Position.Line == 3)
                {
                    Position left = new Position(Position.Line, Position.Column - 1);
                    if (ChessBoard.PositionValid(left) && ExistEnimie(left) && ChessBoard.ReturnPiece(left) == _match.VulnerableEnPassant)
                    {
                        mat[left.Line - 1, left.Column] = true;
                    }
                    Position right = new Position(Position.Line, Position.Column + 1);
                    if (ChessBoard.PositionValid(right) && ExistEnimie(right) && ChessBoard.ReturnPiece(right) == _match.VulnerableEnPassant)
                    {
                        mat[right.Line - 1, right.Column] = true;
                    }
                }
            }
            else
            {
                pos.SetValues(Position.Line + 1, Position.Column);
                if (ChessBoard.PositionValid(pos) && Free(pos))
                {
                    mat[pos.Line, pos.Column] = true;
                }

                pos.SetValues(Position.Line + 2, Position.Column);
                if (ChessBoard.PositionValid(pos) && Free(pos) && AmountMovement == 0)
                {
                    mat[pos.Line, pos.Column] = true;
                }

                pos.SetValues(Position.Line + 1, Position.Column - 1);
                if (ChessBoard.PositionValid(pos) && ExistEnimie(pos))
                {
                    mat[pos.Line, pos.Column] = true;
                }

                pos.SetValues(Position.Line + 1, Position.Column + 1);
                if (ChessBoard.PositionValid(pos) && ExistEnimie(pos))
                {
                    mat[pos.Line, pos.Column] = true;
                }

                //#SPECIALPLAY EN PASSANT
                if (Position.Line == 4)
                {
                    Position left = new Position(Position.Line, Position.Column - 1);
                    if (ChessBoard.PositionValid(left) && ExistEnimie(left) && ChessBoard.ReturnPiece(left) == _match.VulnerableEnPassant)
                    {
                        mat[left.Line + 1, left.Column] = true;
                    }
                    Position right = new Position(Position.Line, Position.Column + 1);
                    if (ChessBoard.PositionValid(right) && ExistEnimie(right) && ChessBoard.ReturnPiece(right) == _match.VulnerableEnPassant)
                    {
                        mat[right.Line + 1, right.Column] = true;
                    }
                }
            }
            return mat;

        }
    }
}

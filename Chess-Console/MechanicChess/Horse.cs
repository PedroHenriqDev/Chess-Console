using Board;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicChess
{
    class Horse : Piece
    {
        public Horse(ChessBoard chessBoard, Color color) : base(chessBoard, color) 
        {
        }

        public override string ToString()
        {
            return "H";
        }

        private bool CanMove(Position pos)
        {
            Piece p = ChessBoard.ReturnPiece(pos);
            return p == null || p.ColorPart != ColorPart;
        }

        public override bool[,] PossibleMovements()
        {
            bool[,] mat = new bool[ChessBoard.Lines, ChessBoard.Columns];

            Position pos = new Position(0, 0);
            
            pos.SetValues(Position.Line - 1, Position.Column - 2);
            if (ChessBoard.PositionValid(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }

            pos.SetValues(Position.Line - 2, Position.Column - 1);
            if (ChessBoard.PositionValid(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }

            pos.SetValues(Position.Line - 2, Position.Column + 1);
            if (ChessBoard.PositionValid(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }

            pos.SetValues(Position.Line - 1, Position.Column + 2);
            if (ChessBoard.PositionValid(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }

            pos.SetValues(Position.Line + 1, Position.Column + 2);
            if (ChessBoard.PositionValid(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }

            pos.SetValues(Position.Line + 2, Position.Column + 1);
            if (ChessBoard.PositionValid(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }

            pos.SetValues(Position.Line + 2, Position.Column - 1);
            if (ChessBoard.PositionValid(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }

            pos.SetValues(Position.Line + 1, Position.Column - 2);
            if (ChessBoard.PositionValid(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }

            return mat;
        }
    }
}

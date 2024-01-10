using Board;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicChess
{

    class King : Piece
    {
        public King(ChessBoard chessBoard, Color color) : base(chessBoard, color)
        {

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

        public override bool[,] PossibleMovements()
        {
            bool[,] mat = new bool[ChessBoard.Lines, ChessBoard.Columns];

            Position pos = new Position(0, 0);
            //ABOVE
            pos.SetValues(Position.Line - 1, Position.Column);
            if(ChessBoard.positionValid(pos) && CanMove(pos)) 
            {
                mat[pos.Line, pos.Column] = true;
            }

            //NORTHWEST
            pos.SetValues(Position.Line - 1, Position.Column + 1);
            if (ChessBoard.positionValid(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }

            //RIGHT
            pos.SetValues(Position.Line, Position.Column + 1);
            if (ChessBoard.positionValid(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }

            //SOUTHEAST
            pos.SetValues(Position.Line + 1, Position.Column + 1);
            if (ChessBoard.positionValid(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }

            //BELOW
            pos.SetValues(Position.Line + 1, Position.Column );
            if (ChessBoard.positionValid(pos)  && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }

            //SOUTH-WEST
            pos.SetValues(Position.Line + 1, Position.Column - 1);
            if (ChessBoard.positionValid(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }

            //LEFT
            pos.SetValues(Position.Line, Position.Column - 1);
            if (ChessBoard.positionValid(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }

            //NORTHWEST
            pos.SetValues(Position.Line - 1, Position.Column - 1);
            if (ChessBoard.positionValid(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }

            return mat;
        }
    }
}

using Board;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicChess
{

    class Castle : Piece
    {
        public Castle(ChessBoard chessBoard, Color color) : base(chessBoard, color)
        {

        }

        public override string ToString()
        {
            return "C";
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
            while(ChessBoard.positionValid(pos) && CanMove(pos)) 
            {
                mat[pos.Line, pos.Column] = true;
                if(ChessBoard.ReturnPiece(pos) != null && ChessBoard.ReturnPiece(pos).ColorPart != ColorPart)
                {
                    break;
                }
                pos.Line = pos.Line - 1;
            }


            //BELOW
            pos.SetValues(Position.Line + 1, Position.Column);
            while (ChessBoard.positionValid(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
                if (ChessBoard.ReturnPiece(pos) != null && ChessBoard.ReturnPiece(pos).ColorPart != ColorPart)
                {
                    break;
                }
                pos.Line = pos.Line + 1;
            }

            //RIGHT
            pos.SetValues(Position.Line, Position.Column + 1);
            while (ChessBoard.positionValid(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
                if (ChessBoard.ReturnPiece(pos) != null && ChessBoard.ReturnPiece(pos).ColorPart != ColorPart)
                {
                    break;
                }
                pos.Column = pos.Column + 1;
            }

            //LEFT
            pos.SetValues(Position.Line, Position.Column - 1);
            while (ChessBoard.positionValid(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
                if (ChessBoard.ReturnPiece(pos) != null && ChessBoard.ReturnPiece(pos).ColorPart != ColorPart)
                {
                    break;
                }
                pos.Column = pos.Column - 1;
            }
            return mat;
        }
    }
}

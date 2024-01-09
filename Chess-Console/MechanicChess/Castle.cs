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
    }
}

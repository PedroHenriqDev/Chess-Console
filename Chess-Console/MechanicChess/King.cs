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
    }
}

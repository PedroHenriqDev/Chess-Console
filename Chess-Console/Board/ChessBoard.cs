using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board
{
    class ChessBoard
    {
        private Part[,] Parts;
        public int Lines { get; set; }
        public int Columns { get; set; }

        public ChessBoard(int lines, int columns)
        {
            Lines = lines;
            Columns = columns;
            Parts = new Part[Lines, Columns];
        }

        public Part Piece(int line, int column)
        {
            return Parts[line, column];
        }

        public void putPart(Part p, Position pos) 
        {
            Parts[pos.Line, pos.Column] = p;
            p.Position = pos;
        }
    }
}
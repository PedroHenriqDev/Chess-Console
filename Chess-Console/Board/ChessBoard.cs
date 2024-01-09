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
        public Part Piece(Position pos)
        {
            return Parts[pos.Line, pos.Column];
        }

        public bool existPart(Position pos) 
        {
            validatePosition(pos);
            return Piece(pos) != null;
        }

        public void putPart(Part p, Position pos)
        {
            if (existPart(pos)) 
            {
                throw new ChessExcepetion("There is a piece in this position!");
            }
            Parts[pos.Line, pos.Column] = p;
            p.Position = pos;
        }

        public bool positionValid(Position pos)
        {
            if (pos.Line < 0 || pos.Line >= Lines || pos.Column < 0 || pos.Column >= Lines)
            {
                return false;
            }
            return true;
        }

        public void validatePosition(Position pos) 
        {
            if (!positionValid(pos)) 
            {
                throw new ChessExcepetion("Position invalid!");
            }
        }
    }
}
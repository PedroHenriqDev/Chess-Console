using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board
{
    class ChessBoard
    {
        private Piece[,] Pieces;
        public int Lines { get; set; }
        public int Columns { get; set; }

        public ChessBoard(int lines, int columns)
        {
            Lines = lines;
            Columns = columns;
            Pieces = new Piece[Lines, Columns];
        }

        public Piece Piece(int line, int column)
        {
            return Pieces[line, column];
        }
        public Piece Piece(Position pos)
        {
            return Pieces[pos.Line, pos.Column];
        }

        public bool existPart(Position pos) 
        {
            validatePosition(pos);
            return Piece(pos) != null;
        }

        public void putPart(Piece p, Position pos)
        {
            if (existPart(pos)) 
            {
                throw new ChessExcepetion("There is a piece in this position!");
            }
            Pieces[pos.Line, pos.Column] = p;
            p.Position = pos;
        }

        public Piece removePart(Position pos)
        {
            if (Piece(pos) == null) 
            {
                return null;
            }
            Piece aux = Piece(pos);
            aux.Position = null;
            Pieces[pos.Line, pos.Column] = null; 
            return aux;
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
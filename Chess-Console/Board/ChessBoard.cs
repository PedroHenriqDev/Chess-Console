using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board
{
    class ChessBoard
    {
        private Piece[,] _pieces;
        public int Lines { get; set; }
        public int Columns { get; set; }

        public ChessBoard(int lines, int columns)
        {
            Lines = lines;
            Columns = columns;
            _pieces = new Piece[Lines, Columns];
        }

        public Piece ReturnPiece(int line, int column)
        {
            return _pieces[line, column];
        }
        public Piece ReturnPiece(Position pos)
        {
            return _pieces[pos.Line, pos.Column];
        }

        public bool existPiece(Position pos) 
        {
            validatePosition(pos);
            return ReturnPiece(pos) != null;
        }

        public void PutPiece(Piece p, Position pos)
        {
            if (existPiece(pos)) 
            {
                throw new ChessExcepetion("There is a piece in this position!");
            }
            _pieces[pos.Line, pos.Column] = p;
            p.Position = pos;
        }

        public Piece RemovePiece(Position pos)
        {
            if (ReturnPiece(pos) == null) 
            {
                return null;
            }
            Piece aux = ReturnPiece(pos);
            aux.Position = null;
            _pieces[pos.Line, pos.Column] = null; 
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
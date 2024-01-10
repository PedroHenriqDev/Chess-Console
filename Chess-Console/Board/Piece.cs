using MechanicChess;
using Chess_Console;

namespace Board
{
    abstract class Piece
    {
        public Position Position { get; set; }
        public Color ColorPart { get; protected set; }
        public int AmountMovement { get; protected set; }
        public ChessBoard ChessBoard { get; protected set; }

        public Piece(ChessBoard chessBoard, Color colorPart)
        {
            this.Position = null;
            this.ColorPart = colorPart;
            this.ChessBoard = chessBoard;
            this.AmountMovement = 0;
        }

        public void IncreaseAmountMovement() 
        {
            AmountMovement++;
        }

        public void DecreaseAmountMovement()
        {
            AmountMovement--;
        }

        public bool ExistPossibleMovements() 
        {
            bool[,] mat = PossibleMovements();
            for(int i = 0; i < ChessBoard.Lines; i++) 
            {
                for(int j = 0; j < ChessBoard.Columns; j++) 
                {
                    if (mat[i, j]) 
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool MovementPossible(Position pos) 
        {
            return PossibleMovements()[pos.Line, pos.Column];
        }

        public abstract bool[,] PossibleMovements();
    }
}
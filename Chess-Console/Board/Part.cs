﻿namespace Board
{
    class Part
    {
        public Position Position { get; set; }
        public Color ColorPart { get; protected set; }
        public int AmountMovement { get; protected set; }
        public ChessBoard ChessBoard { get; protected set; }

        public Part(ChessBoard chessBoard, Color colorPart)
        {
            this.Position = null;
            this.ColorPart = colorPart;
            this.ChessBoard = chessBoard;
            this.AmountMovement = 0;
        }
    }
}
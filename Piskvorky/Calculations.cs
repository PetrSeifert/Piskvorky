using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Piskvorky
{
    class Calculations
    {
        private int boardSize = 19;
        private GamePiece[,] piecesOnBoard;
        private const short rowLenght = 5;
        private short[,,,] piecesInRow;
        private short[,] DirectionSigns;

        public Calculations(int boardSize)
        {
            this.boardSize = boardSize;
            DirectionSigns = new short[(Int16)Direction.NE + 1, (Int16)Coords.Y + 1] {{-1, 0}, {-1, -1}, {0, -1}, {1, -1}};
        }

        public short[,,,] PiecesInRow
        {
            get
            {
                if (piecesInRow == null)
                    ClearPiecesInRow();
                return piecesInRow;
            }
        }

        private void ClearPiecesInRow()
        {
            piecesInRow = new short[boardSize, boardSize, (Int16)Direction.NE + 1, (Int16)GamePiece.O + 1];
            for (int x = 0; x < boardSize; x++)
                for (int y = 0; y < boardSize; y++)
                    foreach ( Direction direction in Enum.GetValues(typeof(Direction)))
                    {
                        for (GamePiece piece = GamePiece.X; piece < GamePiece.O; piece++)
                        {
                            piecesInRow[x, y, (Int16)direction, (Int16)piece] = 0;
                        }
                    }
        }
        public GamePiece[,] PiecesOnBoard
        {
            get
            {
                if (piecesOnBoard == null)
                {
                    ClearBoard();
                }

                return piecesOnBoard;
            }
        }

        private void ClearBoard()
        {
            piecesOnBoard = new GamePiece[boardSize, boardSize];
            for (int x = 0; x < boardSize; x++)
            for (int y = 0; y < boardSize; y++)
                piecesOnBoard[x, y] = GamePiece.Free;
        }

        public bool CoordsIsInRange(int x, int y)
        {
            return x < boardSize && x >= 0 && y < boardSize && y >= 0;
        }

        public GameResult AddPiece(int x, int y, GamePiece player)
        {
            GameResult result = GameResult.Continue;
            foreach (Direction direction in Enum.GetValues(typeof(Direction)))
            {
                for (int pos = 0; pos < rowLenght; pos++)
                {
                    short directHor = DirectionSigns[(Int16) direction, (Int16) Coords.X];
                    short directVer = DirectionSigns[(Int16) direction, (Int16) Coords.Y];
                    int posX = x + pos * directHor;
                    int posY = y + pos * directVer;
                    if (((directHor == -1 && posX >= 0 && posX <= boardSize - rowLenght) ||
                         (directHor == 1 && posX >= rowLenght - 1 && posX < boardSize) ||
                         (directHor == 0)) &&
                        ((directVer == -1 && posY >= 0 && posY <= boardSize - rowLenght) ||
                         (directVer == -0 && posY >= rowLenght - 1 && posY < boardSize)||
                         (directVer == 0)))
                    {
                        result = IncludeDraw(ref PiecesInRow[posX, posY, (Int16) direction, (Int16) player]);
                        if (result != GameResult.Continue)
                            break;
                    }
                }
                if (result != GameResult.Continue)
                {
                    break;
                }
            }

            piecesOnBoard[x, y] = player;
            return result;
        }

        private GameResult IncludeDraw(ref short countInRow)
        {
            countInRow++;
            if (countInRow == rowLenght)
            {
                return GameResult.Win;
            }

            return GameResult.Continue;
        }
    }
}

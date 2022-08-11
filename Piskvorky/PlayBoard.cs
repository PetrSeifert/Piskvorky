using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Piskvorky
{
    public partial class PlayBoard : UserControl
    {
        public PlayBoard()
        {
            InitializeComponent();
        }

        private int boardSize = 19;
        private int fieldSize = 20;
        private Color colorGrid = Color.Red;
        private Color colorX = Color.DarkBlue;
        private Color colorO = Color.DarkGreen;private GamePiece[,] piecesOnBoard;
        private GamePiece currentPlayer = GamePiece.X;
        private Calculations calc;

        private GamePiece Opponent
        {
            get
            {
                if (currentPlayer == GamePiece.X) return GamePiece.O;
                if (currentPlayer == GamePiece.O) return GamePiece.X;
                throw new Exception("Není hráč");
            }
        }

        private Calculations Calc
        {
            get
            {
                if (calc == null)
                    calc = new Calculations(boardSize);
                return calc;
            }
        }

       

        #region Pens
        private Pen penGrid;
        public Pen PenGrid
        {
            get
            {
                if (penGrid == null)
                {
                    penGrid = new Pen(colorGrid);
                }

                return penGrid;
            }
        }

        private Pen penX;
        public Pen PenX
        {
            get
            {
                if (penX == null)
                {
                    penX = new Pen(colorX);
                }

                return penX;
            }
        }

        private Pen penO;
        public Pen PenO
        {
            get
            {
                if (penO == null)
                {
                    penO = new Pen(colorO);
                }

                return penO;
            }
        }
        #endregion

        public int BoardSize
        {
            get => boardSize;
            set
            {
                boardSize = value;
                Refresh();
            }
        }

        public int FieldSize
        {
            get => fieldSize;
            set
            {
                fieldSize = value;
                Refresh();
            }
        }

        public Color ColorGrid
        {
            get => colorGrid;
            set
            {
                penGrid = null;
                colorGrid = value;
                Refresh();
            }
        }

        private void PlayBoard_Paint(object sender, PaintEventArgs e)
        {
            DrawBoard(e.Graphics);
            DrawPieces(e.Graphics);
        }

        private void DrawBoard(Graphics g)
        {
            for (int i = 0; i <= boardSize; i++)
            {
                g.DrawLine(PenGrid, 0, i * fieldSize, boardSize * fieldSize, i * fieldSize);
                g.DrawLine(PenGrid, i * fieldSize, 0, i * fieldSize, boardSize * fieldSize);
            }
        }

        private void DrawPiece(Graphics g, GamePiece piece, int x, int y)
        {
            if (!calc.CoordsIsInRange(x, y))
            {
                throw new Exception("Bod je mimo hrací plochu");
            }

            if (piece == GamePiece.X)
            {
                g.DrawLine(PenX, x * fieldSize + 1, y * fieldSize + 1, x * fieldSize + fieldSize - 1, y * fieldSize + fieldSize - 1);
                g.DrawLine(PenX, x * fieldSize + fieldSize - 1, y * fieldSize + 1, x * fieldSize + 1, y * fieldSize + fieldSize - 1);
            }

            if (piece == GamePiece.O)
            {
                g.DrawEllipse(PenO, x * fieldSize + 1, y * fieldSize + 1, fieldSize -2, fieldSize - 2);
            }
        }

        private void DrawPieces(Graphics g)
        {
            for (int x = 0; x < boardSize; x++)
            {
                for (int y = 0; y < boardSize; y++)
                {
                    DrawPiece(g, Calc.PiecesOnBoard[x, y], x, y);
                }
            }
        }

        private void PlayBoard_MouseClick(object sender, MouseEventArgs e)
        {
            int x = e.X / fieldSize;
            int y = e.Y / fieldSize;
            if (!Calc.CoordsIsInRange(x, y))
            {
                return;
            }

            if (Calc.PiecesOnBoard[x, y] != GamePiece.Free)
            {
                return;
            }

            GameResult result = Calc.AddPiece(x, y, currentPlayer);
            Refresh();
            if (result == GameResult.Win)
            {
                MessageBox.Show("Player " + currentPlayer + " won!");
            }
            currentPlayer = Opponent;
        }
    }
}

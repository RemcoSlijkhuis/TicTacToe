using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TicTacToeShared;

namespace TicTacToeViewer
{

    /// <summary>
    /// Used by Winforms application to visualize the board in a Panel
    /// </summary>
    class Drawer
    {

        private Panel panel;
        private Graphics graphics;
        private int width;
        private int height;

        public Drawer(Panel destination)
        {
            this.width = destination.Width;
            this.height = destination.Height;
            this.panel = destination;
            this.graphics = panel.CreateGraphics();
        }

        public void Draw(Game game)
        {

            Brush brush;

            int smallmargin = 10;
            int smallwidth = (int)Math.Floor(((this.width - (4 * smallmargin)) / 3) * 1.0);
            int smallheight = (int)Math.Floor(((this.height - (4 * smallmargin)) / 3) * 1.0);
            int cellmargin = 5;
            int cellwidth = (int)Math.Floor(((smallwidth - (4 * cellmargin)) / 3) * 1.0);
            int cellheight = (int)Math.Floor(((smallheight - (4 * cellmargin)) / 3) * 1.0);

            SmallBoard sb;
            int sx, sy;
            int cx, cy;


            Move lastMove = null;
            if (game.Moves.Count > 0)
            {
                lastMove = game.Moves[game.Moves.Count - 1];
            }

            for (int sbcolumn = 0; sbcolumn < 3; sbcolumn++)
            {
                for (int sbrow = 0; sbrow < 3; sbrow++)
                {

                    sb = game.Board.SmallBoards[sbcolumn, sbrow];
                    //draw background
                    sx = (sbcolumn + 1) * smallmargin + sbcolumn * smallwidth;
                    sy = (sbrow + 1) * smallmargin + sbrow * smallheight;


                    brush = new SolidBrush(Color.FromArgb(192, 192, 192));


                    if (sb.IsWinner(1)) { brush = new SolidBrush(Color.FromArgb(150, 50, 50)); }
                    if (sb.IsWinner(2)) { brush = new SolidBrush(Color.FromArgb(50, 150, 50)); }
                    if (sb.IsWinner(3)) { brush = new SolidBrush(Color.FromArgb(50, 50, 150)); }

                    graphics.FillRectangle(brush, new Rectangle(sx, sy, smallwidth, smallheight));


                    for (int cellcol = 0; cellcol < 3; cellcol++)
                    {
                        for (int cellrow = 0; cellrow < 3; cellrow++)
                        {

                            cx = sx + (cellcol + 1) * cellmargin + cellcol * cellwidth;
                            cy = sy + (cellrow + 1) * cellmargin + cellrow * cellheight;


                           // var cell = sb.Cells[cellcol, cellrow];
                            var owner = sb.GetOwner(cellcol, cellrow);
                            brush = new SolidBrush(Color.FromArgb(50, 50, 50));

                            if (owner == 1)
                            {
                                brush = new SolidBrush(Color.FromArgb(240, 0, 0));
                            }
                            if (owner == 2)
                            {
                                brush = new SolidBrush(Color.FromArgb(0, 240, 0));
                            }

                            graphics.FillRectangle(brush, new Rectangle(cx, cy, cellwidth, cellheight));

                        }

                    }
                }
            }
        }
    }

 
}

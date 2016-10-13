using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace _2048
{
    public partial class Form1 : Form
    {
        int score;
        Random rnd = new Random();
        int[][] grid = new int[4][];
        Label[][] lgrid = new Label[4][];

        public Form1()
        {
            InitializeComponent();
            InitializeGame();
        }

        private void InitializeGame()
        {
            score = 0;
            int x = 0, y = 0;
            for (int i = 0; i < 4; i++)
            {
                grid[i] = new int[4];
                lgrid[i] = new Label[4];
            }
            foreach (Control item in tlp.Controls)
            {
                if (item.Name.StartsWith("label"))
                {
                    lgrid[x][y] = (Label)item;

                    if (x < 3)
                        x++;
                    else
                    {
                        x = 0;
                        y++;
                    }
                }
            }

            for (int tiles = 0; tiles < 2; tiles++)
            {
                addTile();
            }
        }

        private void addTile()
        {
            int value = rnd.Next(2);
            if (value == 0)
                value = 2;
            else
                value = 4;

            bool Continue = false, ganho = false;
            for (int x = 0; x < grid.Length; x++)
            {
                for (int y = 0; y < grid[x].Length; y++)
                {
                    if (grid[x][y] == 0)
                        Continue = true;
                    else if (!ganho && grid[x][y] == 2048)
                        ganho = true;
                }
            }
            if (!Continue)
            {
                MessageBox.Show("O jogo Acabou");
                return;
            }
            else if (ganho)
            {
                MessageBox.Show("Ganhou o jogo");
                return;
            }
            while (Continue)
            {
                int x = rnd.Next(4), y = rnd.Next(4);
                if (grid[x][y] == 0)
                {
                    grid[x][y] = value;
                    Continue = false;
                }
            }

            arrayToLabels();
        }

        private Color setColor(int num)
        {
            if (num == 0)
                return SystemColors.ActiveBorder;

            int l = Convert.ToInt32(Math.Log(num, 2));
            int
                red = l * 100,
                green = l * 50,
                blue = l * 10;
            while (red>=255)
                red -= 255;
            while (green >= 255)
                green -= 255;
            while (blue >= 255)
                blue -= 255;
            return Color.FromArgb(red, green, blue);
        }

        private void arrayToLabels()
        {
            for (int x = 0; x < grid.Length; x++)
            {
                for (int y = 0; y < grid[x].Length; y++)
                {
                    if (grid[x][y] != 0)
                        lgrid[x][y].Text = grid[x][y].ToString();
                    else
                        lgrid[x][y].Text = "";
                    lgrid[x][y].BackColor = setColor(grid[x][y]);
                }
            }
            label1  = lgrid[0][0];
            label2  = lgrid[1][0];
            label3  = lgrid[2][0];
            label4  = lgrid[3][0];

            label5  = lgrid[0][1];
            label6  = lgrid[1][1];
            label7  = lgrid[2][1];
            label8  = lgrid[3][1];
            
            label9  = lgrid[0][2];
            label10 = lgrid[1][2];
            label11 = lgrid[2][2];
            label12 = lgrid[3][2];
            
            label13 = lgrid[0][3];
            label14 = lgrid[1][3];
            label15 = lgrid[2][3];
            label16 = lgrid[3][3];
        }

        #region MoveTheTiles
        private void goUp()
        {
            bool didmove = false;
            for (int i = 0; i < 3; i++)
            {
                for (int x = 0; x < grid.Length; x++)
                {
                    for (int y = grid[x].Length - 1; y > 0; y--)
                    {
                        if (grid[x][y] == 0 && grid[x][y -1] != 0)
                        {
                            grid[x][y] = grid[x][y - 1];
                            grid[x][y - 1] = 0;
                            didmove = true;
                        }
                    }
                }
            }
            for (int x = 0; x < grid.Length; x++)
            {
                for (int y = grid[x].Length - 1; y > 0; y--)
                {
                    if (grid[x][y] == grid[x][y - 1] && grid[x][y - 1] != 0)
                    {
                        grid[x][y] += grid[x][y];
                        score += grid[x][y];
                        grid[x][y - 1] = 0;
                        didmove = true;
                    }
                }
            }
            arrayToLabels();
            if (didmove)
                addTile();
        }
        private void goDown()
        {
            bool didmove = false;
            for (int i = 0; i < 3; i++)
            {
                for (int x = 0; x < grid.Length; x++)
                {
                    for (int y = 0; y < grid[x].Length - 1; y++)
                    {
                        if (grid[x][y] == 0 && grid[x][y + 1] != 0)
                        {
                            grid[x][y] = grid[x][y + 1];
                            grid[x][y + 1] = 0;
                            didmove = true;
                        }
                    }
                }
            }
            for (int x = 0; x < grid.Length; x++)
            {
                for (int y = 0; y < grid[x].Length - 1; y++)
                {
                    if (grid[x][y] == grid[x][y + 1] && grid[x][y + 1] != 0)
                    {
                        grid[x][y] += grid[x][y];
                        score += grid[x][y];
                        grid[x][y + 1] = 0;
                        didmove = true;
                    }
                }
            }
            arrayToLabels();
            if (didmove)
                addTile();
        }
        private void goLeft()
        {
            bool didmove = false;
            for (int i = 0; i < 3; i++)
            {
                for (int x = grid.Length - 1; x > 0; x--)
                {
                    for (int y = 0; y < grid[x].Length; y++)
                    {
                        if (grid[x][y] == 0 && grid[x - 1][y] != 0)
                        {
                            grid[x][y] = grid[x - 1][y];
                            grid[x - 1][y] = 0;
                            didmove = true;
                        }
                    }
                }
            }
            for (int x = grid.Length - 1; x > 0; x--)
            {
                for (int y = 0; y < grid[x].Length; y++)
                {
                    if (grid[x][y] == grid[x - 1][y] && grid[x - 1][y] != 0)
                    {
                        grid[x][y] += grid[x][y];
                        score += grid[x][y];
                        grid[x - 1][y] = 0;
                        didmove = true;
                    }
                }
            }
            arrayToLabels();
            if (didmove)
                addTile();
        }
        private void goRigth()
        {
            bool didmove = false;
            for (int i = 0; i < 3; i++)
            {
                for (int x = 0; x < grid.Length - 1; x++)
                {
                    for (int y = 0; y < grid[x].Length; y++)
                    {
                        if (grid[x][y] == 0 && grid[x + 1][y] != 0)
                        {
                            grid[x][y] = grid[x + 1][y];
                            grid[x + 1][y] = 0;
                            didmove = true;
                        }
                    }
                }
            }
            for (int x = 0; x < grid.Length - 1; x++)
            {
                for (int y = 0; y < grid[x].Length; y++)
                {
                    if (grid[x][y] == grid[x + 1][y] && grid[x + 1][y] != 0)
                    {
                        grid[x][y] += grid[x][y];
                        score += grid[x][y];
                        grid[x + 1][y] = 0;
                        didmove = true;
                    }
                }
            }
            arrayToLabels();
            if (didmove)
                addTile();
        } 
        #endregion

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up: goUp(); break;
                case Keys.Down: goDown(); break;
                case Keys.Right: goRigth(); break;
                case Keys.Left: goLeft(); break;
                default:
                    break;
            }

            lblScore.Text = score.ToString();

            if (!checkPlaying())
                MessageBox.Show("O jogo Acabou");
        }

        private bool checkPlaying()
        {
            bool canmove = false;
            #region UP
            for (int i = 0; i < 3; i++)
            {
                for (int x = 0; x < grid.Length; x++)
                {
                    for (int y = grid[x].Length - 1; y > 0; y--)
                    {
                        if (grid[x][y] == 0 && grid[x][y - 1] != 0)
                        {
                            canmove = true;
                        }
                    }
                }
            }
            for (int x = 0; x < grid.Length; x++)
            {
                for (int y = grid[x].Length - 1; y > 0; y--)
                {
                    if (grid[x][y] == grid[x][y - 1] && grid[x][y - 1] != 0)
                    {
                        canmove = true;
                    }
                }
            } 
            #endregion
            #region Down
            for (int i = 0; i < 3; i++)
            {
                for (int x = 0; x < grid.Length; x++)
                {
                    for (int y = 0; y < grid[x].Length - 1; y++)
                    {
                        if (grid[x][y] == 0 && grid[x][y + 1] != 0)
                        {
                            canmove = true;
                        }
                    }
                }
            }
            for (int x = 0; x < grid.Length; x++)
            {
                for (int y = 0; y < grid[x].Length - 1; y++)
                {
                    if (grid[x][y] == grid[x][y + 1] && grid[x][y + 1] != 0)
                    {
                        canmove = true;
                    }
                }
            }
            #endregion
            #region Right
            for (int i = 0; i < 3; i++)
            {
                for (int x = 0; x < grid.Length - 1; x++)
                {
                    for (int y = 0; y < grid[x].Length; y++)
                    {
                        if (grid[x][y] == 0 && grid[x + 1][y] != 0)
                        {
                            canmove = true;
                        }
                    }
                }
            }
            for (int x = 0; x < grid.Length - 1; x++)
            {
                for (int y = 0; y < grid[x].Length; y++)
                {
                    if (grid[x][y] == grid[x + 1][y] && grid[x + 1][y] != 0)
                    {
                        canmove = true;
                    }
                }
            }
            #endregion
            #region Left
            for (int i = 0; i < 3; i++)
            {
                for (int x = grid.Length - 1; x > 0; x--)
                {
                    for (int y = 0; y < grid[x].Length; y++)
                    {
                        if (grid[x][y] == 0 && grid[x - 1][y] != 0)
                        {
                            canmove = true;
                        }
                    }
                }
            }
            for (int x = grid.Length - 1; x > 0; x--)
            {
                for (int y = 0; y < grid[x].Length; y++)
                {
                    if (grid[x][y] == grid[x - 1][y] && grid[x - 1][y] != 0)
                    {
                        canmove = true;
                    }
                }
            }
            #endregion
            return canmove;
        }

        private void lblNewGame_Click(object sender, EventArgs e)
        {
            InitializeGame();
        }
    }
}

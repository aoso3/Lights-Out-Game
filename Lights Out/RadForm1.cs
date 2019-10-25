using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;

namespace Lights_Out
{
    public partial class RadForm1 : Telerik.WinControls.UI.RadForm
    {

        private List<PictureBox> boardPicture;
        private List<Label> solveLabel;

        private double Height;
        private double Width;


        private int rowscount = -1;
        private int columnscount =  -1;

        private int state;

        BFS bfs = new BFS();
        OurAlgo ouralgo = new OurAlgo();
        Board board;
        int[,] userBoard;
        bool[,] solveBoard;
        public RadForm1()
        {
            InitializeComponent();
            tableSolve.Hide();
            table.Click += new EventHandler(this.clickCell);
            radLabel1.Hide();
            radLabel2.Hide();
        }

        private void RadForm1_Load(object sender, EventArgs e)
        {

        }

        void init()
        {
            Random rnd = new Random();
            userBoard = new int[rowscount, columnscount];
            for (int i = 0; i < rowscount; i++)
            {
                for (int j = 0; j < columnscount; j++)
                {
                    userBoard[i, j] = rnd.Next(2); 
                }
            }

        }

        private void drawTable(TableLayoutPanel table, bool solve = false)
        {
            table.RowCount = rowscount;
            table.ColumnCount = columnscount;

            this.Height = table.Height / table.RowCount;

            for (int i = 0; i < table.RowCount; i++)
            {
                table.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, (float)this.Height));
            }



            this.Width = table.Width / table.ColumnCount;
            Console.WriteLine(" table.Height : " + table.Height.ToString() + "\t table.Width: " + table.Width.ToString());
            Console.WriteLine(" Height : " + Height.ToString() + "\t Width: " + Width.ToString());
            for (int i = 0; i < table.ColumnCount; i++)
            {
                table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, (float)this.Width));
            }
            if (!solve)
            {
                boardPicture = new List<PictureBox>();
                for (int i = 0; i < table.RowCount; i++)
                {
                    for (int j = 0; j < table.ColumnCount; j++)
                    {
                        PictureBox box;
                        if (userBoard[i, j] == 0)
                            box = createPictureBox(false);
                        else
                            box = createPictureBox(true);
                        this.boardPicture.Add(box);
                        table.Controls.Add(box, j, i);
                        //box.SendToBack();
                        box.Click += new EventHandler(this.clcikeventhandle);

                    }
                }
                //table.BringToFront();
                
            }
            else
            {
                solveLabel = new List<Label>();
                for (int i = 0; i < table.RowCount; i++)
                {
                    for (int j = 0; j < table.ColumnCount; j++)
                    {
                        table.Controls.Add(createLabel(i, j), j, i);

                    }
                }

                //table.Click += new clcikeventhandle(this.clickCell);
            }
            table.Refresh();
        }


        private PictureBox createPictureBox(bool light)
        {

            PictureBox pictureBox = new PictureBox() { Width = (int)this.Width, Height = (int)this.Height, AutoSize = false, Font = new Font("Arial", 12, FontStyle.Bold) };

            //if (i == 0)
            //    newtextbox.KeyPress += new KeyPressEventHandler(this.textBox_KeyPress);
            //else
            //    newtextbox.KeyPress += new KeyPressEventHandler(this.textBox_KeyPress1);

            pictureBox.BackgroundImageLayout = ImageLayout.Stretch;
            if (!light)
                pictureBox.BackgroundImage = Properties.Resources.unlight;
            else
                pictureBox.BackgroundImage = Properties.Resources.light;
            //pictureBox.Click += new EventHandler(this.clickCell);
            return pictureBox;
        }
        private Label createLabel(int i, int j)
        {

            Label label = new Label() { Width = (int)this.Width, Height = (int)this.Height, AutoSize = false, Font = new Font("Arial", 12, FontStyle.Bold), TextAlign = ContentAlignment.MiddleCenter, Text = (solveBoard[i, j]) ? "1" : "0" };
            return label;
        }
        void clcikeventhandle(object sender, EventArgs e)
        {
            clickCell(sender, e);
        }

        private void clickCell(object sender, EventArgs e)
        {
            //PictureBox pictureBox = (PictureBox)sender;

            //if (pictureBox.BackgroundImage == Properties.Resources.light)
            //{

            //}
            var cellPos = GetRowColIndex(table, table.PointToClient(Cursor.Position));
            if (cellPos!= null)
            {
                if (state == 2)
                    edit(cellPos.Value.X, cellPos.Value.Y);
                else if (state == 1)
                {
                    int i = cellPos.Value.X, j = cellPos.Value.Y;
                    edit(i, j);

                    if (i + 1 < rowscount)
                        edit(i + 1, j);

                    if (i - 1 >= 0)
                        edit(i - 1, j);

                    if (j + 1 < columnscount)
                        edit(i, j + 1);


                    if (j - 1 >= 0)
                        edit(i, j - 1);

                    board = new Board(userBoard, rowscount, columnscount);
                    if (board.isFinished())
                        MessageBox.Show("You are the winner  ^_^ ");
                }
            }
            

        }
        Point? GetRowColIndex(TableLayoutPanel tlp, Point point)
        {
            if (point.X > tlp.Width || point.Y > tlp.Height)
                return null;

            int w = tlp.Width;
            int h = tlp.Height;
            int[] widths = tlp.GetColumnWidths();

            int i;
            for (i = widths.Length - 1; i >= 0 && point.X < w; i--)
                w -= widths[i];
            int col = i + 1;

            int[] heights = tlp.GetRowHeights();
            for (i = heights.Length - 1; i >= 0 && point.Y < h; i--)
                h -= heights[i];

            int row = i + 1;

            return new Point(row, col);
        }

        private void edit(int i, int j)
        {
            this.userBoard[i, j] = (this.userBoard[i, j] == 0) ? 1 : 0;
            int index = i * columnscount + j;
            this.boardPicture[index].BackgroundImage = (this.userBoard[i, j] == 0) ? Properties.Resources.unlight : Properties.Resources.light;
        }
        private void radLabel1_Click(object sender, EventArgs e)
        {

        }


        ///Cick Play Button
        private void radButton1_Click(object sender, EventArgs e)
        {
            if (radDropDownList1.Text != "Boarder Size")
            {
                state = 1;
            }
            else
            {
                MessageBox.Show("You should select board size");
            }

        }

        ///Cick Edit Button
        private void radButton2_Click(object sender, EventArgs e)
        {
            if (radDropDownList1.Text != "Boarder Size")
            {
                state = 2;
            }
            else
            {
                MessageBox.Show("You should select board size");
            }
        }

        ///Cick Solve Button
        private void radButton3_Click(object sender, EventArgs e)
        {
            if (userBoard != null)
            {
                Board board = new Board(this.userBoard,this.rowscount,this.columnscount);
                if (rowscount == 3  )
                {
                    try
                    {
                        bfs = new BFS();
                        board = bfs.bfs(board);
                    }
                    catch (Exception ee)
                    {
                        MessageBox.Show("memory is full, go and upgrade your ram fool !");
                    }
                }
                else
                {
                    ouralgo = new OurAlgo();
                    board = ouralgo.solve(board);
                }
                if (board == null)
                    MessageBox.Show("Sorry , No solution");
                else
                {
                    board.printBoard();
                    tableSolve.Show();
                    radLabel1.Show();
                    radLabel2.Show();
                    radLabel1.Text = "Steps Number : " + board.getDepth(); ;
                    this.solveBoard = board.visited;
                    tableSolve.ColumnStyles.Clear();
                    tableSolve.RowStyles.Clear();
                    tableSolve.Controls.Clear();
                    if (this.solveLabel != null)
                        this.solveLabel.Clear();
                    drawTable(this.tableSolve, true);
                }
            }
        }


        ///click Exit button
        private void radButton6_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        ///click reset button
        private void radButton5_Click(object sender, EventArgs e)
        {
            if (userBoard != null)
            {
                for (int i = 0; i < rowscount; i++)
                {
                    for (int j = 0; j < columnscount; j++)
                    {
                        userBoard[i, j] = 1;
                        edit(i, j);
                    }
                }
               tableSolve.Hide();
               radLabel1.Hide();
               radLabel2.Hide();
               table.Refresh();
            }
            else
            {
                MessageBox.Show("You should select board size");

            }
           
        }


        ///click new game button
        private void radButton4_Click(object sender, EventArgs e)
        {
            if (radDropDownList1.Text != "Boarder Size")
            {
                state = 1;
                
                table.ColumnStyles.Clear();
                table.RowStyles.Clear();
                table.Controls.Clear();
                if (this.boardPicture!=null)
                    this.boardPicture.Clear();
                
                rowscount =  Convert.ToInt32(radDropDownList1.Text[0].ToString());
                if (rowscount == 1)
                    rowscount = 10;
                columnscount = rowscount;// Convert.ToInt32(radDropDownList1.Text[0].ToString());
                init();
                drawTable(table);
                tableSolve.Hide();
                radLabel2.Hide();
                radLabel1.Hide();
            }
            else
            {
                MessageBox.Show("You should select board size");
            }
        }

        private void radButton7_Click(object sender, EventArgs e)
        {
            if (userBoard != null)
            {
                int numberLight = 0;
                for (int i = 0; i < rowscount; i++)
                {
                    for (int j = 0; j < columnscount; j++)
                    {
                        if (userBoard[i, j] == 1)
                            numberLight++;
                    }
                }
                radButton5_Click(sender, e);
                int k, v;
                for (int i = 0; i < numberLight; i++)
                {
                    Random rand = new Random();
                    while (true)
                    {
                        k = rand.Next(rowscount);
                        v=rand.Next(columnscount);
                        if (userBoard[k,v]==0)
                        {
                            edit(k,v);
                            break;
                        }
                    }
                }
                tableSolve.Hide();
                radLabel1.Hide();
                radLabel2.Hide();
                table.Refresh();

            }

            //if (userBoard != null)
            //{
            //    for (int i = 0; i < rowscount; i++)
            //    {
            //        for (int j = 0; j < columnscount; j++)
            //        {
            //            userBoard[i, j] = 1;
            //            edit(i, j);
            //        }
            //    }
            //    tableSolve.Hide();
            //    table.Refresh();
            //}



        }

        private void radPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lights_Out
{
    public class Board : IComparable<Board>
    {
        public int[,] board;
        public int rowNum;
        public int colNum;
        public bool[,] visited;
        public int lastIndexI;
        public int lastIndexJ;
        public int depth;
        public Board(int[,] board, int rowNum, int colNum)
        {
            depth = 0;
            this.rowNum = rowNum;
            this.colNum = colNum;
            this.visited = new bool[rowNum, colNum];
            for (int i = 0; i < rowNum; i++)
                for (int j = 0; j < colNum; j++)
                    visited[i, j] = false;

            this.board = new int[rowNum, colNum];

            for (int i = 0; i < rowNum; i++)
                for (int j = 0; j < colNum; j++)
                    this.board[i, j] = board[i, j];
        }

        public Board(Board b)
        {
            this.rowNum = b.rowNum;
            this.colNum = b.colNum;
            this.board = new int[rowNum, colNum];
            this.visited = new bool[rowNum, colNum];
            for (int i = 0; i < rowNum; i++)
                for (int j = 0; j < colNum; j++)
                    visited[i, j] = b.visited[i, j];


            for (int i = 0; i < rowNum; i++)
                for (int j = 0; j < colNum; j++)
                    board[i, j] = b.board[i, j];
            depth = b.depth;
        }

        //public int[] evaluate()
        //{
        //    int indexI = 0;
        //    int indexJ = 0;
        //    double max = -1;
        //    int temp = 0;

        //    for (int i = 0; i < rowNum; i++)
        //    {
        //        for (int j = 0; j < colNum; j++)
        //        {
        //            temp = 0;
        //            int count = 0;
        //            if (board[i,j] == 1)
        //                temp++;


        //            if (i + 1 < rowNum)
        //            {
        //                if (board[i + 1,j] == 1)
        //                    temp++;
        //                count++;
        //            }

        //            if (i - 1 >= 0)
        //            {
        //                if (board[i - 1,j] == 1)
        //                    temp++;
        //                count++;
        //            }

        //            if (j + 1 < colNum)
        //            {
        //                if (board[i,j + 1] == 1)
        //                    temp++;
        //                count++;
        //            }


        //            if (j - 1 >= 0)
        //            {
        //                if (board[i,j - 1] == 1)
        //                    temp++;
        //                count++;
        //            }


        //            if ((double)(temp / count) > max)
        //            {
        //                max = (double)(temp / count);
        //                indexI = i;
        //                indexJ = j;
        //            }
        //        }

        //    }
        //    clickCell(indexI, indexJ);
        //    visited[indexI,indexJ] = true;
        //    int[] a = { indexI, indexJ };
        //    return a;



        //}
        public void clickCell(int i, int j, bool visited = false)
        {
            lastIndexI = i;
            lastIndexJ = j;
            
            this.board[i, j] = (this.board[i, j] == 0) ? 1 : 0;

            if (i + 1 < rowNum)
                this.board[i + 1, j] = (this.board[i + 1, j] == 0) ? 1 : 0;

            if (i - 1 >= 0)
                this.board[i - 1, j] = (this.board[i - 1, j] == 0) ? 1 : 0;

            if (j + 1 < colNum)
                this.board[i, j + 1] = (this.board[i, j + 1] == 0) ? 1 : 0;

            if (j - 1 >= 0)
                this.board[i, j - 1] = (this.board[i, j - 1] == 0) ? 1 : 0;

            if (visited)
            {
                this.visited[i, j] = !this.visited[i, j];
                depth++;
            }
        }



        public LinkedList<Board> childrenBoard(int i, int j)
        {
            LinkedList<Board> nextBoard = new LinkedList<Board>();
            Board temp;
            int prio = getStateVal();
            for (int k = 0; k < rowNum; k++)
                for (int v = 0; v < colNum; v++)
                {

                    temp = new Board(this);
                    temp.clickCell(k, v);
                    //                if (prio>=temp.getStateVal())
                    nextBoard.AddFirst(temp);
                }

            //        if (i+1 < rowNum){
            //            temp = new Board(this);
            //            temp.clickCell(i+1,j);
            //            nextBoard.add(temp);
            //        }
            //
            //        if (i-1 >= 0)
            //        {
            //            temp = new Board(this);
            //            temp.clickCell(i-1,j);
            //            nextBoard.add(temp);
            //        }
            //
            //        if (j+1 < colNum)
            //        {
            //            temp = new Board(this);
            //            temp.clickCell(i,j+1);
            //            nextBoard.add(temp);
            //        }
            //
            //
            //        if (j - 1 >= 0)
            //        {
            //            temp = new Board(this);
            //            temp.clickCell(i,j-1);
            //            nextBoard.add(temp);
            //        }

            return nextBoard;
        }


        public LinkedList<Board> childrenfirstrow(int i, int j)
        {
            LinkedList<Board> nextBoard = new LinkedList<Board>();
            Board temp;
            int prio = getStateVal();
                for (int v = 0; v < colNum; v++)
                {

                    temp = new Board(this);
                    temp.clickCell(0, v);
                    nextBoard.AddFirst(temp);
                }


            return nextBoard;
        }


        public bool isFinished()
        {
            for (int i = 0; i < rowNum; i++)
            {
                for (int j = 0; j < colNum; j++)
                {
                    if (board[i, j] == 1)
                        return false;
                }
            }
            return true;
        }
        int getStateVal()
        {
            int val = 0;
            int basee = 1;
            for (int i = 0; i < rowNum; i++)
            {
                for (int j = 0; j < colNum; j++)
                {
                    val += basee * board[i, j];
                    basee *= 2;
                }
            }
            return val;
        }

        public void printBoard()
        {
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("Board : ");


            for (int i = 0; i < rowNum; i++)
            {
                for (int j = 0; j < colNum; j++)
                {
                    Console.Write(board[i, j] + "\t");

                }
                Console.WriteLine("");

            }

            /////////////////
            Console.WriteLine("Visited : ");

            for (int i = 0; i < rowNum; i++)
            {
                for (int j = 0; j < colNum; j++)
                {
                    Console.Write(((visited[i, j]) ? 1 : 0) + "\t");

                }
                Console.WriteLine("");

            }
        }

        public int CompareTo(Board other)
        {
            int a = getStateVal();
            int b = other.getStateVal();
            if (a < b) return -1;
            else if (a > b) return 1;
            else return 0;
        }

        public override string ToString()
        {
            return "(" + rowNum + ", " + colNum + ")";
        }


        public int getDepth()
        {
            int num = 0;
            for (int i = 0; i < rowNum; i++)
            {
                for (int j = 0; j < colNum; j++)
                {
                    num += (visited[i, j]) ? 1 : 0;

                }
            }
            return num;
        }


    }



}

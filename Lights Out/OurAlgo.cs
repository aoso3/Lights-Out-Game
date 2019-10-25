using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lights_Out
{
    class OurAlgo
    {


        Queue<Board> queue = new Queue<Board>();
        int minDepth = int.MaxValue;
        Board bestBoard;
    
        public Board bfs2(Board board)
        {
            board.depth = 0;
            queue.Enqueue(board);
            while (queue.Count() != 0)
            {
                Board b = queue.Dequeue();
                if (b.isFinished())
                {
                    if (b.depth < minDepth)
                    {
                        minDepth = b.depth;
                        bestBoard = b;
                    }
                }
                for (int i = 0; i < b.rowNum - 1; i++)
                {
                    change_row(i, b);
                }
                if (b.isFinished())
                {
                    if (b.depth < minDepth)
                    {
                        minDepth = b.depth;
                        bestBoard = b;
                    }
                }
                else if (b.depth < minDepth)
                {
                    //if (b.depth >= 9)
                    //{
                    //    return null;
                    //}
                    LinkedList<Board> nextBoards = b.childrenfirstrow(0, 0);
                    foreach (Board next in nextBoards)
                    {
                        if (!next.visited[next.lastIndexI, next.lastIndexJ])
                        {
                            next.visited[next.lastIndexI, next.lastIndexJ] = true;
                            next.depth++;
                            try
                            {
                                queue.Enqueue(next);
                            }
                            catch (Exception e)
                            {
                                if (bestBoard != null) return bestBoard;
                                throw new OutOfMemoryException();
                            }
                        }
                    }

                }
            }
            if (bestBoard == null)
                return null;
            else
                return bestBoard;
        }




        public Board solve(Board board){
            
         for (int i = 0; i < board.rowNum-1; i++) {
            change_row(i,board); 
         }
           return bfs2(board);












         if (board.rowNum == 5)
         {
             lastrow_five(board);
         }
         else{
         lastrow_six(board);
         }
         for (int i = 0; i < board.rowNum - 1; i++)
         {
              change_row(i, board); 

         }
          if (board.isFinished()){
                return board;
          }
          else{
              return null;
          }
    }


        void change_row(int row , Board board)
        {
            for (int i = 0; i < board.rowNum; i++)
            {
                if (board.board[row,i] == 1)
                {
                    board.clickCell(row + 1, i, true);
                    
                }
            }
        }
        void lastrow_five(Board board)
        {
            if (board.board[4,0] == 1)
            {
                board.clickCell(0, 3, true);
                board.clickCell(0, 4, true);
            }
            if (board.board[4,1] == 1)
            {
                board.clickCell(0, 1, true);
                board.clickCell(0, 4, true);
            }
            if (board.board[4,2] == 1)
            {
                board.clickCell(0, 3, true);
            }
        }

        void lastrow_six(Board board)
        {
            if (board.board[5,0] == 1)
            {
                board.clickCell(0, 0, true);
                board.clickCell(0, 2, true);
            }
            if (board.board[5,1] == 1)
            {
                board.clickCell(0, 3, true);
            }
            if (board.board[5,2] == 1)
            {
                board.clickCell(0, 0, true);
                board.clickCell(0, 4, true);
            }
            if (board.board[5,3] == 1)
            {
                board.clickCell(0, 1, true);
                board.clickCell(0, 5, true);
            }
            if (board.board[5,4] == 1)
            {
                board.clickCell(0, 2, true);
            }
            if (board.board[5,5] == 1)
            {
                board.clickCell(0, 3, true);
                board.clickCell(0, 5, true);
            }
        }
    }
}

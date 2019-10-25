using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lights_Out
{
    public class BFS 
    {
        //List<Board> week = new List<Board>();
         PriorityQueue<Board> queue = new PriorityQueue<Board>();
         int minDepth = int.MaxValue;
         Board bestBoard;
        // add some stuff to the list
        // now sort

    //    Queue<Board>queue = new Queue<Board>();

    //    Lights_Out.PriorityQueue.PriorityQueue<Board> a = new Lights_Out.PriorityQueue.PriorityQueue<Board>();
    //    Queue<Board> queue = new SortedList<Board>(new Comparator<Board>() {
    //    @Override
    //    public int compare(Board o1, Board o2) {
    //        return o1.getStateVal() - o2.getStateVal() ;
    //    }
    //});   
            
            public Board bfs(Board board) {
                queue.Enqueue(board);
                while (queue.Count()!=0){
                    Board b = queue.Dequeue();
                    if (b.isFinished())
                    {
                        if (b.depth < minDepth)
                        {
                            minDepth = b.depth;
                            bestBoard = b;
                        }

                    }
                    else if (b.depth < minDepth )
                    {
                        if (b.depth >= 9)
                        {
                            return null;
                        }
                        LinkedList<Board> nextBoards = b.childrenBoard(0, 0);
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

    }










}

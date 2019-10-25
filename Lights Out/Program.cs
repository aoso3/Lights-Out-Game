using System;
using System.Linq;
using System.Windows.Forms;

namespace Lights_Out
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            Application.Run(new RadForm1());


             BFS m = new BFS();


                    //         Work

            //        int [][]arr  = {
            //                {1, 0, 0},
            //                {1, 0, 1},
            //                {1, 1, 1}
            //        };
            //
            //        Board board = new Board(arr,3,3);
            //
            //




                    //        Not Work on web
            //
            //        int [][]arr  = {
            //                {1, 0, 1,0,0},
            //                {1, 0, 1,1,0},
            //                {0,1, 1,0, 1},
            //                {1,0, 0,0, 1},
            //                {0,0,1, 1,0}
            //        };
            //
            //        Board board = new Board(arr,5,5);



            //        Not Work
            //        int [][]arr  = {
            //                {1, 1, 1,0,1},
            //                {0, 1, 1,1,0},
            //                {1,0, 1,0, 1},
            //                {1,1, 1,1, 1},
            //                {1,1,0, 1,0}
            //        };
            //        Board board = new Board(arr,5,5);



            //
            //                int [][]arr  = {
            //                {0,0,0,1},
            //                {1,0, 0, 1},
            //                {1,0, 0,0},
            //                {1,1,0, 1}
            //        };
            //        Board board = new Board(arr,4,4);

                    //

                    //work
                            int [,]arr  = {
                            {1,0,1,1},
                            {0,1, 1, 0},
                            {0,0, 1,1},
                            {1,1,1, 1} 
                    };
                    Board board = new Board(arr,4,4);

                    //        int [,]arr  = {
                    //        {1, 0, 1,0,1},
                    //        {1, 1, 0,1,1},
                    //        {0,0, 0,1, 0},
                    //        {0,0, 1,1, 0}

                    //};
                    //Board board = new Board(arr,4,5);


                    board.printBoard();


                    board = m.bfs(board);
                    if (board == null)
                        Console.WriteLine("Error");
                    else
                    if (board.isFinished()){
                        Console.WriteLine("Fininsh");
                        board.printBoard();
                    }
















        }
    }
}
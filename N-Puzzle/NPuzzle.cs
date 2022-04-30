using System;
using System.Collections.Generic;
using System.Text;

namespace N_Puzzle
{
    class NPuzzle
    {
        public int[,] matrix;
        public int depth;
        int n;
        public int x0;
        public int y0;

        public NPuzzle(int n)
        {
            this.n = n;
            matrix = new int[n, n];
        }
        
        public bool isSolvable(List<int> lis)
        { 
            int inversion = 0;
            for(int x = 0 ; x < n*n-1 ; x++)
            {
                for(int y = x + 1 ; y < n*n ; y++)
                {
                    if (lis[y] != 0 && lis[x] != 0 && (lis[x] > lis[y]))
                    {
                        inversion++;
                    }
                        
                }
            }

            if (n % 2 != 0)
            {
                if (inversion % 2 != 0) return false;
            }
            else
            {
                int index = 0;
                for (int i = n - 1; i >= 0; i--)
                    for (int j = n - 1; j >= 0; j--)
                        if (matrix[i,j] == 0)
                            index = n-i;

                if ((index % 2 == 0 && inversion %2 == 0 )|| (index % 2 != 0 && inversion % 2 != 0)) return false;
            }
            return true; 
        }


        public void solveHamming()
        {

            while (hamming(matrix) != 1)
            {                                  /*up*//*down*/ /*left*/  /*right*/
                int[] aroundZeroX = new int[4] { x0 - 1, x0 + 1, x0,     x0 };
                int[] aroundZeroY = new int[4] { y0,     y0,     y0 - 1, y0 + 1 };
                PriorityQueue priorityQueue = new PriorityQueue();
                depth++;
                for ( int i = 0; i < 4; i++)
                {
                    try
                    {
                        Node node = new Node();
                        node.mat = (int[,]) matrix.Clone();
                        Swap(x0, y0, aroundZeroX[i], aroundZeroY[i], node);
                        node.hValue = hamming(node.mat) + depth;
                        priorityQueue.push(node);
                        
                        //Print(node.mat);
                    }
                    catch
                    {
                        continue;
                    }
                }
                Node no = priorityQueue.pop();
                matrix = no.mat;
                x0 = no.x0;
                y0 = no.y0;
                if (n == 3)
                {
                    Print(matrix);
                    Console.WriteLine(depth);
                }
            }

            
        }

        int manhattan(int x, int x2, int y, int y2)
        {
            return Math.Abs(x - x2) + Math.Abs(y - y2);
        }

        public int hamming(int[,] mat)
        {
            int misPlaced = 0, temp = 1;
            for (int i = 0; i < n; i++)
            {
                for(int j = 0; j < n; j++)
                {
                    if (mat[i, j] != temp) 
                            misPlaced++;
                    temp++;
                }
            }

            return misPlaced;
        }

        void Swap(int first, int second , int third, int forth , Node node)
        {
            int temp = node.mat[first, second];
            node.mat[first,second] = node.mat[third, forth];
            node.mat[third, forth] = temp;
            node.x0 = third;
            node.y0 = forth;
        }

        void Print(int[,] matrix)
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Console.Write(matrix[i, j] + " ");
                }
                Console.WriteLine();
            }

            Console.WriteLine();
        }
    }
}

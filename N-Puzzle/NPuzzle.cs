using System;
using System.Collections.Generic;
using System.Text;

namespace N_Puzzle
{
    class NPuzzle
    {
        class Node
        {
            public int[,] mat;
            public int depth;
            public int minDistance;
        }
        public bool isSolvable(List<int> lis, int n, int[,]matrix)
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


        public void solve(int[,] matrix)
        {
            Node node = new Node();
            node.mat = matrix;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace N_Puzzle
{
   public class Node
    {
        public int[,] mat;
        public int hValue;
        public int x0 = -1;
        public int y0 = -1 ;
        public int depth;
        public string state;
        public Node parent;

        public Node(int[,] mat , int depth, Node parent)
        {
            this.mat = (int[,])mat.Clone();
            this.depth = depth;
            this.parent = parent;
        }

        public void setState()
        {
            for(int i = 0; i < Math.Sqrt(mat.Length); i++)
            {
                for(int j = 0; j < Math.Sqrt(mat.Length); j++)
                {
                    state += mat[i, j].ToString();
                }
            }

        }

    }
}

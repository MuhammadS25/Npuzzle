using System;
using System.Collections.Generic;
using System.Text;

namespace N_Puzzle
{
   public class Node
    {
        public int[,] mat;
        public int hValue;
        public int x0 = 0;
        public int y0 = 0;
        public int depth;
        public string state;
        public int n;
        public Node parent;
    }
}

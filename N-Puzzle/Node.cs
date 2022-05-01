using System;
using System.Collections.Generic;
using System.Text;

namespace N_Puzzle
{
   public class Node
    {
        public int[,] mat;
        public int[,] goal;
        public int hValue;
        public int x0 = -1;
        public int y0 = -1 ;
        public int depth;
        public UInt64 state;

        public Node()
        {

        }

        public Node(Node node)
        {
            this.mat = (int[,])node.mat.Clone();
        }
        public void setState()
        {
            string s = "";
            for(int i = 0; i < Math.Sqrt(mat.Length); i++)
            {
                for(int j = 0; j < Math.Sqrt(mat.Length); j++)
                {
                    s += mat[i, j].ToString();
                }
            }

           state =  CalculateHash(s);
        }

        static UInt64 CalculateHash(string read)
        {
            UInt64 hashedValue = 3074457345618258791ul;
            for (int i = 0; i < read.Length; i++)
            {
                hashedValue += read[i];
                hashedValue *= 3074457345618258799ul;
            }
            return hashedValue;
        }

    }
}

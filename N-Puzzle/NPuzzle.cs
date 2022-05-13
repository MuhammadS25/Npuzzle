using System;
using System.Collections.Generic;
using System.Text;

namespace N_Puzzle
{
    class NPuzzle
    {
        public int[,] matrix;
        int n, methodType;
        public int x0, y0;
        PriorityQueue priorityQueue = new PriorityQueue();
        HashSet<string> set = new HashSet<string>();


        public NPuzzle(int n, int methodType)
        {
            this.n = n;
            this.methodType = methodType;
            matrix = new int[n, n];
        }

        public bool isSolvable(List<int> lis)
        {
            int inversion = 0;
            for (int x = 0; x < n * n - 1; x++)
            {
                for (int y = x + 1; y < n * n; y++)
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
                        if (matrix[i, j] == 0)
                            index = n - i;

                if ((index % 2 == 0 && inversion % 2 == 0) || (index % 2 != 0 && inversion % 2 != 0)) return false;
            }
            return true;
        }

        int method(int type, Node node){

            int value = 0;

            if(type == 0){
                value = hamming(node.mat) + node.depth;
            }
            else{
                value = manhattan(node.mat) + node.depth;
            }

            return value;
        }

        public void solve()
        {
            Node parent = new Node();
            parent.mat = matrix;
            parent.n = n;
            parent.state = setState(parent);
            parent.x0 = x0;
            parent.y0 = y0;
            parent.hValue = method(methodType, parent); 
            set.Add(parent.state);
            priorityQueue.push(parent);

            while (priorityQueue.getSize() != 0)
            {
                if (parent.hValue == parent.depth)
                    break;

                parent = priorityQueue.pop();
                childFactory(parent);
            }

            if (n == 3)
                printroot(parent);
            Console.WriteLine("steps :" + parent.depth);
        }

        void childFactory(Node parent)
        {    
            if (parent.x0 - 1 >= 0)
            {
                Node newNode = Swap(parent.x0, parent.y0, parent.x0 - 1, parent.y0, parent);
                child(newNode);
            }
            if (parent.x0 + 1 < n)
            {
                Node newNode = Swap(parent.x0, parent.y0, parent.x0 + 1, parent.y0, parent);
                child(newNode);
            }
            if (parent.y0 + 1 < n)
            {
                Node newNode = Swap(parent.x0, parent.y0, parent.x0, parent.y0 + 1, parent);
                child(newNode);
            }
            if (parent.y0 - 1 >= 0)
            {
                Node newNode = Swap(parent.x0, parent.y0, parent.x0, parent.y0 - 1, parent);
                child(newNode);
            }
        }

        void child(Node newNode)
        {
            if (!set.Contains(newNode.state))
            {
                set.Add(newNode.state);
                newNode.hValue = method(methodType, newNode);
                priorityQueue.push(newNode);
            }
        }

        int manhattan(int[,] mat)
        {
            int miss = 0;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (mat[i, j] != 0)
                    {
                        int x = (mat[i, j] - 1) / n;
                        int y = (mat[i, j] - 1) % n;
                        miss += Math.Abs(x - i) + Math.Abs(y - j);
                    }
                }
            }
            return miss;
        }

        public int hamming(int[,] mat)
        {
            int misPlaced = 0;
            int temp = 0;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (mat[i, j] != 0 && mat[i, j] != (1 + j + (i % n) * n))
                        misPlaced++;
                    temp++;
                }
            }

            return misPlaced;
        }

        Node Swap(int x, int y, int x1, int y1, Node node)
        {
            Node newNode = new Node();
            newNode.mat = node.mat.Clone() as int[,];
            newNode.depth = node.depth + 1;
            newNode.parent = node;
            newNode.n = n;
            
            newNode.mat[x, y] = newNode.mat[x1, y1];
            newNode.mat[x1, y1] = 0;
            newNode.state = setState(newNode);

            newNode.x0 = x1;
            newNode.y0 = y1;
            return newNode;
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

        void printroot(Node root)
        {
            if (root == null)
            {
                return;
            }
            printroot(root.parent);
            Print(root.mat);
        }

        public string setState(Node node)
        {
            string state = "";
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    state += node.mat[i, j];
                }
            }
            return state;
        }
    }
}

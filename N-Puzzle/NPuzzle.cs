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
        int[] aroundX = { -1, 1, 0, 0 };
        int[] aroundY = { 0, 0, -1, 1 };
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
            Node parent = new(0,"",null,n);
            parent.mat = matrix;
            parent.state = setState(parent.mat);
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

        bool permissionToGo(int x , int y)
        {
            if (x < 0 || y < 0 || x >= n || y >= n) return false;
            return true;
        }

        void childFactory(Node parent)
        {
            for (int i = 0; i < 4; i++)
            {
                if (permissionToGo(parent.x0 + aroundX[i], parent.y0 + aroundY[i]))
                {
                    int[,] temp = parent.mat.Clone() as int[,];
                    Swap(temp,parent.x0 + aroundX[i], parent.y0 + aroundY[i], parent);
                    string s = setState(temp);
                    if (!set.Contains(s))
                    {
                        Node newNode = generate(parent.x0, parent.y0, parent.x0 + aroundX[i], parent.y0 + aroundY[i], s, parent);
                        set.Add(newNode.state);
                        newNode.hValue = method(methodType, newNode);
                        priorityQueue.push(newNode);
                    }
                }
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
                    if (mat[i, j] != 0 && mat[i, j] != (1 + j + (i * n)))
                        misPlaced++;
                    temp++;
                }
            }

            return misPlaced;
        }

        void Swap(int[,] mat , int x , int y , Node parent)
        {
            mat[parent.x0, parent.y0] = mat[x, y];
            mat[x, y] = 0;
        }

        Node generate(int x, int y, int x1, int y1, string state,Node node)
        {
            Node newNode = new(node.depth + 1, state, node, n);
            newNode.mat = node.mat.Clone() as int[,];
            newNode.mat[x, y] = newNode.mat[x1, y1];
            newNode.mat[x1, y1] = 0;
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

        public string setState(int[,] matrix)
        {
            string state = "";
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    state += matrix[i, j];
                }
            }
            return state;
        }
    }
}

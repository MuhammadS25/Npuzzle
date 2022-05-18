using System;
using System.Collections.Generic;
using System.Text;

namespace N_Puzzle
{
    class NPuzzle
    {
        public int[,] matrix;
        public int n, methodType;
        public int x0, y0;
        bool first = true;
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

        public bool isSolvable(List<int> lis) //Θ(S^2)
        {
            int inversion = 0;
            for (int x = 0; x < n * n - 1; x++) //Θ(S^2)
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
                if (inversion % 2 != 0) return false; //Θ(1)
            }
            else
            {
                int index = 0;
                for (int i = n - 1; i >= 0; i--) //Θ(n^2)
                    for (int j = n - 1; j >= 0; j--)
                        if (matrix[i, j] == 0)
                            index = n - i;

                if ((index % 2 == 0 && inversion % 2 == 0) || (index % 2 != 0 && inversion % 2 != 0)) return false;
            }
            return true;
        } 

        int method(int type, Node node, int cost = 0) //O(n^2)
        { 

            int value = 0;

            if(type == 0){
                value = hamming(node.mat) + node.depth; //Θ(n^2)
            }
            else{
                value = manhattan(node.mat , cost) + node.depth; //O(n^2)
            }

            return value;
        }

        public Node solve() //O(Elog(V) + n^2)
        {
            Node parent = new(0,"",null,n);
            parent.mat = matrix;
            parent.state = setState(parent.mat); //Θ(n^2)
            parent.x0 = x0;
            parent.y0 = y0;
            parent.hValue = method(methodType, parent); //O(n^2)
            set.Add(parent.state);
            priorityQueue.push(parent); //O(log(V))

            while (priorityQueue.getSize() != 0) //Θ(1)
            {
                if (parent.hValue == parent.depth)
                    break;

                parent = priorityQueue.pop(); //O(log(V))
                childFactory(parent);
            }

            return parent;
        }

        bool permissionToGo(int x , int y) //Θ(1)
        {
            if (x < 0 || y < 0 || x > n - 1 || y > n - 1) return false;
            return true;
        }

        void childFactory(Node parent) //O(log(V) + n^2)
        {
            for (int i = 0; i < 4; i++)
            {
                if (permissionToGo(parent.x0 + aroundX[i], parent.y0 + aroundY[i])) //Θ(1)
                {
                    int[,] temp = parent.mat.Clone() as int[,]; //Θ(n^2)
                    int cost = Swap(temp,parent.x0 + aroundX[i], parent.y0 + aroundY[i], parent , parent.depth); //Θ(1)
                    string s = setState(temp); //Θ(n^2)
                    if (!set.Contains(s))
                    {
                        Node newNode = generate(parent.x0, parent.y0, parent.x0 + aroundX[i], parent.y0 + aroundY[i], s, parent); //Θ(n^2)
                        set.Add(newNode.state); //Θ(1)
                        newNode.hValue = method(methodType, newNode, cost);//O(n^2)
                        priorityQueue.push(newNode);//O(log(V))
                    }
                }
            }
        }

        int manhattan(int[,] mat , int cost) //O(n^2)
        {
            int miss = 0;
            if (first) { 
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
                        first = false;
                }
            }
            else
            {
                miss = cost;
            }
            return miss;
        }

        public int hamming(int[,] mat) //Θ(n^2)
        {
            int misPlaced = 0;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (mat[i, j] != 0 && mat[i, j] != (1 + j + (i * n)))
                        misPlaced++;
                }
            }

            return misPlaced;
        }

        int Swap(int[,] mat , int x , int y , Node parent, int depth) // O(1)
        {
            mat[parent.x0, parent.y0] = mat[x, y];

            int rightX = (mat[x, y] - 1) / n;
            int rightY = (mat[x, y] - 1) % n;
            int miss = parent.hValue - Math.Abs(rightX - x) - Math.Abs(rightY - y);

            int newX = (mat[parent.x0, parent.y0] - 1) / n;
            int newY = (mat[parent.x0, parent.y0] - 1) % n;

            int cost = miss + (Math.Abs(newX - parent.x0) + Math.Abs(newY - parent.y0)) - depth;

            mat[x, y] = 0;
            return cost;

        }

        Node generate(int x, int y, int x1, int y1, string state,Node node) //Θ(n^2)
        {
            Node newNode = new(node.depth + 1, state, node, n);
            newNode.mat = node.mat.Clone() as int[,];
            newNode.mat[x, y] = newNode.mat[x1, y1];
            newNode.mat[x1, y1] = 0;
            newNode.x0 = x1;
            newNode.y0 = y1;
            return newNode;
        }

        void Print(int[,] matrix) //Θ(n^2)
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

        public void printroot(Node root) //??
        {
            if (root == null)
            {
                return;
            }
            printroot(root.parent);
            Print(root.mat);
        }

        public string setState(int[,] matrix) //Θ(n^2)
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

using System;
using System.Collections.Generic;
using System.Text;

namespace N_Puzzle
{
    class NPuzzle
    {
        public int[,] matrix;
        public int[,] goal;
        int n;
        public int x0, y0;


        public NPuzzle(int n)
        {
            this.n = n;
            matrix = new int[n, n];
            goal = new int[n, n];
            int temp = 1;

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (temp != n * n)
                    {
                        goal[i, j] = temp;
                        temp++;
                    }
                    else goal[i, j] = 0;
                }
            }
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

        bool isGoal(Node parent)
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (parent.mat[i, j] != goal[i, j])
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        void findZero(Node node)
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (node.mat[i, j] == 0)
                    {
                        node.x0 = i;
                        node.y0 = j;
                    }
                }
            }
        }

        public void solveHamming()
        {
            PriorityQueue priorityQueue = new PriorityQueue();
            HashSet<string> set = new HashSet<string>();
            Node parent = new Node(matrix, 0, null);
            parent.setState();
            parent.x0 = x0;
            parent.y0 = y0;
            parent.hValue = hamming(parent.mat) ;
            set.Add(parent.state);
            priorityQueue.push(parent);


            while (priorityQueue.getSize() != 0)
            {
                parent = priorityQueue.pop();
                int[] aroundZeroX = new int[4] { parent.x0 - 1, parent.x0 + 1, parent.x0, parent.x0 };
                int[] aroundZeroY = new int[4] { parent.y0, parent.y0, parent.y0 + 1, parent.y0 - 1 };
                //Print(parent.mat);
                if (isGoal(parent))
                {
                    if( n == 3)
                        printroot(parent);
                    Console.WriteLine("steps :" + parent.depth);
                    break;
                }

                for (int i = 0; i < 4; i++)
                {
                    try
                    {
                        Node newNode = Swap(parent.x0, parent.y0, aroundZeroX[i], aroundZeroY[i], parent);
                        newNode.setState();
                        if (!set.Contains(newNode.state))
                        {
                            set.Add(newNode.state);
                            newNode.hValue = manhattan(newNode.mat) + newNode.depth ;
                            priorityQueue.push(newNode);
                        }

                    }
                    catch
                    {
                        continue;
                    }
                }
            }
        }

        int manhattan(int[,] mat)
        {
            int sum = 0;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (mat[i, j] != 0)
                    {
                        int x = (mat[i, j] - 1) / n;
                        int y = (mat[i, j] - 1) % n;
                        sum += Math.Abs(x - i) + Math.Abs(y - j);
                    }
                }
            }
            return sum;
        }

        public int hamming(int[,] mat)
        {
            int misPlaced = 0;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (mat[i, j] != 0 && mat[i, j] != goal[i, j])
                        misPlaced++;
                }
            }

            return misPlaced;
        }

        Node Swap(int x, int y, int x1, int y1, Node node)
        {
            Node newNode = new Node(node.mat, node.depth+1 ,node);
            int temp = newNode.mat[x, y];
            newNode.mat[x, y] = newNode.mat[x1, y1];
            newNode.mat[x1, y1] = temp;

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
    }
}

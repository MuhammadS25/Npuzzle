using System;
using System.Collections.Generic;
using System.IO;

namespace N_Puzzle
{
    /*
     * 8 Puzzle - Case 1
     * 8 Puzzle(2) - Case 1
     * 8 Puzzle(3) - Case 1
     * 15 Puzzle - Case 2
     * 15 Puzzle - Case 3
     */
    class Program
    {
        
        static void Main(string[] args)
        {

            Console.WriteLine("Solvable [1] : \nUnSolvable [2] :");
            int solvable = int.Parse(Console.ReadLine());
            if (solvable == 1)
            {
                string[] samplefilenames = { "8 Puzzle (1)", "8 Puzzle (2)", "8 Puzzle (3)", "15 Puzzle - 1", "24 Puzzle 1", "24 Puzzle 2" };
                string[] completefilenames = { "15 Puzzle 1", "15 Puzzle 3", "15 Puzzle 4", "15 Puzzle 5" };
                string[] completefilenames2 = { "50 Puzzle", "99 Puzzle - 1", "99 Puzzle - 2", "9999 Puzzle" };
                Console.WriteLine("Sample : ");
                foreach (string name in samplefilenames)
                {
                    string solvedSample = @"D:\college\Algo\[3] N Puzzle\Testcases\Sample\Sample Test\Solvable Puzzles\" + name + @".txt";
                    NPuzzle nPuzzle = new NPuzzle();
                    List<int> lis = new List<int>();
                    string[] s = File.ReadAllLines(solvedSample);
                    int n = int.Parse(s[0]);
                    int[,] matrix = new int[n, n];
                    int c = 0;
                    for (int i = 2; i < s.Length; i++)
                    {
                        string[] k = s[i].Split(' ');
                        for (int j = 0; j < k.Length; j++)
                        {
                            if (!k[j].Equals(""))
                            {
                                int result = 0;
                                int.TryParse(k[j], out result);
                                lis.Add(result);
                                matrix[c, j] = result;
                            }
                        }
                        c++;
                    }

                    Console.WriteLine(nPuzzle.isSolvable(lis, n, matrix) ? "Solvable" : "not Solvable");
                    Console.WriteLine();


                }

                Console.WriteLine("#####################################");
                Console.WriteLine("Complete : ");

                foreach (string name in completefilenames)
                {
                    string solvedComplete = @"D:\college\Algo\[3] N Puzzle\Testcases\Complete\Complete Test\Complete Test\Solvable puzzles\Manhattan Only\" + name + @".txt";
                    NPuzzle nPuzzle = new NPuzzle();
                    List<int> lis = new List<int>();
                    string[] s = File.ReadAllLines(solvedComplete);
                    int n = int.Parse(s[0]);
                    int[,] matrix = new int[n, n];
                    int c = 0;
                    for (int i = 2; i < s.Length; i++)
                    {
                        string[] k = s[i].Split(' ');
                        for (int j = 0; j < k.Length; j++)
                        {
                            if (!k[j].Equals(""))
                            {
                                int result = 0;
                                int.TryParse(k[j], out result);
                                lis.Add(result);
                                matrix[c, j] = result;
                            }
                        }
                        c++;
                    }

                    Console.WriteLine(nPuzzle.isSolvable(lis, n, matrix) ? "Solvable" : "not Solvable");
                    Console.WriteLine();
                }

                foreach (string name in completefilenames2)
                {
                    string solvedComplete = @"D:\college\Algo\[3] N Puzzle\Testcases\Complete\Complete Test\Complete Test\Solvable puzzles\Manhattan & Hamming\" + name + @".txt";
                    NPuzzle nPuzzle = new NPuzzle();
                    List<int> lis = new List<int>();
                    string[] s = File.ReadAllLines(solvedComplete);
                    int n = int.Parse(s[0]);
                    int[,] matrix = new int[n, n];
                    int c = 0;
                    for (int i = 2; i < s.Length; i++)
                    {
                        string[] k = s[i].Split(' ');
                        for (int j = 0; j < k.Length; j++)
                        {
                            if (!k[j].Equals(""))
                            {
                                int result = 0;
                                int.TryParse(k[j], out result);
                                lis.Add(result);
                                matrix[c, j] = result;
                            }
                        }
                        c++;
                    }

                    Console.WriteLine(nPuzzle.isSolvable(lis, n, matrix) ? "Solvable" : "not Solvable");
                    Console.WriteLine();
                }
            }
            else if (solvable != 1)
            {
                string[] samplefilenames = { "8 Puzzle - Case 1", "8 Puzzle(2) - Case 1", "8 Puzzle(3) - Case 1", "15 Puzzle - Case 2", "15 Puzzle - Case 3" };
                string[] completefilenames = { "15 Puzzle 1 - Unsolvable", "99 Puzzle - Unsolvable Case 1", "99 Puzzle - Unsolvable Case 2", "9999 Puzzle" };
                Console.WriteLine("Sample : ");
                foreach (string name in samplefilenames)
                {
                    string unsolvedSample = @"D:\college\Algo\[3] N Puzzle\Testcases\Sample\Sample Test\Unsolvable Puzzles\" + name + @".txt";
                    NPuzzle nPuzzle = new NPuzzle();
                    List<int> lis = new List<int>();
                    string[] s = File.ReadAllLines(unsolvedSample);
                    int n = int.Parse(s[0]);
                    int[,] matrix = new int[n, n];
                    int c = 0;
                    for (int i = 2; i < s.Length; i++)
                    {
                        string[] k = s[i].Split(' ');
                        for (int j = 0; j < k.Length; j++)
                        {
                            if (!k[j].Equals(""))
                            {
                                int result = 0;
                                int.TryParse(k[j], out result);
                                lis.Add(result);
                                matrix[c, j] = result;
                            }
                        }
                        c++;
                    }

                    Console.WriteLine(nPuzzle.isSolvable(lis, n, matrix) ? "Solvable" : "not Solvable");
                    Console.WriteLine();
                }

                Console.WriteLine("#####################################");
                Console.WriteLine("Complete : ");

                foreach (string name in completefilenames)
                {
                    string unsolvedComplete = @"D:\college\Algo\[3] N Puzzle\Testcases\Complete\Complete Test\Complete Test\Unsolvable puzzles\" + name + @".txt";
                    NPuzzle nPuzzle = new NPuzzle();
                    List<int> lis = new List<int>();
                    string[] s = File.ReadAllLines(unsolvedComplete);
                    int n = int.Parse(s[0]);
                    int[,] matrix = new int[n, n];
                    int c = 0;
                    for (int i = 2; i < s.Length; i++)
                    {
                        string[] k = s[i].Split(' ');
                        for (int j = 0; j < k.Length; j++)
                        {
                            if (!k[j].Equals(""))
                            {
                                int result = 0;
                                int.TryParse(k[j], out result);
                                lis.Add(result);
                                matrix[c, j] = result;
                            }
                        }
                        c++;
                    }

                    Console.WriteLine(nPuzzle.isSolvable(lis, n, matrix) ? "Solvable" : "not Solvable");
                    Console.WriteLine();
                }
            }


            //PriorityQueue queue = new PriorityQueue();

            //queue.push(1);
            //queue.push(5);
            //queue.push(6);
            //queue.push(3);
            //queue.push(7);
            //queue.push(100);
            //queue.push(0);
            //queue.push(1000);
            //queue.push(200);
            //queue.push(2);
            //queue.push(3);
            //queue.push(101);


            //while (queue.getSize() >= 0)
            //{
            //    Console.WriteLine(queue.pop());
            //}

        }
    }
}
